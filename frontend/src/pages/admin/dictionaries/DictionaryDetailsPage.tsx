import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import type { ColumnDef } from "@tanstack/react-table";
import { Loader, Text } from "@mantine/core";
import { Hash, Layers, ListTree } from "lucide-react";
import { fetchById } from "@/shared/api/crud";
import { getColorHex } from "@/shared/lib/colors";
import type { AdminDictionary, AdminDictionaryValue } from "@/shared/lib/types/admin";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import { InfoTile } from "@/widgets/admin/info-tile";
import { CrudPage, codeField, idColumn } from "@/widgets/admin/crud-page";
import styles from "../AdminPages.module.css";

const columns: ColumnDef<AdminDictionaryValue>[] = [
  idColumn(),
  {
    id: "value",
    header: "Значення",
    accessorFn: (item) => `${item.label ?? ""} ${item.code} ${item.value ?? ""}`,
    cell: ({ row }) => {
      const item = row.original;
      const hex = getColorHex(item.code);
      const isColor = hex !== getColorHex("");
      return (
        <div className={styles.row}>
          {isColor && (
            <span
              aria-hidden
              style={{
                width: 18,
                height: 18,
                borderRadius: 6,
                background: hex,
                border: "1px solid var(--color-border)",
                flexShrink: 0,
              }}
            />
          )}
          <div>
            <Text fw={600} size="sm" c="var(--color-base)">
              {item.label || item.value || item.code}
            </Text>
            <Text size="xs" c="dimmed" ff="monospace">
              {item.code}
            </Text>
          </div>
        </div>
      );
    },
  },
  {
    accessorKey: "value",
    header: "Технічне значення",
    cell: ({ row }) => (
      <Text size="sm" c={row.original.value ? undefined : "dimmed"}>
        {row.original.value || "—"}
      </Text>
    ),
  },
];

export default function DictionaryDetailsPage() {
  const dictionaryId = Number(useParams().id);

  const { data: dictionary, isLoading } = useQuery({
    queryKey: ["admin", "dictionary", dictionaryId],
    queryFn: () => fetchById<AdminDictionary>("/admin/attribute-dictionaries", dictionaryId),
    enabled: Number.isFinite(dictionaryId),
  });

  if (isLoading) return <Loader mx="auto" display="block" mt="xl" />;

  if (!dictionary) {
    return (
      <div className={styles.page}>
        <AdminPageHeader title="Довідник не знайдено" backTo="/admin/dictionaries" />
      </div>
    );
  }

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title={`Довідник «${dictionary.code}»`}
        description="Значення, які продавці обирають у полях типу «Вибір зі списку»"
        backTo="/admin/dictionaries"
        backLabel="До довідників"
      />

      <div className={styles.infoGrid}>
        <InfoTile icon={<Hash size={18} />} label="Код" value={dictionary.code} mono />
        <InfoTile
          icon={<ListTree size={18} />}
          label="Значень"
          value={dictionary.valueIds?.length ?? 0}
        />
        <InfoTile
          icon={<Layers size={18} />}
          label="Використовують атрибути"
          value={dictionary.attributeIds?.length ?? 0}
        />
      </div>

      <section className={styles.card}>
        <CrudPage<AdminDictionaryValue>
          embedded
          title="Значення"
          description="Код — унікальний ідентифікатор, назва перекладається на кожну мову"
          entityName="значення"
          endpoint="/admin/attribute-dictionary-values"
          filters={{ DictionaryId: dictionaryId }}
          searchParam="Code"
          searchPlaceholder="Пошук за кодом..."
          columns={columns}
          pageSize={20}
          fields={[
            codeField("Код", "Напр. black, 256gb"),
            {
              name: "value",
              label: "Технічне значення",
              kind: "text",
              maxLength: 100,
              hint: "Необов'язково: число, HEX-колір тощо",
            },
          ]}
          getDefaults={(item) => ({
            code: item?.code ?? "",
            value: item?.value ?? "",
          })}
          toPayload={(values) => ({
            dictionaryId,
            code: values.code,
            value: values.value ? String(values.value) : null,
          })}
          translations={{
            path: "/admin/attribute-dictionary-values/translations",
            codeParam: "DictionaryValueCode",
            codeOf: (values) => String(values.code),
          }}
        />
      </section>
    </div>
  );
}
