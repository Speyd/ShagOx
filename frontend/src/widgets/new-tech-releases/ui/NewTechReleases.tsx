import { Text } from "@mantine/core";
import styles from "./NewTechReleases.module.css";
import ReleaseCard from "./ReleaseCard";
import Container from "@/shared/ui/container";
import Arrow from "@/shared/ui/Arrow/Arrow";

const releases = [
    {
        title: "iPhone 15 Pro Max",
        description: "The latest iPhone with Pro Max features.",
        image: "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1788943075/468491a7c6fdd81c946b3d38710b520a33e7b3e2_rw3gcs.png",
        price: 1120,
    },
    {
        title: "Samsung Galaxy S23 Ultra",
        description: "The latest Samsung Galaxy with Ultra features.",
        image: "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1788943073/c4ed186fcebbf4c6861f6d6c9a7d536eaa6aadab_zl1zob.png",
        price: 320,
    },
    {
        title: "Google Pixel 7 Pro",
        description: "The latest Google Pixel with Pro features.",
        image: "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1788943071/9a56dbb652cde537cecdbb60fd8e31eed174680f_yimgde.png",
        price: 5000,
    },
]

export default function NewTechReleases() {
    return (
        <Container>
            <section className={styles.newTechReleases}>
                <div className={styles.header}>
                    <Text fw={700} fz={24}>
                        Новинки техніки
                    </Text>
                    
                        <Arrow to="/" text="Переглянути всі" />
               
                </div>

                <div className={styles.content}>
                    {releases.map((release) => (
                        <ReleaseCard key={release.title} {...release} />
                    ))}
                </div>
            </section>
        </Container>
    )
}
