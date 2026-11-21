import {
  Group,
  Paper,
  Skeleton,
  Stack,
  Table,
  Title,
  Text,
} from "@mantine/core";

export function UsersPageSkeleton() {
  return (
    <Stack gap="lg">
      <div>
        <Title order={2}>Користувачі</Title>
        <Text c="dimmed" size="sm" mt={4}>
          Керування користувачами маркетплейсу
        </Text>
      </div>

      <Skeleton height={42} radius="md" />

      <Paper withBorder radius="md">
        <Table verticalSpacing="md" horizontalSpacing="md">
          <Table.Thead>
            <Table.Tr>
              <Table.Th style={{ width: "30%" }}>Користувач</Table.Th>
              <Table.Th style={{ width: "25%" }}>Email</Table.Th>
              <Table.Th style={{ width: "12%" }}>Місто</Table.Th>
              <Table.Th style={{ width: "10%" }}>Роль</Table.Th>
              <Table.Th style={{ width: "13%" }}>Реєстрація</Table.Th>
              <Table.Th style={{ width: "10%" }} />
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {Array.from({ length: 7 }).map((_, index) => (
              <Table.Tr key={index}>
                <Table.Td>
                  <Group gap="sm" wrap="nowrap">
                    <Skeleton circle height={40} />
                    <Skeleton height={16} width={160} radius="sm" />
                  </Group>
                </Table.Td>

                <Table.Td>
                  <Skeleton height={16} width={180} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={16} width={60} radius="sm" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={20} width={55} radius="xl" />
                </Table.Td>

                <Table.Td>
                  <Skeleton height={16} width={90} radius="sm" />
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
