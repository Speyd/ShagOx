import { useMutation } from "@tanstack/react-query";
import { changeEmail, changePhone } from "../../api/api";
import type { ContactKind } from "../schemas/changeContactSchema";

export function useChangeContact(kind: ContactKind) {
  return useMutation({
    mutationFn: (value: string) =>
      kind === "email"
        ? changeEmail({ email: value })
        : changePhone({ phone: value }),
  });
}
