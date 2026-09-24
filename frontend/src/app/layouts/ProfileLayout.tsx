import { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Container from '@/shared/ui/container';
import { ProfileTabs } from '@/widgets/profile-tabs';
import { ProfileHeader } from '@/widgets/profile-header';
import styles from './ProfileLayout.module.css';
import { Text } from '@mantine/core';

export type ProfileContextType = {
    setOrientation: (orientation: 'row' | 'column') => void;
};

export default function ProfileLayout() {
    const [orientation, setOrientation] = useState<'row' | 'column'>('column');

    return (
      <div className={styles.profileLayout}>
        <Container>
          <div className={styles.content}>
            <Text fw={700} fz={40} c={"var(--color-base)"}>
              Мій профіль
            </Text>
            <ProfileTabs />
            <div className={`${styles.pageLayout} ${styles[orientation]}`}>
              <ProfileHeader
                orientation={orientation === "row" ? "vertical" : "horizontal"}
              />
              <Outlet
                context={{ setOrientation } satisfies ProfileContextType}
              />
            </div>
          </div>
        </Container>
      </div>
    );
}