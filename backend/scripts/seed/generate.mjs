// Генератор демо-наповнення каталогу ShagOx.
//
//   node backend/scripts/seed/generate.mjs        → створює seed.sql поруч
//
// Що робить seed.sql (одна транзакція, можна запускати повторно):
//   1. Доповнює довідники значень (кольори, сокети, бренди, ...) з перекладами uk/en/de.
//   2. Приводить атрибути категорій до схеми з data/catalog-*.mjs
//      (brand — атрибут товару, color — довідник, прибирає нерелевантні атрибути).
//   3. Створює демо-продавців і покупців (email @shagox.demo).
//   4. Оновлює наявні товари з тими самими назвами (атрибути, опис, ціни в ₴, варіанти)
//      і додає нові товари з варіантами та фото (фото беруться з наявних товарів категорії).
//   5. Генерує відгуки з оцінками та відповідями продавців.
//
// Дані детерміновані (фіксований seed генератора випадкових чисел).

import fs from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";
import { DICTIONARIES, HEX_TO_COLOR } from "./data/dictionaries.mjs";
import { ATTRIBUTES } from "./data/attributes.mjs";
import gadgets from "./data/catalog-gadgets.mjs";
import computer from "./data/catalog-computer.mjs";
import accessories from "./data/catalog-accessories.mjs";
import home from "./data/catalog-home.mjs";
import * as people from "./data/people.mjs";

const CATALOG = { ...gadgets, ...computer, ...accessories, ...home };
const LANGS = ["uk", "en", "de"];
const NOW = new Date("2026-11-01T12:00:00Z");
const DAY = 24 * 60 * 60 * 1000;

const CATEGORY_GROUPS = {
  GadgetsAndSmartphones: ["smartphones", "tablets", "smartwatches", "e-readers"],
  ComputersAndHardware: ["laptops", "desktop-pcs", "processors", "graphics-cards", "motherboards", "ram-memory", "ssd-drives", "hdd-drives", "power-supplies", "pc-cases", "cooling-systems", "routers", "external-drives"],
  PeripheralsAndAudio: ["headphones", "wireless-speakers", "monitors", "keyboards", "mice-pads", "webcams", "microphones"],
  TVAndVideo: ["televisions", "drones", "action-cameras", "dslr-cameras", "security-cameras"],
  Gaming: ["gaming-consoles"],
  AccessoriesAndPower: ["powerbanks", "chargers-cables", "phone-cases", "screen-protectors", "power-stations"],
  SmartHomeAndLighting: ["smarthome-hubs", "smart-lighting"],
  MajorAppliances: ["refrigerators", "washing-machines"],
  SmallKitchenAppliances: ["coffee-makers", "microwaves", "electric-kettles", "air-fryers"],
  HomeCleaning: ["robot-vacuums", "air-purifiers", "vacuum-cleaners"],
  PersonalCare: ["shavers-trimmers", "hair-dryers", "electric-toothbrushes"],
  Transport: ["electric-scooters"],
};

// ---------------------------------------------------------------- утиліти

function createRandom(seed) {
  let a = seed >>> 0;
  return () => {
    a = (a + 0x6d2b79f5) >>> 0;
    let t = a;
    t = Math.imul(t ^ (t >>> 15), t | 1);
    t ^= t + Math.imul(t ^ (t >>> 7), t | 61);
    return ((t ^ (t >>> 14)) >>> 0) / 4294967296;
  };
}
const random = createRandom(20261105);
const randInt = (min, max) => min + Math.floor(random() * (max - min + 1));
const pick = (items) => items[Math.floor(random() * items.length)];
const chance = (p) => random() < p;
const shuffle = (items) => {
  const copy = [...items];
  for (let i = copy.length - 1; i > 0; i--) {
    const j = Math.floor(random() * (i + 1));
    [copy[i], copy[j]] = [copy[j], copy[i]];
  }
  return copy;
};
const weighted = (entries) => {
  const total = entries.reduce((s, [, w]) => s + w, 0);
  let r = random() * total;
  for (const [value, w] of entries) if ((r -= w) <= 0) return value;
  return entries[0][0];
};

