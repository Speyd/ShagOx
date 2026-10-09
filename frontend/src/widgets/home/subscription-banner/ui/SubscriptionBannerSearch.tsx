import Input from '@/shared/ui/input'
import styles from './SubscriptionBannerSearch.module.css'
import Button from '@/shared/ui/button'
import { ArrowRight } from 'lucide-react'

export default function SubscriptionBannerSearch() {
    return (
        <div className={styles.inputWrapper}>
            <Input placeholder='Ваш e-mail' className={styles.input} />
            <Button className={styles.button}><ArrowRight /></Button>
        </div>
    )
}

