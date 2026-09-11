import styles from './CategoriesBlock.module.css';
import Container from '@/shared/ui/container';
import { Divider, Image, Text } from '@mantine/core';
import { Carousel } from '@mantine/carousel';
import { ChevronLeft, ChevronRight } from 'lucide-react';

import smartphone from "@/shared/assets/icons/mobile-phone.png";
import laptop from "@/shared/assets/icons/laptop.png";
import headphones from "@/shared/assets/icons/headphones.png";
import camera from "@/shared/assets/icons/camera.png";
import joystick from "@/shared/assets/icons/joystick.png";
import smartwatch from "@/shared/assets/icons/smartwatch.png";
import monitor from "@/shared/assets/icons/monitor.png";
import tablet from "@/shared/assets/icons/tablet.png";

const categories = [
    {
        title: "Смартфони",
        icon: smartphone,
    },
    {
        title: "Ноутбуки",
        icon: laptop,
    },
    {
        title: "Аудіо",
        icon: headphones,
    },
    {
        title: "Камери",
        icon: camera,
    },
    {
        title: "Геймінг",
        icon: joystick,
    },
    {
        title: "Телевізори",
        icon: monitor,
    },
    {
        title: "Планшети",
        icon: tablet,
    },
    {
        title: "Аксесуари",
        icon: smartwatch,
    },
];

export default function CategoriesBlock() {
    return (
        <Container>
            <section className={styles.categoriesBlock}>
                <Text fw={700} fz={24}>Категорії</Text>

                <Carousel
                    slideSize={{ base: '100%', sm: '50%', md: '33.333333%', lg: '16.666667%' }}
                    slideGap={0}
                    nextControlIcon={<ChevronRight size={24} />}
                    previousControlIcon={<ChevronLeft size={24} />}
                    classNames={{
                        root: styles.carouselRoot,
                        controls: styles.carouselControls,
                        control: styles.carouselControl,
                    }}
                >
                    {categories.map((category, index) => (
                        <Carousel.Slide key={category.title} className={styles.itemContainer}>
                            <div className={styles.categoryItem}>
                                <div className={styles.category}>
                                    <Image
                                        src={category.icon}
                                        alt={category.title}
                                        className={styles.icon}
                                    />
                                    <Text
                                        className={styles.categoryTitle}
                                        fw={600}
                                        fz={16}
                                    >
                                        {category.title}
                                    </Text>
                                </div>
                            </div>
                            {index < categories.length - 1 && (
                                <Divider
                                    orientation="vertical"
                                    h={65}
                                    color="var(--color-primary)"
                                />
                            )}
                        </Carousel.Slide>
                    ))}
                </Carousel>
            </section>
        </Container>
    );
}