const slug = (s) => s.toLowerCase().replace(/[^a-z0-9]+/g, "-").replace(/^-|-$/g, "");
const sql = (v) => (v === null || v === undefined ? "null" : `'${String(v).replace(/'/g, "''")}'`);
const ts = (date) => `${sql(date.toISOString())}::timestamptz`;
const daysAgo = (days) => new Date(NOW.getTime() - days * DAY - randInt(0, DAY - 1));
const roundPrice = (n) => (n >= 1000 ? Math.round(n / 100) * 100 - 1 : Math.round(n / 10) * 10 - 1);

function fail(message) {
  throw new Error(`Помилка в даних: ${message}`);
}

// ---------------------------------------------------------------- схема та валідація

function parseSchema(schema, categoryCode) {
  return schema.split(/\s+/).filter(Boolean).map((token) => {
    const [key, flags = ""] = token.split(":");
    const def = ATTRIBUTES[key];
    if (!def) fail(`${categoryCode}: невідомий атрибут "${key}"`);
    return {
      key,
      def,
      required: flags.includes("r"),
      variant: flags.includes("v"),
      multiple: flags.includes("m"),
      dict: key === "brand" ? `brand-${categoryCode}` : def.dict ?? null,
    };
  });
}

const dictionaryCodes = Object.fromEntries(
  Object.entries(DICTIONARIES).map(([code, values]) => [code, new Set(values.map((v) => v[0]))]),
);
const dictionaryLabels = Object.fromEntries(
  Object.entries(DICTIONARIES).map(([code, values]) => [code, Object.fromEntries(values.map((v) => [v[0], v[1]]))]),
);

function validateValue(categoryCode, title, attr, value) {
  const where = `${categoryCode} / ${title} / ${attr.key}`;
  const check = (v) => {
    switch (attr.def.type) {
      case "Select": {
        const codes = dictionaryCodes[attr.dict];
        if (!codes?.has(v)) fail(`${where}: значення "${v}" відсутнє в довіднику ${attr.dict}`);
        break;
      }
      case "Integer":
        if (!Number.isInteger(v)) fail(`${where}: очікується ціле число, отримано ${v}`);
        break;
      case "Decimal":
        if (typeof v !== "number") fail(`${where}: очікується число, отримано ${v}`);
        break;
      case "Boolean":
        if (typeof v !== "boolean") fail(`${where}: очікується true/false`);
        break;
      case "String":
        if (typeof v !== "string" || !v.trim()) fail(`${where}: очікується непорожній рядок`);
        break;
    }
  };
  if (attr.multiple) {
    if (!Array.isArray(value) || value.length === 0) fail(`${where}: очікується непорожній масив`);
    value.forEach(check);
  } else {
    if (Array.isArray(value)) fail(`${where}: атрибут не множинний`);
    check(value);
  }
}

function prepareCatalog() {
  const titles = new Set();
  const categories = [];

  for (const [code, category] of Object.entries(CATALOG)) {
    const schema = parseSchema(category.schema, code);
    const byKey = Object.fromEntries(schema.map((a) => [a.key, a]));
    if (!byKey.brand || byKey.brand.variant) fail(`${code}: brand має бути атрибутом товару`);

    const brands = category.brands.map((name) => [slug(name), name]);
    dictionaryCodes[`brand-${code}`] = new Set(brands.map(([c]) => c));
    dictionaryLabels[`brand-${code}`] = Object.fromEntries(brands);

    for (const product of category.products) {
      if (titles.has(product.t)) fail(`дубль назви "${product.t}"`);
      titles.add(product.t);

      const attrs = { brand: product.b, ...product.a };
      for (const [key, value] of Object.entries(attrs)) {
        const attr = byKey[key];
        if (!attr || attr.variant) fail(`${code} / ${product.t}: "${key}" не є атрибутом товару в цій категорії`);
        validateValue(code, product.t, attr, value);
      }
      for (const attr of schema.filter((a) => a.required && !a.variant)) {
        if (attrs[attr.key] === undefined) fail(`${code} / ${product.t}: бракує обов'язкового "${attr.key}"`);
      }
      for (const [key, values] of Object.entries(product.v)) {
        const attr = byKey[key];
        if (!attr?.variant) fail(`${code} / ${product.t}: "${key}" не є варіантним атрибутом`);
        values.forEach((v) => validateValue(code, product.t, attr, Array.isArray(v) ? v[0] : v));
      }
      for (const attr of schema.filter((a) => a.required && a.variant)) {
        if (!product.v[attr.key]?.length) fail(`${code} / ${product.t}: бракує варіантів для "${attr.key}"`);
      }
    }

    categories.push({ code, ...category, schema, byKey, brands });
  }
  return categories;
}

