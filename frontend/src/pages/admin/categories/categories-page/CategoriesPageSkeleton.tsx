import {
  Group,
  Paper,
  Skeleton,
  Stack,
  Table,
  Title,
  Text,
} from "@mantine/core";

export function CategoriesPageSkeleton() {
  return (
    <Stack gap="lg">
      <div>
        <Title order={2}>Категорії</Title>
        <Text c="dimmed" size="sm" mt={4}>
          Керування категоріями товарів
        </Text>
      </div>

      <Skeleton height={42} radius="md" />

      <Paper withBorder radius="md">
        <Table verticalSpacing="md" horizontalSpacing="md">
          <Table.Thead>
            <Table.Tr>
              <Table.Th style={{ width: "10%" }}>ID</Table.Th>
              <Table.Th style={{ width: "45%" }}>Назва</Table.Th>
              <Table.Th style={{ width: "30%" }}>Тип товару</Table.Th>
              <Table.Th style={{ width: "15%" }} />
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {Array.from({ length: 7 }).map((_, index) => (
              <Table.Tr key={index}>
                <Table.Td>
                  <Skeleton height={16} width={30} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={16} width={180} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={22} width={120} radius="xl" />
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
