import { Drawer } from "@mantine/core";
import styles from "./ChannelDrawer.module.css"

type ChannelDrawerProps = {
    opened: boolean;
    close: () => void;
};

export default function ChannelDrawer({ opened, close }: ChannelDrawerProps) {
    return (
        <Drawer opened={opened} onClose={close} title="Налаштувати канал" position="right" className={styles.drawer} zIndex={1001}>
            <div>

            </div>
        </Drawer>
    )
}

