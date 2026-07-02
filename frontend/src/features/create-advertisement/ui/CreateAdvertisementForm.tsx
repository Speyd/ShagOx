import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useCreateAdvertisement } from "../hooks/useCreateAdvertisement";
import {
  createAdvertisementSchema,
  type CreateAdvertisementDto,
} from "../model/schema";
import styles from "./CreateAdvertisementForm.module.css";
import Input from "@/shared/ui/Input";
import TextArea from "@/shared/ui/TextArea";
import Button from "@/shared/ui/Button";

export default function CreateAdvertisementForm() {
  const mutation = useCreateAdvertisement();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateAdvertisementDto>({
    resolver: zodResolver(createAdvertisementSchema),
  });

  const onSubmit = (data: CreateAdvertisementDto) => {
    mutation.mutate({
      ...data,

      previousPrice: data.price,

      currencyId: 1,
      categoryId: 1,

      sellerId: 8,

      images: [],

      properties: {
        Brand: "Apple",
      },
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
      <div className={styles.inputWrapper}>
        <label className={styles.label}>Назва</label>

        <Input {...register("title")} />

        {errors.title && <p className="error">{errors.title.message}</p>}
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Опис</label>

        <TextArea {...register("description")} />

        {errors.description && (
          <p className="error">{errors.description.message}</p>
        )}
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Ціна</label>

        <Input
          type="number"
          {...(register("price"),
          {
            valueAsNumber: true,
          })}
        />

        {errors.price && <p className="error">{errors.price.message}</p>}
      </div>

      <Button type="submit" disabled={mutation.isPending}>
        {mutation.isPending ? "Створюємо..." : "Створити"}
      </Button>
    </form>
  );
}
