import { useState, useMemo } from "react";
import { Text } from "@mantine/core";
import styles from "./AdvertisementInfo.module.css";
import { Star } from "lucide-react";
import { Link } from "react-router-dom";
import Price from "@/shared/ui/data-display/price";
import {
  type Advertisement,
  type AdvertisementVariant,
  getVariantAttributeDisplayValue,
} from "@/shared/lib/types/advertisements";
import CountBlock from "@/shared/ui/data-display/count-block";
import Button from "@/shared/ui/buttons/button";
import { useBasketAction } from "@/features/basket/model/hooks/useBasketAction";
import { useCompareStore } from "@/entities/compare/model/compareStore";

type AdvertisementInfoProps = {
  advertisement: Advertisement;
};

export default function AdvertisementInfo({
  advertisement,
}: AdvertisementInfoProps) {
  const compareList = useCompareStore((state) => state.compareList);
  const addAdvertisement = useCompareStore((state) => state.addAdvertisement);
  const removeAdvertisement = useCompareStore(
    (state) => state.removeAdvertisement,
  );

  const isInCompare = useMemo(
    () => compareList.some((item) => item.id === advertisement.id),
    [compareList, advertisement.id],
  );

  const handleCompareClick = () => {
    if (isInCompare) {
      removeAdvertisement(advertisement.id);
    } else {
      addAdvertisement(advertisement);
    }
  };

  const isColorValue = (value: string) => /^#([0-9A-F]{3}){1,2}$/i.test(value);

  const availableAttributes = useMemo(() => {
    const map = new Map<string, { label: string; values: string[] }>();

    advertisement.variants?.forEach((variant) => {
      variant.attributes.forEach((attr) => {
        const key = attr.attribute.key;
        if (key === "brand") return;

        const displayLabel = attr.attribute.label || key;
        const valText = getVariantAttributeDisplayValue(attr);

        if (!valText) return;

        if (!map.has(key)) {
          map.set(key, { label: displayLabel, values: [] });
        }

        const group = map.get(key)!;
        if (!group.values.includes(valText)) {
          group.values.push(valText);
        }
      });
    });

    return Array.from(map.entries()).filter(
      ([_, group]) => group.values.length > 0,
    );
  }, [advertisement.variants]);

  const [selectedAttributes, setSelectedAttributes] = useState<
    Record<string, string>
  >(() => {
    const initial: Record<string, string> = {};
    const firstVariant = advertisement.variants?.[0];
    if (firstVariant) {
      firstVariant.attributes.forEach((attr) => {
        const key = attr.attribute.key;
        if (key !== "brand") {
          initial[key] = getVariantAttributeDisplayValue(attr);
        }
      });
    }
    return initial;
  });

  const [quantity, setQuantity] = useState(1);

  const selectedVariant: AdvertisementVariant | undefined = useMemo(() => {
    if (!advertisement.variants?.length) return undefined;

    const matched = advertisement.variants.find((variant) =>
      variant.attributes.every((attr) => {
        const key = attr.attribute.key;
        if (key === "brand") return true;
        const val = getVariantAttributeDisplayValue(attr);
        return selectedAttributes[key] === val;
      }),
    );

    return matched || advertisement.variants[0];
  }, [advertisement.variants, selectedAttributes]);

  const handleSelectAttribute = (key: string, value: string) => {
    setSelectedAttributes((prev) => ({
      ...prev,
      [key]: value,
    }));
    setQuantity(1);
  };

  const isOptionAvailable = (key: string, value: string) => {
    const testState = { ...selectedAttributes, [key]: value };

    return advertisement.variants?.some((variant) =>
      Object.entries(testState).every(([k, v]) => {
        const attr = variant.attributes.find((a) => a.attribute.key === k);
        return !attr || getVariantAttributeDisplayValue(attr) === v;
      }),
    );
  };

  const stock = selectedVariant?.stock ?? 0;
  const currentPrice = selectedVariant?.price ?? 0;

  const { handleAdd, isLoading } = useBasketAction(
    selectedVariant?.id,
    quantity,
    stock,
  );

  return (
    <div className={styles.wrapper}>
      <Text fw={400} fz={40}>
        {advertisement.title}
      </Text>

      <Text fw={400} fz={15}>
        {advertisement.description}
      </Text>

      <div className={styles.ratingContainer}>
        <div className={styles.rating}>
          {[...Array(5)].map((_, index) => (
            <Star
              key={index}
              size={30}
              fill="var(--color-base)"
              color="var(--color-base)"
            />
          ))}
          <Link
            to="/reviews"
            style={{ textDecoration: "none", color: "inherit" }}
          >
            <Text fz={15} c="var(--color-primary)">
              (367)
            </Text>
          </Link>
        </div>
      </div>

      <div className="divider" />

      <Price price={currentPrice} currency={advertisement.currency.symbol} />

      <div className="divider" />

      {availableAttributes.length > 0 && (
        <div className={styles.variantsSection}>
          {availableAttributes.map(([key, group]) => (
            <div key={key} className={styles.variantGroup}>
              <Text fw={500} fz={15} className={styles.variantTitle}>
                {group.label}:
              </Text>
              <div className={styles.variantChips}>
                {group.values.map((value) => {
                  const isSelected = selectedAttributes[key] === value;
                  const isColor = key === "color" || isColorValue(value);

                  if (isColor) {
                    return (
                      <button
                        key={value}
                        type="button"
                        aria-label={`Color ${value}`}
                        disabled={!isOptionAvailable(key, value)}
                        className={`${styles.colorSwatch} ${
                          isSelected ? styles.colorSwatchActive : ""
                        }`}
                        onClick={() => handleSelectAttribute(key, value)}
                      >
                        <span
                          className={styles.colorInner}
                          style={{ backgroundColor: value }}
                        />
                      </button>
                    );
                  }

                  return (
                    <button
                      key={value}
                      type="button"
                      disabled={!isOptionAvailable(key, value)}
                      className={`${styles.chip} ${
                        isSelected ? styles.chipActive : ""
                      }`}
                      onClick={() => handleSelectAttribute(key, value)}
                    >
                      {value}
                    </button>
                  );
                })}
              </div>

              <div className="divider" />
            </div>
          ))}
        </div>
      )}

      {advertisement.attributes?.length > 0 && (
        <>
          <div className="divider" />
          <div className={styles.properties}>
            {advertisement.attributes.map((attr) => (
              <div className={styles.property} key={attr.attribute.id}>
                <Text fw={400} fz={15} c="dimmed">
                  {attr.attribute.label || attr.attribute.key}:
                </Text>
                <Text fw={600} fz={17}>
                  {getVariantAttributeDisplayValue(attr)}
                </Text>
              </div>
            ))}
          </div>
        </>
      )}

      <div className="divider" />

      <div className={styles.countContainer}>
        <CountBlock value={quantity} onChange={setQuantity} max={stock} />
        {stock <= 20 && stock > 0 && (
          <Text fz={14} fw={400}>
            Залишилось всього <b>{stock} штук!</b>
            <br /> встигни придбати
          </Text>
        )}
      </div>

      <div className={styles.buttonsContainer}>
        <Button
          className={styles.actionButton}
          onClick={handleAdd}
          disabled={isLoading || stock === 0 || !selectedVariant}
        >
          {isLoading ? "Додаємо..." : "Купити"}
        </Button>
        <Button className={styles.actionButton} onClick={handleCompareClick}>
          {isInCompare ? "У порівнянні" : "Порівняти"}
        </Button>
      </div>
    </div>
  );
}
