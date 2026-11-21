import {
  Group,
  Paper,
  Skeleton,
  Stack,
  Table,
  Title,
  Text,
} from "@mantine/core";

export function AdvertisementsPageSkeleton() {
  return (
    <Stack gap="lg">
      <div>
        <Title order={2}>Оголошення</Title>
        <Text c="dimmed" size="sm" mt={4}>
          Керування оголошеннями маркетплейсу
        </Text>
      </div>

      <Skeleton height={42} radius="md" />

      {/* Таблиця */}
      <Paper withBorder radius="md">
        <Table verticalSpacing="md" horizontalSpacing="md">
          <Table.Thead>
            <Table.Tr>
              <Table.Th style={{ width: "45%" }}>Оголошення</Table.Th>
              <Table.Th style={{ width: "15%" }}>Продавець</Table.Th>
              <Table.Th style={{ width: "10%" }}>Ціна</Table.Th>
              <Table.Th style={{ width: "10%" }}>Статус</Table.Th>
              <Table.Th style={{ width: "10%" }}>Створено</Table.Th>
              <Table.Th style={{ width: "10%" }} />
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {Array.from({ length: 6 }).map((_, index) => (
              <Table.Tr key={index}>
                <Table.Td>
                  <Group gap="md" wrap="nowrap">
                    <Skeleton height={48} width={48} radius="sm" />
                    <Stack gap={6} style={{ flex: 1 }}>
                      <Skeleton height={16} width="60%" radius="sm" />
                      <Skeleton height={12} width="90%" radius="sm" />
                    </Stack>
                  </Group>
                </Table.Td>

                <Table.Td>
                  <Stack gap={4}>
                    <Skeleton height={14} width={80} radius="sm" />
                    <Skeleton height={12} width={60} radius="sm" />
                  </Stack>
                </Table.Td>

                <Table.Td>
                  <Skeleton height={16} width={70} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={20} width={50} radius="xl" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={14} width={75} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Group gap={6} justify="flex-end" wrap="nowrap">
                    <Skeleton height={32} width={32} radius="md" />
                    <Skeleton height={32} width={32} radius="md" />
                  </Group>
                </Table.Td>
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>
      </Paper>
    </Stack>
  );
}
