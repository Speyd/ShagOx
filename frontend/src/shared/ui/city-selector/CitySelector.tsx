import { Menu, ScrollArea, Text, TextInput } from "@mantine/core";
import { Check, ChevronDown, MapPin, Search } from "lucide-react";
import { useEffect, useMemo, useState } from "react";
import styles from "./CitySelector.module.css";

const cities = [
  "Київ",
  "Львів",
  "Одеса",
  "Харків",
  "Дніпро",
  "Запоріжжя",
  "Вінниця",
  "Полтава",
  "Чернігів",
  "Черкаси",
  "Івано-Франківськ",
  "Тернопіль",
  "Ужгород",
  "Хмельницький",
  "Рівне",
];

export default function CitySelector() {
  const [city, setCity] = useState(() => {
    return localStorage.getItem("selectedCity") || "Київ";
  });

  const [search, setSearch] = useState("");

  useEffect(() => {
    localStorage.setItem("selectedCity", city);
  }, [city]);

  const filteredCities = useMemo(() => {
    return cities.filter((cityName) =>
      cityName.toLowerCase().includes(search.toLowerCase()),
    );
  }, [search]);

  return (
    <Menu
      shadow="md"
      width={250}
      position="bottom-start"
      withinPortal
    >
      <Menu.Target>
        <button className={styles.headerCity}>
          <MapPin size={20} />

          <Text size="sm">{city}</Text>

          <ChevronDown size={16} />
        </button>
      </Menu.Target>

      <Menu.Dropdown>
        <div className={styles.dropdownHeader}>
          <Text fw={600}>Оберіть місто</Text>
        </div>

        <TextInput
          placeholder="Пошук міста"
          value={search}
          onChange={(event) => setSearch(event.currentTarget.value)}
          leftSection={<Search size={16} />}
        />

        <ScrollArea h={250} mt="sm">
          {filteredCities.map((cityName) => (
            <Menu.Item
              key={cityName}
              onClick={() => {
                setCity(cityName);
                setSearch("");
              }}
              rightSection={
                city === cityName ? <Check size={16} /> : null
              }
            >
              {cityName}
            </Menu.Item>
          ))}

          {filteredCities.length === 0 && (
            <Text
              size="sm"
              c="dimmed"
              ta="center"
              py="md"
            >
              Місто не знайдено
            </Text>
          )}
        </ScrollArea>
      </Menu.Dropdown>
    </Menu>
  );
}