import { Skeleton, Card, Group } from "@mantine/core";

export default function AdvertisementCardSkeleton() {
  return (
    <Card shadow="sm" padding="lg" radius="md" withBorder>
      <Skeleton height={200} mb="xl" />

      <Skeleton height={20} radius="xl" mb="sm" />
      <Skeleton height={20} radius="xl" width="70%" mb="xl" />

      <Group justify="space-between" mt="md" mb="xs">
        <Skeleton height={24} width="30%" radius="xl" />

        <Skeleton height={24} width="20%" radius="xl" />
      </Group>
    </Card>
  );
}
