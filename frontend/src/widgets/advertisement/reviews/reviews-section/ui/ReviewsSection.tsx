import { useState } from "react";
import {
  Tabs,
  Text,
  Checkbox,
  Avatar,
  Rating,
  Button,
  UnstyledButton,
} from "@mantine/core";
import { ChevronUp, ThumbsUp, ThumbsDown, Star } from "lucide-react";
import styles from "./ReviewsSection.module.css";

// Тестові дані
interface Review {
  id: string;
  authorName: string;
  authorAvatar: string;
  rating: number;
  text: string;
  date: string;
  likes: number;
  dislikes: number;
  hasMedia: boolean;
}

const MOCK_REVIEWS: Review[] = [
  {
    id: "1",
    authorName: "Darrell Steward",
    authorAvatar:
      "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-1.png",
    rating: 5,
    text: "This is amazing product I have.",
    date: "July 2, 2020 03:29 PM",
    likes: 128,
    dislikes: 0,
    hasMedia: true,
  },
  {
    id: "2",
    authorName: "Darlene Robertson",
    authorAvatar:
      "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-2.png",
    rating: 5,
    text: "This is amazing product I have.",
    date: "July 2, 2020 1:04 PM",
    likes: 82,
    dislikes: 0,
    hasMedia: false,
  },
  {
    id: "3",
    authorName: "Kathryn Murphy",
    authorAvatar:
      "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-3.png",
    rating: 5,
    text: "This is amazing product I have.",
    date: "June 26, 2020 10:03 PM",
    likes: 9,
    dislikes: 0,
    hasMedia: true,
  },
  {
    id: "4",
    authorName: "Ronald Richards",
    authorAvatar:
      "https://raw.githubusercontent.com/mantinedev/mantine/master/.demo/avatars/avatar-4.png",
    rating: 4,
    text: "This is amazing product I have.",
    date: "July 7, 2020 10:14 AM",
    likes: 124,
    dislikes: 0,
    hasMedia: false,
  },
];

export default function ReviewsSection() {
  const [activeTab, setActiveTab] = useState<string | null>("all");
  const [selectedRatings, setSelectedRatings] = useState<number[]>([]);
  const [isRatingOpen, setIsRatingOpen] = useState(true);

  // Фільтрація відгуків
  const filteredReviews = MOCK_REVIEWS.filter((review) => {
    // Фільтр за табами
    if (activeTab === "media" && !review.hasMedia) return false;
    if (activeTab === "text" && !review.text) return false;

    // Фільтр за чекбоксами оцінок
    if (
      selectedRatings.length > 0 &&
      !selectedRatings.includes(review.rating)
    ) {
      return false;
    }

    return true;
  });

  const toggleRatingFilter = (rating: number) => {
    setSelectedRatings((prev) =>
      prev.includes(rating)
        ? prev.filter((r) => r !== rating)
        : [...prev, rating],
    );
  };

  return (
    <div className={styles.container}>
      {/* Ліва частина — Фільтр */}
      <aside className={styles.filterSidebar}>
        <Text fw={700} size="lg" c="#0f172a">
          Фільтр відгуків
        </Text>

        <hr className={styles.divider} />

        <div>
          <UnstyledButton
            className={styles.filterHeader}
            onClick={() => setIsRatingOpen(!isRatingOpen)}
            w="100%"
          >
            <Text fw={600} size="sm" c="#475569">
              Оцінка
            </Text>
            <ChevronUp
              size={16}
              style={{
                transform: isRatingOpen ? "rotate(0deg)" : "rotate(180deg)",
                transition: "transform 0.2s ease",
              }}
            />
          </UnstyledButton>

          {isRatingOpen && (
            <div className={styles.filterList} style={{ marginTop: 12 }}>
              {[5, 4, 3, 2, 1].map((stars) => (
                <Checkbox
                  key={stars}
                  checked={selectedRatings.includes(stars)}
                  onChange={() => toggleRatingFilter(stars)}
                  label={
                    <span className={styles.ratingLabel}>
                      <Star size={14} fill="#fea945" color="#fea945" />
                      <Text size="sm" c="#334155">
                        {stars}
                      </Text>
                    </span>
                  }
                />
              ))}
            </div>
          )}
        </div>

        <hr className={styles.divider} />
      </aside>

      {/* Права частина — Список відгуків */}
      <main className={styles.reviewsContent}>
        <Text fw={700} size="xl" c="#0f172a">
          Відгуки покупців
        </Text>

        <Tabs value={activeTab} onChange={setActiveTab} variant="outline">
          <Tabs.List className={styles.tabsList}>
            <Tabs.Tab value="all">Всі відгуки</Tabs.Tab>
            <Tabs.Tab value="media">З фото та відео</Tabs.Tab>
            <Tabs.Tab value="text">З описом</Tabs.Tab>
          </Tabs.List>

          <Tabs.Panel value={activeTab || "all"} pt={20}>
            <div className={styles.reviewsList}>
              {filteredReviews.map((review) => (
                <div key={review.id} className={styles.reviewCard}>
                  <div className={styles.cardHeader}>
                    <Rating value={review.rating} readOnly color="#fea945" />
                  </div>

                  <div className={styles.cardBody}>
                    <Text fw={600} size="sm" c="#0f172a">
                      {review.text}
                    </Text>
                    <Text size="xs" c="#94a3b8">
                      {review.date}
                    </Text>
                  </div>

                  <div className={styles.cardFooter}>
                    <div className={styles.author}>
                      <Avatar src={review.authorAvatar} radius="xl" size="sm" />
                      <Text fw={500} size="sm" c="#334155">
                        {review.authorName}
                      </Text>
                    </div>

                    <div className={styles.actions}>
                      <Button
                        variant="default"
                        size="xs"
                        leftSection={<ThumbsUp size={14} />}
                        radius="md"
                      >
                        {review.likes > 0 && review.likes}
                      </Button>
                      <Button variant="default" size="xs" radius="md" px={8}>
                        <ThumbsDown size={14} />
                      </Button>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </Tabs.Panel>
        </Tabs>
      </main>
    </div>
  );
}
