import { Modal, Text } from "@mantine/core";
import ChangePasswordForm from "./ChangePasswordForm";

type Props = {
  opened: boolean;
  onClose: () => void;
};

export default function ChangePasswordModal({ opened, onClose }: Props) {
  return (
    <Modal
      opened={opened}
      onClose={onClose}
      title={
        <Text fz={20} fw={600}>
          Зміна пароля
        </Text>
      }
      centered
      radius="lg"
      size="md"
      padding="xl"
      zIndex={1100}
    >
      <Text fz={14} c="dimmed" mb="md">
        Після збереження ми надішлемо код підтвердження на вашу пошту.
      </Text>
      <ChangePasswordForm onCancel={onClose} onSuccess={onClose} />
    </Modal>
  );
}
