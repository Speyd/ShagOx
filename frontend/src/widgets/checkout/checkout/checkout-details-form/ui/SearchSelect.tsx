import { Loader, Select } from "@mantine/core";

type SearchSelectProps<T> = {
  label: string;
  placeholder: string;
  items: T[];
  value: T | null;
  getKey: (item: T) => string;
  getLabel: (item: T) => string;
  onChange: (item: T | null) => void;
  onSearchChange: (search: string) => void;
  loading?: boolean;
  disabled?: boolean;
  error?: string;
  nothingFoundMessage?: string;
};

export default function SearchSelect<T>({
  label,
  placeholder,
  items,
  value,
  getKey,
  getLabel,
  onChange,
  onSearchChange,
  loading,
  disabled,
  error,
  nothingFoundMessage = "Нічого не знайдено",
}: SearchSelectProps<T>) {
  const isValueInItems =
    value !== null && items.some((item) => getKey(item) === getKey(value));

  const options = value && !isValueInItems ? [value, ...items] : items;

  const handleChange = (key: string | null) => {
    const selected = options.find((item) => getKey(item) === key) ?? null;
    onChange(selected);
  };

  return (
    <Select
      label={label}
      placeholder={placeholder}
      searchable
      clearable
      data={options.map((item) => ({
        value: getKey(item),
        label: getLabel(item),
      }))}
      value={value ? getKey(value) : null}
      onChange={handleChange}
      onSearchChange={onSearchChange}
      filter={({ options: allOptions }) => allOptions}
      nothingFoundMessage={nothingFoundMessage}
      rightSection={loading ? <Loader size={16} /> : undefined}
      disabled={disabled}
      error={error}
      radius="md"
    />
  );
}