// ---------------------------------------------------------------- значення та описи

function valueSql(attr, value) {
  const one = (v) => {
    switch (attr.def.type) {
      case "Select":
        return `pg_temp.dv(${sql(attr.dict)}, ${sql(v)})`;
      case "Boolean":
        return v ? "true" : "false";
      case "String":
        return `${sql(v)}::text`;
      default:
        return String(v);
    }
  };
  return attr.multiple ? `jsonb_build_array(${value.map(one).join(", ")})` : one(value);
}

function jsonSql(category, entries) {
  if (entries.length === 0) return "'{}'::jsonb";
  const parts = entries.map(([key, value]) => `${sql(key)}, ${valueSql(category.byKey[key], value)}`);
  return `jsonb_build_object(${parts.join(", ")})`;
}

function splitName(ukName) {
  const match = ukName.match(/^(.*?)\s*\(([^)]+)\)$/);
  return match ? [match[1], match[2]] : [ukName, ""];
}

function formatValue(attr, value) {
  const fmt = (v) => {
    if (attr.def.type === "Select") return dictionaryLabels[attr.dict]?.[v] ?? v;
    if (attr.def.type === "Boolean") return v ? "так" : "ні";
    return String(v).replace(".", ",");
  };
  const [, unit] = splitName(attr.def.names[0]);
  const text = Array.isArray(value) ? value.map(fmt).join(", ") : fmt(value);
  if (!unit || attr.def.type === "Select" || attr.def.type === "Boolean" || attr.def.type === "String") return text;
  return unit === "″" ? `${text}″` : `${text} ${unit}`;
}

const INTRO = [
  "надійний вибір для щоденного використання.",
  "поєднання сучасного дизайну та високої продуктивності.",
  "одна з найпопулярніших моделей у своєму класі.",
  "оптимальне співвідношення ціни та можливостей.",
  "модель, яку обирають за якість і надійність.",
];
const CONDITION_TEXT = {
  new: ["Новий, у заводській упаковці. Офіційна гарантія 12 місяців.", "Новий товар з офіційною гарантією від виробника."],
  like_new: ["Стан як новий: використовувався кілька разів, повний комплект, без слідів експлуатації. Гарантія магазину 6 місяців."],
  refurbished: ["Відновлений у сервісному центрі, пройшов повну діагностику та заміну зношених деталей. Гарантія магазину 6 місяців."],
  excellent: ["Вживаний, у відмінному стані. Повністю справний, без подряпин, у комплекті коробка та зарядний пристрій."],
  good: ["Вживаний, у доброму стані: є незначні сліди використання, на роботу не впливають. Перевірений перед продажем."],
};
const DELIVERY = [
  "Відправляємо Новою поштою в день замовлення до 16:00.",
  "Доставка по всій Україні, оплата при отриманні або онлайн.",
  "Можливий самовивіз з магазину або кур’єрська доставка.",
];

function describe(category, product, condition) {
  const lines = [`${category.noun} ${product.t} — ${pick(INTRO)}`, "", "Основні характеристики:"];
  for (const [key, value] of Object.entries(product.a)) {
    const attr = category.byKey[key];
    lines.push(`• ${splitName(attr.def.names[0])[0]}: ${formatValue(attr, value)}`);
  }
  const variantText = Object.entries(product.v)
    .filter(([, values]) => values.length > 1)
    .map(([key, values]) => {
      const attr = category.byKey[key];
      return `${splitName(attr.def.names[0])[0].toLowerCase()} — ${values.map((v) => formatValue(attr, Array.isArray(v) ? v[0] : v)).join(" / ")}`;
    });
  if (variantText.length) lines.push("", `Доступні варіанти: ${variantText.join("; ")}.`);
  lines.push("", pick(CONDITION_TEXT[condition]), pick(DELIVERY));
  return lines.join("\n");
}

