import styles from "./HomePage.module.css";
import { Hero } from "@/widgets/home/hero";
import { WhyMarketly } from "@/widgets/home/why-marketly";

import { CategoriesBlock } from "@/widgets/home/categories-block";
import { HomeShowcase } from "@/widgets/home/showcase";
import { PromoBlocks } from "@/widgets/home/promo-blocks";
import { SubscriptionBanner } from "@/widgets/home/subscription-banner";

export function HomePage() {
  return (
    <div className={styles.homePage}>
      <Hero />
      <CategoriesBlock />
      <HomeShowcase title="Новинки" newest />
      <PromoBlocks />
      <HomeShowcase title="Зі знижкою" discounts newest />
      <SubscriptionBanner />
      <WhyMarketly />
    </div>
  );
}
