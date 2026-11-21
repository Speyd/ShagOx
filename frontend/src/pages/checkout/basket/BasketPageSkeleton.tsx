import {
  Box,
  Container,
  Divider,
  Flex,
  Grid,
  Group,
  Paper,
  Skeleton,
  Stack,
} from "@mantine/core";
import classes from "./BasketPageSkeleton.module.css";

interface BasketPageSkeletonProps {
  itemsCount?: number;
}

export function BasketPageSkeleton({
  itemsCount = 2,
}: BasketPageSkeletonProps) {
  return (
    <Box className={classes.wrapper}>
      <Container size="xl" py="xl">
        <Skeleton height={34} width={130} radius="sm" mb="md" />

        <Group justify="space-between" mb="lg">
          <Group gap="xs">
            <Skeleton height={20} width={20} radius="xs" />
            <Skeleton height={16} width={115} radius="sm" />
          </Group>
          <Group gap="xs">
            <Skeleton height={18} width={18} radius="xs" />
            <Skeleton height={16} width={110} radius="sm" />
          </Group>
        </Group>

        <Grid align="flex-start">
          <Grid.Col span={{ base: 12, md: 8 }}>
            <Stack gap="md">
              {Array.from({ length: itemsCount }).map((_, index) => (
                <Paper
                  key={index}
                  withBorder
                  radius="md"
                  p="md"
                  className={classes.card}
                >
                  <Flex gap="md" align="stretch">
                    <Flex align="center">
                      <Skeleton height={20} width={20} radius="xs" />
                    </Flex>

                    <Skeleton
                      height={90}
                      width={90}
                      radius="md"
                      className={classes.imageSkeleton}
                    />

                    <Flex
                      direction="column"
                      justify="space-between"
                      className={classes.itemContent}
                    >
                      <Group
                        justify="space-between"
                        align="flex-start"
                        wrap="nowrap"
                      >
                        <Stack gap={8}>
                          <Skeleton height={18} width={210} radius="sm" />
                          <Skeleton height={14} width={105} radius="sm" />
                        </Stack>

                        <Skeleton height={22} width={85} radius="sm" />
                      </Group>

                      <Group justify="space-between" align="center" mt="md">
                        <Group gap="sm">
                          <Skeleton height={16} width={95} radius="sm" />
                          <Divider orientation="vertical" />
                          <Skeleton height={16} width={80} radius="sm" />
                        </Group>

                        <Skeleton height={32} width={96} radius="xl" />
                      </Group>
                    </Flex>
                  </Flex>
                </Paper>
              ))}

              <Paper
                withBorder
                radius="md"
                p="md"
                mt="md"
                className={classes.card}
              >
                <Group
                  justify="space-between"
                  align="center"
                  wrap="wrap"
                  gap="md"
                >
                  <Group gap="md">
                    <Skeleton height={36} width={36} radius="md" />
                    <Stack gap={6}>
                      <Skeleton height={16} width={240} radius="sm" />
                      <Skeleton height={13} width={190} radius="sm" />
                    </Stack>
                  </Group>

                  <Stack
                    gap={6}
                    align="flex-end"
                    className={classes.progressWrapper}
                  >
                    <Skeleton height={6} width="100%" radius="xl" />
                    <Skeleton height={12} width={130} radius="sm" />
                  </Stack>
                </Group>
              </Paper>
            </Stack>
          </Grid.Col>

          <Grid.Col span={{ base: 12, md: 4 }}>
            <Stack gap="md">
              <Paper withBorder radius="md" p="lg" className={classes.card}>
                <Skeleton height={24} width={190} radius="sm" mb="lg" />

                <Stack gap="sm">
                  <Group justify="space-between">
                    <Skeleton height={15} width={80} radius="sm" />
                    <Skeleton height={15} width={75} radius="sm" />
                  </Group>

                  <Group justify="space-between">
                    <Skeleton height={15} width={75} radius="sm" />
                    <Skeleton height={15} width={145} radius="sm" />
                  </Group>
                </Stack>

                <Divider my="lg" />

                <Group justify="space-between" align="center" mb="lg">
                  <Skeleton height={20} width={95} radius="sm" />
                  <Skeleton height={28} width={105} radius="sm" />
                </Group>

                <Skeleton height={44} width="100%" radius="md" mb="sm" />

                <Flex justify="center">
                  <Skeleton height={14} width={195} radius="sm" />
                </Flex>
              </Paper>

              <Paper withBorder radius="md" p="lg" className={classes.card}>
                <Group gap="xs" mb="md">
                  <Skeleton height={20} width={20} radius="sm" />
                  <Skeleton height={18} width={165} radius="sm" />
                </Group>

                <Stack gap="sm">
                  <Group gap="xs">
                    <Skeleton height={14} width={14} radius="xl" />
                    <Skeleton height={14} width={175} radius="sm" />
                  </Group>
                  <Group gap="xs">
                    <Skeleton height={14} width={14} radius="xl" />
                    <Skeleton height={14} width={205} radius="sm" />
                  </Group>
                  <Group gap="xs">
                    <Skeleton height={14} width={14} radius="xl" />
                    <Skeleton height={14} width={115} radius="sm" />
                  </Group>
                </Stack>
              </Paper>
            </Stack>
          </Grid.Col>
        </Grid>
      </Container>
    </Box>
  );
}