function expandVariants(product) {
  return Object.entries(product.v).reduce(
    (combos, [key, values]) =>
      combos.flatMap((combo) =>
        values.map((raw) => {
          const [value, delta] = Array.isArray(raw) ? raw : [raw, 0];
          return { entries: [...combo.entries, [key, value]], delta: combo.delta + delta };
        }),
      ),
    [{ entries: [], delta: 0 }],
  );
}

function variantsValuesSql(category, product) {
  const discount = chance(0.35) ? 0.08 + random() * 0.17 : 0;
  return expandVariants(product)
    .map(({ entries, delta }) => {
      const price = product.price + delta;
      const previous = discount ? roundPrice(price * (1 + discount)) : 0;
      const stock = chance(0.07) ? 0 : randInt(1, 40);
      return `(${price}, ${previous}, ${stock}, ${jsonSql(category, entries)})`;
    })
    .join(",\n    ");
}

// ---------------------------------------------------------------- відгуки

function reviewText(grade, noun) {
  const t = people.REVIEW_TEXT;
  const parts = [pick(t.opener[grade])];
  if (grade === 5) parts.push(pick(t.pro), chance(0.5) ? pick(t.pro) : "");
  if (grade === 4) parts.push(pick(t.pro), chance(0.6) ? pick(t.con) : "");
  if (grade === 3) parts.push(pick(t.con), chance(0.5) ? pick(t.pro) : "");
  if (grade === 2) parts.push(pick(t.negative), chance(0.5) ? pick(t.con) : "");
  if (grade === 1) parts.push(pick(t.negative));
  parts.push(pick(t.closer[grade]));
  return [...new Set(parts.filter(Boolean))].join(" ").replaceAll("{noun}", noun);
}

const SELLER_ANSWERS = [
  "Так, офіційна гарантія 12 місяців, оформлюємо чек і гарантійний талон.",
  "Так, повна комплектація від виробника.",
  "Зазвичай 1–2 дні Новою поштою по Україні.",
];

function reviewsSql(category, product) {
  const count = weighted([[0, 15], [randInt(1, 3), 35], [randInt(4, 7), 35], [randInt(8, 12), 15]]);
  const reviewers = shuffle(people.BUYERS).slice(0, count);
  const advId = `pg_temp.adv_id(${sql(product.t)})`;
  const sellerOf = (r) => `(select "OwnerId" from "Advertisements" where "Id" = ${r}."CommentableEntityId")`;

  return reviewers.map(([userName]) => {
    const grade = weighted([[5, 52], [4, 28], [3, 11], [2, 5], [1, 4]]);
    const created = daysAgo(randInt(2, 300));
    const at = (hours) => ts(new Date(Math.min(created.getTime() + hours * 3600000, NOW.getTime())));
    const insertReview = `insert into "ReviewComments"("Grade","Text","CommentableEntityId","CreatedAt","OwnerId","EntityStatus")
  values (${grade}, ${sql(reviewText(grade, category.noun))}, ${advId}, ${ts(created)}, pg_temp.uid(${sql(userName)}), 'Active')`;
    const replyCols = `"ReviewRepliComments"("ParentReplyId","Text","CreatedAt","OwnerId","CommentableEntityId","EntityStatus")`;

    const sellerReplies = grade <= 2 ? chance(0.8) : grade >= 4 && chance(0.2);
    const question = chance(0.08) ? randInt(0, people.BUYER_QUESTION_REPLY.length - 1) : null;

    if (!sellerReplies && question === null) return `${insertReview};`;

    const ctes = [`r as (${insertReview} returning "Id", "CommentableEntityId")`];
    const tail = [];

    if (sellerReplies) {
      const text = pick(grade <= 2 ? people.SELLER_REPLY.negative : people.SELLER_REPLY.positive);
      ctes.push(`s as (insert into ${replyCols} select null, ${sql(text)}, ${at(randInt(2, 30))}, ${sellerOf("r")}, r."Id", 'Active' from r returning "Id")`);
      if (grade <= 2 && chance(0.5)) {
        tail.push(`select s."Id", ${sql(pick(people.BUYER_FOLLOW_UP))}, ${at(randInt(40, 90))}, pg_temp.uid(${sql(userName)}), r."Id", 'Active' from s, r`);
      }
    }
    if (question !== null) {
      const asker = pick(people.BUYERS.filter(([u]) => u !== userName))[0];
      ctes.push(`q as (insert into ${replyCols} select null, ${sql(people.BUYER_QUESTION_REPLY[question])}, ${at(randInt(24, 200))}, pg_temp.uid(${sql(asker)}), r."Id", 'Active' from r returning "Id")`);
      tail.push(`select q."Id", ${sql(SELLER_ANSWERS[question])}, ${at(randInt(210, 260))}, ${sellerOf("r")}, r."Id", 'Active' from q, r`);
    }

    const finalInsert = tail.length
      ? `insert into ${replyCols}\n${tail.join("\nunion all\n")}`
      : "select 1";
    return `with ${ctes.join(",\n")}\n${finalInsert};`;
  });
}

