import styles from "./HomePage.module.css";
import { Hero } from "@/widgets/home/hero";
import { WhyMarketly } from "@/widgets/home/why-marketly";

import { CategoriesBlock } from "@/widgets/home/categories-block";
import { NewTechReleases } from "@/widgets/home/new-tech-releases";
import { PromoBlocks } from "@/widgets/home/promo-blocks";
import { SubscriptionBanner } from "@/widgets/home/subscription-banner";

export default function HomePage() {
  return (
    <div className={styles.homePage}>
      <Hero />
      <CategoriesBlock />
      <NewTechReleases />
      <PromoBlocks />
      <SubscriptionBanner />
      <WhyMarketly />
    </div>
  );
}
