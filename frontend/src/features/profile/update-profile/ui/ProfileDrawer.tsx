import { Drawer } from "@mantine/core";
import { useMediaQuery } from "@mantine/hooks";
import styles from "./ProfileDrawer.module.css";
import ProfileForm from "./ProfileForm";

type ProfileDrawerProps = {
  opened: boolean;
  close: () => void;
};

export default function ProfileDrawer({ opened, close }: ProfileDrawerProps) {
  const isMobile = useMediaQuery("(max-width: 700px)", false, {
    getInitialValueInEffect: false,
  });

  return (
    <Drawer
      opened={opened}
      onClose={close}
      position="right"
      size={isMobile ? "100%" : 660}
      zIndex={1001}
      withCloseButton={false}
      classNames={{
        content: styles.drawerContent,
        body: styles.drawerBody,
      }}
    >
      <ProfileForm close={close} />
    </Drawer>
  );
}
