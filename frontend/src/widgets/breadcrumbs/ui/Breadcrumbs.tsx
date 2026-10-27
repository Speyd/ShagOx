import { Breadcrumbs as MantineBreadcrumbs, Anchor, Text } from "@mantine/core";
import { ChevronRight } from "lucide-react";
import { Link, useLocation, useParams } from "react-router-dom";

import { useGetAdvertisement } from "@/entities/advertisement/model/hooks/useGetAdvertisement";
import { useGetCategory } from "@/entities/category/model/useGetCategory";
import { useGetCategoryByCode } from "@/entities/category/model/useGetCategoryByCode";
import { useGetProductTypes } from "@/entities/advertisement/model/hooks/useGetProductTypes";
import type { Category } from "@/shared/lib/types/category";

const ROUTE_LABELS: Record<string, string> = {
  advertisement: "Оголошення",
  catalog: "Каталог",
  profile: "Профіль",
  cart: "Кошик",
  favorites: "Обране",
};

const IGNORED_SEGMENTS = new Set(["category"]);

type BreadcrumbsProps = {
  category?: Category;
  lastItemLabel?: string;
};

export default function Breadcrumbs({
  category: propCategory,
  lastItemLabel,
}: BreadcrumbsProps) {
  const location = useLocation();
  const {
    categoryId,
    categoryCode,
    categorySlug,
    productTypeCode,
    advertisementId,
  } = useParams<{
    categoryId?: string;
    categoryCode?: string;
    categorySlug?: string;
    productTypeCode?: string;
    advertisementId?: string;
  }>();

  const currentCategoryCode =
    categoryCode || categorySlug || propCategory?.code;

  const parsedCategoryId = categoryId ? Number(categoryId) : undefined;
  const parsedAdvertisementId = advertisementId
    ? Number(advertisementId)
    : undefined;

  const { data: categoryById } = useGetCategory(parsedCategoryId);
  const { data: categoryByCode } = useGetCategoryByCode(currentCategoryCode);
  const { data: advertisement } = useGetAdvertisement(parsedAdvertisementId);
  const { data: productTypes } = useGetProductTypes();

  const category = categoryByCode || categoryById || propCategory;

  const isAdvertisementPage = location.pathname.startsWith("/advertisement/");

  if (isAdvertisementPage && category) {
    const productType = category.productType;

    const matchedProductType = productTypes?.find(
      (type) => type.name?.toLowerCase() === productType?.code?.toLowerCase(),
    );

    const rawCategory = category as unknown as {
      label?: string;
      lable?: string;
    };
    const categoryLabel =
      rawCategory.label || rawCategory.lable || category.code;

    const productTypeLabel =
      matchedProductType?.label || productType?.label || productType?.code;

    const customItems = [
      <Anchor
        component={Link}
        to="/"
        key="home"
        fz={14}
        c="dimmed"
        underline="never"
      >
        Головна
      </Anchor>,
      <Anchor
        component={Link}
        to="/catalog"
        key="catalog"
        fz={14}
        c="dimmed"
        underline="never"
      >
        Каталог
      </Anchor>,
    ];

    if (productType) {
      customItems.push(
        <Anchor
          component={Link}
          to={`/catalog/${productType.code}`}
          key="product-type"
          fz={14}
          c="dimmed"
          underline="never"
        >
          {productTypeLabel}
        </Anchor>,
      );
    }

    if (category) {
      customItems.push(
        <Anchor
          component={Link}
          to={`/catalog/category/${category.code}`}
          key="category"
          fz={14}
          c="dimmed"
          underline="never"
        >
          {categoryLabel}
        </Anchor>,
      );
    }

    if (lastItemLabel) {
      customItems.push(
        <Text
          key="last"
          fz={14}
          fw={500}
          c="dark.3"
          lineClamp={1}
          style={{ maxWidth: 300 }}
        >
          {lastItemLabel}
        </Text>,
      );
    }

    return (
      <MantineBreadcrumbs
        separator={<ChevronRight size={14} color="#adb5bd" />}
        mb="md"
        mt="xs"
      >
        {customItems}
      </MantineBreadcrumbs>
    );
  }

  const matchedProductType = productTypes?.find(
    (type) => type.name?.toLowerCase() === productTypeCode?.toLowerCase(),
  );

  const rawCategory = category as unknown as { label?: string; lable?: string };
  const productTypeLabel = matchedProductType?.label;
  const categoryLabel = rawCategory?.label || rawCategory?.lable;

  const pathnames = location.pathname.split("/").filter(Boolean);
  const filteredSegments = pathnames.filter(
    (segment) => !IGNORED_SEGMENTS.has(segment),
  );

  const items = [
    <Anchor
      component={Link}
      to="/"
      key="home"
      fz={14}
      c="dimmed"
      underline="never"
    >
      Головна
    </Anchor>,
    ...filteredSegments.map((value, index) => {
      const isLast = index === filteredSegments.length - 1;
      const isNumeric = !isNaN(Number(value));

      const segmentIndex = pathnames.indexOf(value);
      const to = `/${pathnames.slice(0, segmentIndex + 1).join("/")}`;

      let label = ROUTE_LABELS[value] || decodeURIComponent(value);

      if (value === currentCategoryCode && categoryLabel) {
        label = categoryLabel;
      } else if (
        productTypeCode &&
        value.toLowerCase() === productTypeCode.toLowerCase() &&
        productTypeLabel
      ) {
        label = productTypeLabel;
      } else if (isNumeric) {
        if (value === categoryId && categoryLabel) {
          label = categoryLabel;
        } else if (value === advertisementId && advertisement?.title) {
          label = advertisement.title;
        }
      }

      return isLast ? (
        <Text key={to} fz={14} fw={500} c="dark.3">
          {lastItemLabel || label}
        </Text>
      ) : (
        <Anchor
          component={Link}
          to={to}
          key={to}
          fz={14}
          c="dimmed"
          underline="never"
        >
          {label}
        </Anchor>
      );
    }),
  ];

  return (
    <MantineBreadcrumbs
      separator={<ChevronRight size={14} color="#adb5bd" />}
      mb="md"
      mt="xs"
    >
      {items}
    </MantineBreadcrumbs>
  );
}
