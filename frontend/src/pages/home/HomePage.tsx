import styles from "./HomePage.module.css";
import { Hero } from "@/widgets/home/hero";
import { WhyMarketly } from "@/widgets/home/why-marketly";

import { CategoriesBlock } from "@/widgets/home/categories-block";
import { HomeShowcase } from "@/widgets/home/showcase";
import { PromoBlocks } from "@/widgets/home/promo-blocks";
import { SubscriptionBanner } from "@/widgets/home/subscription-banner";

export default function HomePage() {
  return (
    <div className={styles.homePage}>
      <Hero />
      <CategoriesBlock />
      <HomeShowcase title="Новинки" sort="Newest" />
      <PromoBlocks />
      <HomeShowcase title="Зі знижкою" sort="Discount" />
      <SubscriptionBanner />
      <WhyMarketly />
    </div>
  );
}
