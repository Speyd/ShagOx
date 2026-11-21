-- Fix rows with "Id" = 0 that came from an imported dump.
--
-- EF Core treats a key value of 0 as "not set", so when such an entity is part of a graph
-- passed to DbSet.Update(...) it is tracked as Added and EF issues an INSERT instead of an
-- UPDATE. Example: a user whose CityId = 0 (Kyiv) re-registers via the form ->
-- INSERT INTO "Cities" ... -> 23505 duplicate key "IX_Cities_Code".
--
-- The script moves every Id = 0 row to the next identity value and rewrites all foreign
-- keys that point to it. It is idempotent and runs in a single transaction.
--
-- Run on the PRIMARY (replicas receive the change via streaming replication):
--   $pw = (Select-String -Path .env -Pattern '^POSTGRES_PASSWORD=(.*)').Matches.Groups[1].Value
--   Get-Content backend/scripts/fix-zero-ids.sql -Raw |
--     docker exec -i -e PGPASSWORD=$pw postgres-primary psql -h 127.0.0.1 -U postgres -d shagox -v ON_ERROR_STOP=1
-- Afterwards flush the data cache (container "redis") or restart the API.

begin;

create function pg_temp.renumber_zero_id(tbl text) returns void
language plpgsql as $$
declare
    rel      regclass := format('%I', tbl)::regclass;
    seq      text     := pg_get_serial_sequence(format('%I', tbl), 'Id');
    new_id   bigint;
    fk       record;
    fks      jsonb := '[]'::jsonb;
    has_zero boolean;
begin
    execute format('select exists(select 1 from %s where "Id" = 0)', rel) into has_zero;
    if not has_zero then
        raise notice '%: no Id = 0 row, skipped', tbl;
        return;
    end if;

    if seq is null then
        raise exception '%: "Id" has no identity/sequence', tbl;
    end if;

    -- make sure the sequence is ahead of existing data, then take a fresh id
    execute format('select setval(%L, coalesce((select max("Id") from %s), 0) + 1, false)', seq, rel);
    new_id := nextval(seq);

    -- drop inbound foreign keys, remembering their definitions
    for fk in
        select c.conname,
               c.conrelid::regclass as child,
               a.attname            as col,
               pg_get_constraintdef(c.oid) as def
        from pg_constraint c
        join pg_attribute a on a.attrelid = c.conrelid and a.attnum = c.conkey[1]
        where c.contype = 'f' and c.confrelid = rel
    loop
        fks := fks || jsonb_build_object('name', fk.conname, 'child', fk.child::text,
                                         'col', fk.col, 'def', fk.def);
        execute format('alter table %s drop constraint %I', fk.child, fk.conname);
    end loop;

    execute format('update %s set "Id" = %s where "Id" = 0', rel, new_id);

    for fk in select * from jsonb_to_recordset(fks) as x(name text, child text, col text, def text)
    loop
        execute format('update %s set %I = %s where %I = 0', fk.child, fk.col, new_id, fk.col);
        execute format('alter table %s add constraint %I %s', fk.child, fk.name, fk.def);
    end loop;

    raise notice '%: Id 0 -> % (% foreign key(s) rewritten)', tbl, new_id, jsonb_array_length(fks);
end $$;

select pg_temp.renumber_zero_id('Cities');
select pg_temp.renumber_zero_id('CityTranslations');
select pg_temp.renumber_zero_id('ProductTypes');
select pg_temp.renumber_zero_id('ProductTypeTranslations');

commit;
