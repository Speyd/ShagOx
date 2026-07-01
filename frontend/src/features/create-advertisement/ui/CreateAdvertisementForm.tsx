import { useState } from "react";

import { useCreateAdvertisement } from "../hooks/useCreateAdvertisement";

export function CreateAdvertisementForm() {
  const mutation = useCreateAdvertisement();

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    mutation.mutate({
      title,
      description,

      price: 100,
      previousPrice: 120,

      currencyId: 0,
      categoryId: 1,

      images: [],

      properties: {
        Brand: "Apple",
      },
    });
  }

  return (
    <form onSubmit={handleSubmit}>
      <input
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Title"
      />

      <textarea
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder="Description"
      />

      <button type="submit" disabled={mutation.isPending}>
        Create
      </button>
    </form>
  );
}
