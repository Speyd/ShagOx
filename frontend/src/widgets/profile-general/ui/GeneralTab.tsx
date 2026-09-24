import { Paper, Tabs, Text } from "@mantine/core";
import { useEffect, useState } from "react";
import styles from "./GeneralTab.module.css"
import { useOutletContext } from "react-router-dom";
import type { ProfileContextType } from "@/app/layouts/ProfileLayout";

export default function GeneralTab() {
    const [activeTab, setActiveTab] = useState<string | null>('videos');
    const { setOrientation } = useOutletContext<ProfileContextType>();

    useEffect(() => {
        setOrientation('column');
    }, [setOrientation]);

    return (
        <Paper p="xl" radius="lg" className={styles.generalTab}>
            <Tabs value={activeTab} onChange={setActiveTab} >
                <Tabs.List className={styles.list}>
                    <Tabs.Tab value="videos" className={styles.tab}>Усі відео</Tabs.Tab>
                    <Tabs.Tab value="playlists" className={styles.tab}>Плейлисти</Tabs.Tab>
                    <Tabs.Tab value="products" className={styles.tab}>Товари</Tabs.Tab>
                </Tabs.List>

                <Tabs.Panel value="videos" pt="xs">
                    <Text mt="md">Тут будуть ваші відео</Text>
                </Tabs.Panel>

                <Tabs.Panel value="playlists" pt="xs">
                    <Text mt="md">Тут будуть ваші плейлисти</Text>
                </Tabs.Panel>

                <Tabs.Panel value="products" pt="xs">
                    <Text mt="md">Тут будуть ваші товари</Text>
                </Tabs.Panel>
            </Tabs>
        </Paper>
    );
}
