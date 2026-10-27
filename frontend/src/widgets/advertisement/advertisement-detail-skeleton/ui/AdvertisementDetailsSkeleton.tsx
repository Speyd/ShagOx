import Container from "@/shared/ui/container";
import { Grid, Group, Skeleton, Stack } from "@mantine/core";

export default function AdvertisementDetailsSkeleton() {
  return (
    <Container>
      <Group gap="xs" mb="lg">
        <Skeleton height={14} width={60} radius="sm" />
        <Skeleton height={14} width={10} radius="sm" />
        <Skeleton height={14} width={70} radius="sm" />
        <Skeleton height={14} width={10} radius="sm" />
        <Skeleton height={14} width={120} radius="sm" />
      </Group>

      <Grid>
        <Grid.Col span={{ base: 12, md: 6 }}>
          <Group align="flex-start" wrap="nowrap" gap="md">
            <Stack gap="xs">
              <Skeleton height={70} width={60} radius="md" />
              <Skeleton height={70} width={60} radius="md" />
              <Skeleton height={70} width={60} radius="md" />
            </Stack>

            <Skeleton height={480} style={{ flex: 1 }} radius="md" />
          </Group>
        </Grid.Col>

        <Grid.Col span={{ base: 12, md: 6 }}>
          <Stack gap="md">
            <Skeleton height={36} width="80%" radius="sm" />

            <Skeleton height={16} width="95%" radius="sm" />

            <Group gap="xs" mt="xs">
              <Skeleton height={20} width={100} radius="sm" />
              <Skeleton height={16} width={40} radius="sm" />
            </Group>

            <Skeleton height={32} width={120} radius="sm" my="xs" />

            <Stack gap="sm" mt="md">
              {Array.from({ length: 7 }).map((_, index) => (
                <Group key={index} gap="md">
                  <Skeleton height={16} width={90} radius="sm" />
                  <Skeleton height={16} width={110} radius="sm" />
                </Group>
              ))}
            </Stack>
          </Stack>
        </Grid.Col>
      </Grid>
    </Container>
  );
}
