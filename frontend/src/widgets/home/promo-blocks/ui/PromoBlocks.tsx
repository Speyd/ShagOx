import { Image } from "@mantine/core"
import styles from "./PromoBlocks.module.css"
import { useNavigate } from "react-router-dom"
import Container from "@/shared/ui/container"

const promo = [
    {
        label: "Знижки до -20%",
        title: "Камери найкращої якості",
        image: "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1788961627/854fee754069713693df4ed25b7eb2584b6f1ed6_wk3aqe.png",
        link: "#",
    },
    {
        label: "Ексклюзивна пропозиція",
        title: "Аксесуари Apple",
        image: "https://res.cloudinary.com/dvq5kvhpy/image/upload/v1788961629/4fb6af7ab51099af3cdef380b96905d10a9f2253_rw9e4x.png",
        link: "#",
    }
]

export default function PromoBlocks() {
    const navigate = useNavigate();
    return (
        <Container>
            <section className={styles.promoBlock}>

                {promo.map((item, index) => (
                    <article className={styles.promoBlockCard} key={index}>

                        <Image src={item.image} alt={item.title} className={styles.image} bdrs={12} />
                        <div className={styles.info}>
                            <div className={styles.labelWrapper}>
                                <p className={styles.label}>{item.label}</p>
                            </div>
                            <h3 className={styles.title}>{item.title}</h3>
                            <button className={styles.button} onClick={() => { navigate(item.link) }}>Переглянути</button>
                        </div>



                    </article>
                ))}


            </section>
        </Container>

    )
}