// ---------------------------------------------------------------- SQL

function header() {
  return `-- Згенеровано backend/scripts/seed/generate.mjs. Не редагуйте вручну.
-- Запуск: psql -v ON_ERROR_STOP=1 -f seed.sql
begin;

-- Лічильники identity після імпорту дампу з явними Id відстають від даних —
-- синхронізуємо всі, інакше нові записи (і через API теж) падають на дублікаті ключа.
do $$
declare t record; seq text;
begin
  for t in select table_name from information_schema.columns
           where table_schema = 'public' and column_name = 'Id' and is_identity = 'YES'
  loop
    seq := pg_get_serial_sequence(format('%I', t.table_name), 'Id');
    execute format('select setval(%L, coalesce((select max("Id") from %I), 0) + 1, false)', seq, t.table_name);
  end loop;
end $$;

create function pg_temp.cat(p text) returns bigint language sql stable as
  $$ select "Id" from "Categories" where "Code" = p and "EntityStatus" = 'Active' $$;
create function pg_temp.dict(p text) returns bigint language sql stable as
  $$ select "Id" from "AttributeDictionaries" where "Code" = p and "EntityStatus" = 'Active' $$;
create function pg_temp.dv(d text, c text) returns bigint language sql stable as
  $$ select v."Id" from "AttributeDictionaryValues" v
     join "AttributeDictionaries" x on x."Id" = v."DictionaryId"
     where x."Code" = d and v."Code" = c and v."EntityStatus" = 'Active' and x."EntityStatus" = 'Active' $$;
create function pg_temp.uid(u text) returns bigint language sql stable as
  $$ select "Id" from "Users" where "Email" = u || '@${people.SEED_EMAIL_DOMAIN}' $$;
create function pg_temp.is_seed(id bigint) returns boolean language sql stable as
  $$ select exists(select 1 from "Users" where "Id" = id and "Email" like '%@${people.SEED_EMAIL_DOMAIN}') $$;
create function pg_temp.adv_id(t text) returns bigint language sql stable as
  $$ select "Id" from "Advertisements" where "Title" = t and "EntityStatus" = 'Active' order by "Id" limit 1 $$;
create function pg_temp.lookup(tbl text, c text) returns bigint language plpgsql stable as
  $$ declare r bigint; begin execute format('select "Id" from %I where "Code" = $1', tbl) into r using c; return r; end $$;
`;
}

function dictionariesSql(categories) {
  const dicts = { ...DICTIONARIES };
  for (const c of categories) dicts[`brand-${c.code}`] = c.brands.map(([code, name]) => [code, name, name, name]);

  const dictRows = Object.keys(dicts).map((d) => `(${sql(d)})`).join(", ");
  const valueRows = [];
  const translationRows = [];
  for (const [d, values] of Object.entries(dicts)) {
    for (const [code, ...names] of values) {
      valueRows.push(`(${sql(d)}, ${sql(code)})`);
      names.forEach((name, i) => translationRows.push(`(${sql(d)}, ${sql(code)}, ${sql(LANGS[i])}, ${sql(name)})`));
    }
  }

  return `
-- ============================================================ 1. Довідники
insert into "AttributeDictionaries"("Code","EntityStatus")
select x.c, 'Active' from (values ${dictRows}) x(c)
on conflict ("Code") where "EntityStatus" = 'Active' do nothing;

insert into "AttributeDictionaryValues"("DictionaryId","Code","EntityStatus")
select pg_temp.dict(x.d), x.c, 'Active' from (values
  ${valueRows.join(",\n  ")}
) x(d, c)
on conflict ("DictionaryId","Code") where "EntityStatus" = 'Active' do nothing;

insert into "AttributeDictionaryValueTranslations"("TranslatableId","Language","Name","EntityStatus")
select pg_temp.dv(x.d, x.c), x.l, x.n, 'Active' from (values
  ${translationRows.join(",\n  ")}
) x(d, c, l, n)
on conflict ("TranslatableId","Language") where "EntityStatus" = 'Active' do update set "Name" = excluded."Name";
`;
}

