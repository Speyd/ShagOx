import { Group, Paper, SimpleGrid, Skeleton, Stack } from "@mantine/core";
import Container from "@/shared/ui/layout/container";

export function ComparePageSkeleton() {
  return (
    <Container>
      <Stack gap="lg" py="xl">
        <Stack gap="xs">
          <Skeleton height={34} width={280} radius="md" />
          <Skeleton height={16} width={220} radius="sm" />
        </Stack>

        <Group justify="space-between">
          <Group gap="sm">
            <Skeleton height={36} width={130} radius="md" />
            <Skeleton height={36} width={130} radius="md" />
          </Group>
          <Skeleton height={24} width={180} radius="sm" />
        </Group>

        <SimpleGrid cols={{ base: 1, sm: 2, md: 4 }} spacing="md">
          {Array.from({ length: 4 }).map((_, index) => (
            <Paper key={index} p="md" radius="lg" withBorder>
              <Stack gap="sm">
                <Skeleton height={160} radius="md" />
                <Skeleton height={18} width="85%" radius="sm" />
                <Skeleton height={14} width="50%" radius="sm" />
                <Group justify="space-between" mt="xs">
                  <Skeleton height={24} width={90} radius="sm" />
                  <Skeleton height={34} width={34} radius="md" />
                </Group>
              </Stack>
            </Paper>
          ))}
        </SimpleGrid>

        <Paper p="md" radius="lg" withBorder>
          <Stack gap="md">
            {Array.from({ length: 5 }).map((_, rowIndex) => (
              <SimpleGrid key={rowIndex} cols={{ base: 2, md: 4 }} spacing="md">
                <Skeleton height={16} width="70%" radius="sm" />
                <Skeleton height={16} width="60%" radius="sm" />
                <Skeleton height={16} width="75%" radius="sm" />
                <Skeleton height={16} width="65%" radius="sm" />
              </SimpleGrid>
            ))}
          </Stack>
        </Paper>
      </Stack>
    </Container>
  );
}
