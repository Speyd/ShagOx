import { useAuthStore } from "@/features/auth/store/useAuthStore";
import styles from "./ProfileHeader.module.css";
import { Button, Image, Text } from "@mantine/core";
import { Link, Mail, MapPin } from "lucide-react";
import { useDisclosure } from "@mantine/hooks";
import ProfileDrawer from "./ProfileDrawer";
import ChannelDrawer from "./ChannelDrawer";

type ProfileHeaderProps = {
    orientation?: "horizontal" | "vertical";
};

export default function ProfileHeader({ orientation = "horizontal" }: ProfileHeaderProps) {
    const user = useAuthStore((state) => state.user);

    const [userProfileOpened, { open: openUserProfile, close: closeUserProfile }] = useDisclosure(false);
    const [userChannelOpened, { open: openUserChannel, close: closeUserChannel }] = useDisclosure(false);

    return (
        <section className={styles.profileHeader} data-orientation={orientation}>
            <div className={styles.avatarBlock}>
                <Image
                    src={user?.avatar?.url}
                    alt={user?.firstName || user?.userName || "Аватар користувача"}
                    w={170}
                    h={170}
                    radius={100}
                    fallbackSrc="https://placehold.co/170x170?text=Avatar"
                />
            </div>

            <div className={styles.content}>
                <div className={styles.leftBlock}>
                    <div>
                        <Text fw={700} fz={24}>
                            {user?.firstName && user?.lastName
                                ? `${user.firstName} ${user.lastName}`
                                : user?.firstName || user?.lastName || user?.userName || "Користувач"}
                        </Text>
                        <Text fw={500} fz={12} c="dimmed">
                            @{user?.userName}
                        </Text>
                    </div>

                    <Text fw={500} fz={15}>
                        {user?.bio || ""}
                    </Text>

                    <div className={styles.statsBlock}>
                        <div className={styles.statItem}>
                            <Text fw={700} fz={15}>47</Text>
                            <Text fw={500} fz={12} c="dimmed">Відео</Text>
                        </div>
                        <div className={styles.statItem}>
                            <Text fw={700} fz={15}>12 тис.</Text>
                            <Text fw={500} fz={12} c="dimmed">Підписників</Text>
                        </div>
                        <div className={styles.statItem}>
                            <Text fw={700} fz={15}>128 тис.</Text>
                            <Text fw={500} fz={12} c="dimmed">Переглядів</Text>
                        </div>
                        <div className={styles.statItem}>
                            <Text fw={500} fz={12} c="dimmed">На платформі</Text>
                            <Text fw={700} fz={15}>з 12.10.24</Text>
                        </div>
                    </div>
                </div>

                <div className={styles.rightBlock}>
                    <div className={styles.buttonsBlock}>
                        <Button bg="var(--color-base)" fz={15} className={styles.editButton} onClick={openUserProfile}>
                            Редагувати профіль
                        </Button>
                        <Button bg="white" c="black" fz={15} className={styles.channelButton} onClick={openUserChannel}>
                            Налаштувати канал
                        </Button>
                    </div>

                    <div className={styles.infoBlock}>
                        {user?.city?.name && (
                            <div className={styles.infoItem}>
                                <MapPin size={14} strokeWidth={2.5} />
                                <Text fw={500} fz={12}>{user.city.name}</Text>
                            </div>
                        )}
                        <div className={styles.infoItem}>
                            <Link size={14} strokeWidth={2.5} />
                            <Text fw={500} fz={12}>Посилання</Text>
                        </div>
                        {user?.email && (
                            <div className={styles.infoItem}>
                                <Mail size={14} strokeWidth={2.5} />
                                <Text fw={500} fz={12}>{user.email}</Text>
                            </div>
                        )}
                    </div>
                </div>
            </div>

            <ProfileDrawer
                opened={userProfileOpened}
                close={closeUserProfile}
            />

            <ChannelDrawer
                opened={userChannelOpened}
                close={closeUserChannel}
            />
        </section>

    );
}