function definitionsSql(categories) {
  const rows = [];
  const keep = [];
  for (const c of categories) {
    for (const a of c.schema) {
      rows.push(`(${sql(c.code)}, ${sql(a.key)}, ${sql(a.def.type)}, ${a.required}, ${a.variant}, ${a.multiple}, ${sql(a.dict)})`);
      keep.push(`(${sql(c.code)}, ${sql(a.key)})`);
    }
  }
  const translations = Object.entries(ATTRIBUTES).flatMap(([key, def]) =>
    def.names.map((name, i) => `(${sql(key)}, ${sql(LANGS[i])}, ${sql(name)})`),
  );
  const managed = categories.map((c) => sql(c.code)).join(", ");

  return `
-- ============================================================ 2. Атрибути категорій
insert into "AttributeDefinitions"("CategoryId","Key","Type","Required","Min","Max","IsVariant","Multiple","DictionaryId","EntityStatus")
select pg_temp.cat(x.c), x.k, x.t, x.r, null, null, x.v, x.m, case when x.d is null then null else pg_temp.dict(x.d) end, 'Active'
from (values
  ${rows.join(",\n  ")}
) x(c, k, t, r, v, m, d)
on conflict ("CategoryId","Key") where "EntityStatus" = 'Active' do update set
  "Type" = excluded."Type", "Required" = excluded."Required", "IsVariant" = excluded."IsVariant",
  "Multiple" = excluded."Multiple", "DictionaryId" = excluded."DictionaryId", "Min" = null, "Max" = null;

-- атрибути, яких немає в схемі категорії, видаляються
delete from "AttributeDefinitions" d
using "Categories" c
where c."Id" = d."CategoryId" and c."Code" in (${managed})
  and (c."Code", d."Key") not in (select x.c, x.k from (values ${keep.join(", ")}) x(c, k));

insert into "AttributeDefinitionTranslations"("TranslatableId","Language","Name","EntityStatus")
select d."Id", x.l, x.n, 'Active'
from "AttributeDefinitions" d
join (values
  ${translations.join(",\n  ")}
) x(k, l, n) on x.k = d."Key"
where d."EntityStatus" = 'Active'
on conflict ("TranslatableId","Language") where "EntityStatus" = 'Active' do update set "Name" = excluded."Name";
`;
}

function usersSql() {
  const rows = [
    ...people.SELLERS.map(([u, f, l, city, bio]) => [u, f, l, city, bio, 900]),
    ...people.BUYERS.map(([u, f, l, city]) => [u, f, l, city, null, 600]),
  ].map(([u, f, l, city, bio, maxDays]) =>
    `(${sql(f)}, ${sql(l)}, '', ${sql(`${u}@${people.SEED_EMAIL_DOMAIN}`)}, pg_temp.lookup('Cities', ${sql(city)}), ${ts(daysAgo(randInt(0, 10)))}, ${ts(daysAgo(randInt(200, maxDays)))}, ${sql(bio)}, ${sql(u)}, true, false, 'Active', 'Active')`,
  );

  return `
-- ============================================================ 3. Демо-користувачі
insert into "Users"("FirstName","LastName","PasswordHash","Email","CityId","LastSeenAt","RegisteredAt","Bio","UserName","EmailConfirmed","PhoneConfirmed","Status","EntityStatus")
values
  ${rows.join(",\n  ")}
on conflict ("Email") where "Email" is not null do update set
  "FirstName" = excluded."FirstName", "LastName" = excluded."LastName", "CityId" = excluded."CityId", "Bio" = excluded."Bio";

-- попереднє демо-наповнення видаляється (каскадом — варіанти, фото, відгуки, обране)
delete from "Advertisements" where pg_temp.is_seed("OwnerId");
delete from "ReviewRepliComments" where pg_temp.is_seed("OwnerId");
delete from "ReviewComments" where pg_temp.is_seed("OwnerId");
`;
}

