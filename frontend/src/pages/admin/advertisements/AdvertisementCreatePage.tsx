import { useMemo, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useDebouncedValue } from "@mantine/hooks";
import { Select, Text } from "@mantine/core";
import { UserRound } from "lucide-react";
import { useAdminList } from "@/shared/api/crudHooks";
import type { User } from "@/shared/lib/types/user";
import Button from "@/shared/ui/buttons/button";
import { FormField } from "@/shared/ui/form";
import {
  CreateAdvertisementForm,
  type CreateAdvertisementPayload,
} from "@/features/advertisement/create-advertisement";
import { useAdminCreateAdvertisement } from "@/features/admin/advertisement/create-advertisement";
import { useGetUser } from "@/entities/user/model/hooks/useGetUser";
import { AdminPageHeader } from "@/widgets/admin/page-header";
import styles from "../AdminPages.module.css";

const FORM_ID = "admin-create-advertisement";

export default function AdvertisementCreatePage() {
  const navigate = useNavigate();
  const [params] = useSearchParams();
  const presetSellerId = params.get("sellerId");

  const [sellerId, setSellerId] = useState<string | null>(presetSellerId);
  const [sellerError, setSellerError] = useState<string>();
  const [search, setSearch] = useState("");
  const [debouncedSearch] = useDebouncedValue(
    search.split(" · ")[0].replace(/^@/, "").trim(),
    300,
  );

  const { data: users } = useAdminList<User>("/admin/users", {
    pageSize: 20,
    filters: { UserName: debouncedSearch },
  });
  const { data: presetSeller } = useGetUser(Number(presetSellerId));

  const sellerOptions = useMemo(() => {
    const list = [...(users?.items ?? [])];
    if (presetSeller && !list.some((u) => u.id === presetSeller.id)) list.unshift(presetSeller);
    return list.map((user) => ({
      value: String(user.id),
      label: `@${user.userName}${user.email ? ` · ${user.email}` : ""}`,
    }));
  }, [users, presetSeller]);

  const create = useAdminCreateAdvertisement();

  const handleSubmit = (payload: CreateAdvertisementPayload) => {
    if (!sellerId) {
      setSellerError("Оберіть продавця, від імені якого створюється оголошення");
      window.scrollTo({ top: 0, behavior: "smooth" });
      return;
    }
    create.mutate(
      { sellerId: Number(sellerId), payload },
      { onSuccess: ({ id }) => navigate(`/admin/advertisements/${id}`) },
    );
  };

  return (
    <div className={styles.page}>
      <AdminPageHeader
        title="Нове оголошення"
        description="Створення товару від імені користувача: характеристики, варіанти та фото"
        backTo="/admin/advertisements"
        backLabel="До оголошень"
        actions={
          <>
            <Button variant="ghost" onClick={() => navigate(-1)} disabled={create.isPending}>
              Скасувати
            </Button>
            <Button type="submit" form={FORM_ID} loading={create.isPending}>
              Опублікувати
            </Button>
          </>
        }
      />

      <section className={styles.card}>
        <div className={styles.row}>
          <UserRound size={18} color="var(--color-primary)" />
          <Text fw={700} c="var(--color-base)">
            Продавець
          </Text>
        </div>
        <FormField
          label="Користувач"
          required
          error={sellerError}
          hint="Почніть вводити нікнейм для пошуку"
        >
          <Select
            data={sellerOptions}
            value={sellerId}
            onChange={(value) => {
              setSellerId(value);
              setSellerError(undefined);
            }}
            searchable
            searchValue={search}
            onSearchChange={setSearch}
            placeholder="Оберіть продавця"
            nothingFoundMessage="Користувачів не знайдено"
            clearable
            radius="md"
            maw={480}
            error={Boolean(sellerError)}
          />
        </FormField>
      </section>

      <CreateAdvertisementForm formId={FORM_ID} onSubmit={handleSubmit} />
    </div>
  );
}
