import Container from '@/shared/ui/container'
import styles from './SubscriptionBanner.module.css'
import { Text } from '@mantine/core'
import SubscriptionBannerSearch from './SubscriptionBannerSearch'

export default function SubscriptionBanner() {
    return (
        <Container>
            <section className={styles.subscriptionBanner}>

                <div className={styles.content}>
                    <div className={styles.text}>
                        <Text fw={700} fz={28}>
                            Весняний розпродаж техніки!
                        </Text>
                        <Text fw={400} fz={14} color="var(--text-secondary)">
                            Підпишіться на новини та отримуйте знижки
                        </Text>
                    </div>
                    <SubscriptionBannerSearch />
                </div>

            </section>
        </Container>
    )
}