function sellerFor(categoryCode, index) {
  const group = Object.entries(CATEGORY_GROUPS).find(([, codes]) => codes.includes(categoryCode))?.[0];
  if (!group) fail(`категорія ${categoryCode} не належить жодній групі`);
  const primary = people.SELLER_BY_PRODUCT_TYPE[group];
  return index % 3 === 2 ? people.SECONDARY_SELLER[primary] : primary;
}

function productsSql(categories) {
  const out = ["\n-- ============================================================ 4. Товари"];
  const notSeed = `not pg_temp.is_seed(a."OwnerId")`;

  for (const c of categories) {
    out.push(`\n-- ---------- ${c.code}`);
    c.products.forEach((product, index) => {
      const condition = weighted([["new", 78], ["like_new", 7], ["refurbished", 7], ["excellent", 4], ["good", 4]]);
      const attrs = jsonSql(c, Object.entries({ brand: product.b, ...product.a }));
      const variants = variantsValuesSql(c, product);
      const popularity = randInt(5, 450);
      const title = sql(product.t);

      // наявний товар з такою назвою (не демо) — оновлюється, фото зберігаються
      out.push(`update "Advertisements" a set "Description" = ${sql(describe(c, product, "new"))}, "Attributes" = ${attrs},
  "CurrencyId" = pg_temp.lookup('Currencies', 'UAH'), "ConditionId" = pg_temp.lookup('Conditions', 'new'),
  "StatusId" = pg_temp.lookup('Statuses', 'published'), "Popularity" = ${popularity}
where a."Title" = ${title} and ${notSeed};
delete from "AdvertisementVariants" v using "Advertisements" a where v."AdvertisementId" = a."Id" and a."Title" = ${title} and ${notSeed};
insert into "AdvertisementVariants"("AdvertisementId","Price","PreviousPrice","Stock","Attributes","EntityStatus")
select a."Id", x.p, x.pp, x.s, x.j, 'Active' from "Advertisements" a, (values
    ${variants}
) x(p, pp, s, j) where a."Title" = ${title} and ${notSeed};`);

      // інакше — новий товар демо-продавця з фото інших товарів категорії
      out.push(`with a as (
  insert into "Advertisements"("Title","Description","Popularity","CurrencyId","CategoryId","OwnerId","CreatedAt","Attributes","ConditionId","StatusId","EntityStatus")
  select ${title}, ${sql(describe(c, product, condition))}, ${popularity}, pg_temp.lookup('Currencies', 'UAH'), pg_temp.cat(${sql(c.code)}),
    pg_temp.uid(${sql(sellerFor(c.code, index))}), ${ts(daysAgo(randInt(3, 300)))}, ${attrs},
    pg_temp.lookup('Conditions', ${sql(condition)}), pg_temp.lookup('Statuses', 'published'), 'Active'
  where pg_temp.adv_id(${title}) is null
  returning "Id", "CategoryId"
), v as (
  insert into "AdvertisementVariants"("AdvertisementId","Price","PreviousPrice","Stock","Attributes","EntityStatus")
  select a."Id", x.p, x.pp, x.s, x.j, 'Active' from a, (values
    ${variants}
  ) x(p, pp, s, j)
)
insert into "AdvertisementImages"("Order","Url","PublicId","OwnerId","EntityStatus")
select (row_number() over ())::int - 1, i."Url", 'seed-shared/' || i."PublicId", a."Id", 'Active'
from a, lateral (
  select ii."Url", ii."PublicId" from "AdvertisementImages" ii
  join "Advertisements" aa on aa."Id" = ii."OwnerId"
  where aa."CategoryId" = a."CategoryId" and ii."EntityStatus" = 'Active' and ii."PublicId" not like 'seed-shared/%'
  order by md5(ii."Id"::text || ${title}) limit 3
) i;`);
    });
  }
  return out.join("\n");
}

