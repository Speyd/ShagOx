import { ChevronRight } from "lucide-react";
import { Image, Text } from "@mantine/core";

import styles from "./ReleaseCard.module.css";

type ReleaseCardProps = {
    title: string;
    description: string;
    image: string;
    price: number;
};

export default function ReleaseCard({
    title,
    description,
    image,
    price,
}: ReleaseCardProps) {
    return (
        <article className={styles.card}>
            <Image
                src={image}
                alt={title}
                className={styles.image}
            />

            <div className={styles.content}>
                <Text className={styles.title} fw={600} fz={22}>
                    {title}
                </Text>

                <Text className={styles.description} fz={16}>
                    {description}
                </Text>

                <div className={styles.footer}>
                    <Text className={styles.price} fz={24}>
                        Від {price.toLocaleString("uk-UA")} ₴
                    </Text>

                    <button className={styles.button}>
                        <ChevronRight size={22} />
                    </button>
                </div>
            </div>
        </article>
    );
}