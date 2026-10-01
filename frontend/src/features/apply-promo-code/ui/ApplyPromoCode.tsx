import { useState } from "react";
import { Button, Flex, Text } from "@mantine/core";
import Input from "@/shared/ui/input";

import styles from "./ApplyPromoCode.module.css";

export default function ApplyPromoCode() {
  const [promoCode, setPromoCode] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const handleApply = async () => {
    if (!promoCode.trim()) return;

    setIsLoading(true);
    try {
      // TODO: apply promo code
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className={styles.container}>
      <Text
        component="label"
        htmlFor="promo-input"
        fz={14}
        fw={500}
        c="gray.7"
        mb={6}
      >
        Подарункова карта / Промокод
      </Text>

      <Flex gap={12} align="center">
        <Input
          id="promo-input"
          placeholder="Введіть код"
          value={promoCode}
          onChange={(e) => setPromoCode(e.target.value)}
          className={styles.input}
        />

        <Button
          variant="outline"
          color="blue.7"
          radius="md"
          loading={isLoading}
          disabled={!promoCode.trim()}
          onClick={handleApply}
          className={styles.button}
        >
          Застосувати
        </Button>
      </Flex>
    </div>
  );
}