function cleanupSql() {
  const hexRows = Object.entries(HEX_TO_COLOR).map(([hex, code]) => `(${sql(hex)}, ${sql(code)})`).join(", ");
  return `
-- ============================================================ 5. Приведення інших товарів до нової схеми
-- старі кольори-рядки (#000000) → значення довідника color
update "AdvertisementVariants" v set "Attributes" = jsonb_set(v."Attributes", '{color}', to_jsonb(pg_temp.dv('color', m.code)))
from (values ${hexRows}) m(hex, code)
where v."Attributes"->>'color' = m.hex;

-- атрибути товару — в оголошення, варіантні — у варіанти; невідомі ключі прибираються
with def as (select "CategoryId" cat, "Key" k, "IsVariant" iv, "Type" t from "AttributeDefinitions" where "EntityStatus" = 'Active')
update "Advertisements" a set "Attributes" = coalesce((
  select jsonb_object_agg(e.key, e.value)
  from jsonb_each(a."Attributes" || coalesce((select v."Attributes" from "AdvertisementVariants" v where v."AdvertisementId" = a."Id" order by v."Id" limit 1), '{}'::jsonb)) e
  join def on def.cat = a."CategoryId" and def.k = e.key and not def.iv
  where def.t <> 'Select' or jsonb_typeof(e.value) in ('number', 'array')
), '{}'::jsonb);

with def as (select "CategoryId" cat, "Key" k, "IsVariant" iv, "Type" t from "AttributeDefinitions" where "EntityStatus" = 'Active')
update "AdvertisementVariants" v set "Attributes" = coalesce((
  select jsonb_object_agg(e.key, e.value)
  from jsonb_each(v."Attributes") e
  join "Advertisements" a on a."Id" = v."AdvertisementId"
  join def on def.cat = a."CategoryId" and def.k = e.key and def.iv
  where def.t <> 'Select' or jsonb_typeof(e.value) in ('number', 'array')
), '{}'::jsonb);

-- варіанти, що після чистки стали однаковими
delete from "AdvertisementVariants" v using "AdvertisementVariants" w
where v."AdvertisementId" = w."AdvertisementId" and v."Attributes" = w."Attributes" and v."Id" > w."Id";
`;
}

function footer() {
  return `
-- ============================================================ Перевірки
do $$
begin
  if exists (select 1 from "Advertisements" a, jsonb_each(a."Attributes") e where e.value = 'null'::jsonb)
     or exists (select 1 from "AdvertisementVariants" v, jsonb_each(v."Attributes") e where e.value = 'null'::jsonb) then
    raise exception 'Seed: у JSON атрибутів залишились null (не знайдено значення довідника)';
  end if;
  if exists (select 1 from "Advertisements" a where not exists (select 1 from "AdvertisementVariants" v where v."AdvertisementId" = a."Id" and v."EntityStatus" = 'Active')) then
    raise exception 'Seed: є товари без жодного варіанта';
  end if;
end $$;

select
  (select count(*) from "Advertisements" where "EntityStatus" = 'Active') as advertisements,
  (select count(*) from "AdvertisementVariants" where "EntityStatus" = 'Active') as variants,
  (select count(*) from "AdvertisementImages" where "EntityStatus" = 'Active') as images,
  (select count(*) from "ReviewComments" where "EntityStatus" = 'Active') as reviews,
  (select count(*) from "ReviewRepliComments" where "EntityStatus" = 'Active') as replies,
  (select count(*) from "Users" where "Email" like '%@${people.SEED_EMAIL_DOMAIN}') as demo_users;

commit;
`;
}

// ---------------------------------------------------------------- main

const categories = prepareCatalog();
const reviews = categories.flatMap((c) => c.products.flatMap((p) => reviewsSql(c, p)));

const output = [
  header(),
  dictionariesSql(categories),
  definitionsSql(categories),
  usersSql(),
  productsSql(categories),
  cleanupSql(),
  "\n-- ============================================================ 6. Відгуки\n" + reviews.join("\n"),
  footer(),
].join("\n");

const target = path.join(path.dirname(fileURLToPath(import.meta.url)), "seed.sql");
fs.writeFileSync(target, output, "utf8");

const productCount = categories.reduce((n, c) => n + c.products.length, 0);
const variantCount = categories.reduce((n, c) => n + c.products.reduce((m, p) => m + expandVariants(p).length, 0), 0);
console.log(`seed.sql: ${categories.length} категорій, ${productCount} товарів, ${variantCount} варіантів, ${reviews.length} відгуків`);
