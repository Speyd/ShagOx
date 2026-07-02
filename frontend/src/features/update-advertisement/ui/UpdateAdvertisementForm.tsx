import { useForm } from "react-hook-form";
import useUpdateAdvertisement from "../hooks/useUpdateAdvertisement";
import type { UpdateAdvertisementDto } from "../model/types";
import { useGetAdvertisement } from "@/entities/Advertisement/hooks/useGetAdvertisement";
import { useEffect } from "react";
import Button from "@/shared/ui/Button";
import { zodResolver } from "@hookform/resolvers/zod";
import { updateAdvertisementSchema } from "../model/schema";
import styles from "./UpdateAdvertisementForm.module.css";
import Input from "@/shared/ui/Input";
import TextArea from "@/shared/ui/TextArea";

interface UpdateAdvertisementFormProps {
  advertisementId: number;
}

export default function UpdateAdvertisementForm({
  advertisementId,
}: UpdateAdvertisementFormProps) {
  const mutation = useUpdateAdvertisement();

  const { data: advertisement } = useGetAdvertisement(advertisementId);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(updateAdvertisementSchema),
  });

  useEffect(() => {
    if (advertisement) {
      reset({
        title: advertisement.title,
        description: advertisement.description,
        price: advertisement.price,
      });
    }
  }, [advertisement, reset]);

  const onSubmit = (data: UpdateAdvertisementDto) => {
    mutation.mutate({
      id: advertisementId,
      data,
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <div className={styles.inputWrapper}>
        <label className={styles.label}>Title</label>

        <div>
          <Input {...register("title")} placeholder="Enter title" />
          {errors.title && <p className="error">{errors.title.message}</p>}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Description</label>

        <div>
          <TextArea
            {...register("description")}
            placeholder="Enter description"
          />
          {errors.description && (
            <p className="error">{errors.description.message}</p>
          )}
        </div>
      </div>

      <div className={styles.inputWrapper}>
        <label className={styles.label}>Price</label>
        <div>
          <Input
            type="number"
            {...register("price")}
            placeholder="Enter price"
          />
          {errors.price && <p className="error">{errors.price.message}</p>}
        </div>
      </div>

      <Button type="submit" disabled={mutation.isPending}>
        {mutation.isPending ? "Updating..." : "Update advertisement"}
      </Button>
    </form>
  );
}
