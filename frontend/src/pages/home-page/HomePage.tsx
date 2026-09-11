import styles from "./HomePage.module.css";
import { Hero } from "@/widgets/hero";
import { WhyMarketly } from "@/widgets/why-marketly";
import { SubscriptionBanner } from "@/widgets/subscription-banner";
import { CategoriesBlock } from "@/widgets/categories-block";
import { NewTechReleases } from "@/widgets/new-tech-releases";
import { PromoBlocks } from "@/widgets/promo-blocks";

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
