import { useMutation } from "@tanstack/react-query";
import { disableTwoFactor, enableTwoFactor } from "../../api/api";

export type TwoFactorAction = "enable" | "disable";

export function useRequestTwoFactorChange() {
  return useMutation({
    mutationFn: (action: TwoFactorAction) =>
      action === "enable" ? enableTwoFactor() : disableTwoFactor(),
  });
}
