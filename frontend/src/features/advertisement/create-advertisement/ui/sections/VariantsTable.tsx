import { useState } from "react";
import {
  ActionIcon,
  Badge,
  Button,
  NumberInput,
  Table,
  Text,
} from "@mantine/core";
import { Trash2 } from "lucide-react";
import { useFormContext } from "react-hook-form";
import type { AttributeInfo } from "@/entities/advertisement/model/types";
import { getOptionLabel } from "../../lib/attributes";
import type { AdvertisementFormValues } from "../../model/schema";
import type { SelectOption, VariantRow } from "../../model/types";
import VariantNumberField from "../fields/VariantNumberField";
import styles from "../CreateAdvertisementForm.module.css";

type VariantsTableProps = {
  rows: VariantRow[];
  variantAttributes: AttributeInfo[];
  optionsByKey: Record<string, SelectOption[]>;
  currencySymbol?: string;
};

export default function VariantsTable({
  rows,
  variantAttributes,
  optionsByKey,
  currencySymbol,
}: VariantsTableProps) {
  const {
    getValues,
    setValue,
    formState: { isSubmitted },
  } = useFormContext<AdvertisementFormValues>();

  const removeRow = (key: string) =>
    setValue(
      "variants",
      getValues("variants").filter((row) => row.key !== key),
    );

  return (
    <div className={styles.stack}>
      <BulkFill
        onApply={(patch) =>
          setValue(
            "variants",
            getValues("variants").map((row) => ({ ...row, ...patch })),
            { shouldValidate: isSubmitted },
          )
        }
      />

      <Table.ScrollContainer minWidth={620}>
        <Table verticalSpacing="sm" highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Варіант</Table.Th>
              <Table.Th w={150}>Ціна *</Table.Th>
              <Table.Th w={150}>Стара ціна</Table.Th>
              <Table.Th w={120}>Кількість *</Table.Th>
              <Table.Th w={44} />
            </Table.Tr>
          </Table.Thead>

          <Table.Tbody>
            {rows.map((row, index) => (
              <Table.Tr key={row.key}>
                <Table.Td>
                  <div className={styles.variantBadges}>
                    {variantAttributes
                      .filter((d) => row.options[d.key] !== undefined)
                      .map((d) => (
                        <Badge key={d.key} variant="light" radius="sm">
                          {getOptionLabel(
                            optionsByKey[d.key],
                            row.options[d.key],
                          )}
                        </Badge>
                      ))}
                  </div>
                </Table.Td>
                <Table.Td>
                  <VariantNumberField
                    index={index}
                    name="price"
                    placeholder="0"
                    rightSection={currencySymbol}
                    hideControls
                  />
                </Table.Td>
                <Table.Td>
                  <VariantNumberField
                    index={index}
                    name="previousPrice"
                    placeholder="—"
                    rightSection={currencySymbol}
                    hideControls
                  />
                </Table.Td>
                <Table.Td>
                  <VariantNumberField index={index} name="stock" />
                </Table.Td>
                <Table.Td>
                  <ActionIcon
                    variant="subtle"
                    color="red"
                    aria-label="Видалити варіант"
                    disabled={rows.length === 1}
                    onClick={() => removeRow(row.key)}
                  >
                    <Trash2 size={16} />
                  </ActionIcon>
                </Table.Td>
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>
      </Table.ScrollContainer>

      <Text fz={12} c="dimmed">
        Варіантів: {rows.length}. Непотрібні комбінації можна видалити.
      </Text>
    </div>
  );
}

type BulkPatch = Partial<Pick<VariantRow, "price" | "stock">>;

/** Швидке заповнення ціни та кількості для всіх варіантів одразу. */
function BulkFill({ onApply }: { onApply: (patch: BulkPatch) => void }) {
  const [price, setPrice] = useState<number | "">("");
  const [stock, setStock] = useState<number | "">("");

  const apply = () => {
    onApply({
      ...(price !== "" && { price }),
      ...(stock !== "" && { stock }),
    });
    setPrice("");
    setStock("");
  };

  return (
    <div className={styles.bulkFill}>
      <NumberInput
        size="sm"
        label="Ціна для всіх"
        w={140}
        min={0}
        allowNegative={false}
        hideControls
        value={price}
        onChange={(value) => setPrice(value === "" ? "" : Number(value))}
      />
      <NumberInput
        size="sm"
        label="Кількість для всіх"
        w={140}
        min={0}
        allowNegative={false}
        allowDecimal={false}
        value={stock}
        onChange={(value) => setStock(value === "" ? "" : Number(value))}
      />
      <Button
        size="sm"
        variant="light"
        disabled={price === "" && stock === ""}
        onClick={apply}
      >
        Застосувати
      </Button>
    </div>
  );
}
