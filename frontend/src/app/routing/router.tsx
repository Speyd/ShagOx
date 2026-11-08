import { lazy } from "react";
import HomePage from "@/pages/home";
import { Navigate, Routes, Route } from "react-router-dom";
import NotFoundPage from "@/pages/not-found";
import UserUpdateAdvertisementPage from "@/pages/advertisement/update-advertisement-page";
import { AdminLayout, MainLayout } from "../layouts";
const DashboardPage = lazy(() => import("@/pages/admin/dashboard/DashboardPage"));
const AdvertisementsPage = lazy(() => import("@/pages/admin/advertisements/AdvertisementsPage"));
const AdvertisementCreatePage = lazy(() => import("@/pages/admin/advertisements/AdvertisementCreatePage"));
const AdvertisementEditPage = lazy(() => import("@/pages/admin/advertisements/AdvertisementEditPage"));
const UsersPage = lazy(() => import("@/pages/admin/users/UsersPage"));
const UserEditPage = lazy(() => import("@/pages/admin/users/UserEditPage"));
const ReviewsPage = lazy(() => import("@/pages/admin/reviews/ReviewsPage"));
const ProductTypesPage = lazy(() => import("@/pages/admin/product-types/ProductTypesPage"));
const CategoriesPage = lazy(() => import("@/pages/admin/categories/CategoriesPage"));
const CategoryDetailsPage = lazy(() => import("@/pages/admin/categories/CategoryDetailsPage"));
const DictionariesPage = lazy(() => import("@/pages/admin/dictionaries/DictionariesPage"));
const DictionaryDetailsPage = lazy(() => import("@/pages/admin/dictionaries/DictionaryDetailsPage"));
const ConditionsPage = lazy(() => import("@/pages/admin/conditions/ConditionsPage"));
const StatusesPage = lazy(() => import("@/pages/admin/statuses/StatusesPage"));
const CurrenciesPage = lazy(() => import("@/pages/admin/currencies/CurrenciesPage"));
const RegionsPage = lazy(() => import("@/pages/admin/regions/RegionsPage"));
const CitiesPage = lazy(() => import("@/pages/admin/cities/CitiesPage"));
const RolesPage = lazy(() => import("@/pages/admin/roles/RolesPage"));

import { GeneralTab } from "@/widgets/profile/profile-general";
import { FavoritesTab } from "@/widgets/profile/profile-favorites";
import { OrdersTab } from "@/widgets/profile/profile-orders";
import ProfileLayout from "../layouts/ProfileLayout";
import { StatisticsTab } from "@/widgets/profile/profile-statistics";
import { AuthPage } from "@/pages/auth";
import ForgotPasswordPage from "@/pages/auth/ForgotPasswordPage";
import CategorySelectionPage from "@/pages/category-selection/CategorySelectionPage";
import VerifyPage from "@/pages/verify";
import CheckoutPage from "@/pages/checkout/checkout";
import PaymentPage from "@/pages/checkout/payment";
import BasketPage from "@/pages/checkout/basket";

import NotificationSettingsPage from "@/pages/notifications/notification-settings/NotificationSettingsPage";
import NotificationsLayout from "../layouts/NotificationsLayout";
import NotificationsGeneral from "@/widgets/notifications/notifications-generel/ui/NotificationsGeneral";
import NotificationsVideos from "@/widgets/notifications/notifications-videos";
import NotificationsOrders from "@/widgets/notifications/notifications-orders";
import NotificationsComments from "@/widgets/notifications/notifications-comments";
import NotificationsDiscounts from "@/widgets/notifications/notifications-discounts";
import ReferencePage from "@/pages/info/reference/ReferencePage";

import PaymentMethodsPage from "@/pages/info/payment-methods";
import DeliveryTermsPage from "@/pages/info/delivery-terms";
import OrderTrackingPage from "@/pages/info/order-tracking";
import SupportPage from "@/pages/info/support/SupportPage";
import CatalogLayout from "../layouts/CatalogLayout";
import { CatalogMainPage } from "@/pages/catalog/main-page";
import { CatalogCategorySelectionPage } from "@/pages/catalog/category-selection-page";
import { CategoryAdvertisementsPage } from "@/pages/catalog/advertisements-page";
import CreateAdvertisementPage from "@/pages/advertisement/create-advertisement-page";
import ComparisonPage from "@/pages/compare";
import AboutPage from "@/pages/info/about";
import WarrantyPage from "@/pages/info/warranty/WarrantyPage";
import PrivacyPolicyPage from "@/pages/info/privacy-policy/PrivacyPolicyPage";
import { AdvertisementPage } from "@/pages/advertisement/advertisement-page";
import { AdminRoute, ProtectedRoute, PublicRoute } from "./routes";

export default function Router() {
  return (
    <Routes>
      <Route element={<PublicRoute />}>
        <Route path="/authentication" element={<AuthPage />} />
        <Route path="/authentication/verify" element={<VerifyPage />} />
        <Route path="/forgot-password" element={<ForgotPasswordPage />} />
      </Route>

      <Route element={<AdminRoute />}>
        <Route path="/admin" element={<AdminLayout />}>
          <Route index element={<DashboardPage />} />
          <Route path="advertisements" element={<AdvertisementsPage />} />
          <Route path="advertisements/new" element={<AdvertisementCreatePage />} />
          <Route path="advertisements/:id" element={<AdvertisementEditPage />} />
          <Route path="users" element={<UsersPage />} />
          <Route path="users/:id" element={<UserEditPage />} />
          <Route path="reviews" element={<ReviewsPage />} />
          <Route path="product-types" element={<ProductTypesPage />} />
          <Route path="categories" element={<CategoriesPage />} />
          <Route path="categories/:id" element={<CategoryDetailsPage />} />
          <Route path="dictionaries" element={<DictionariesPage />} />
          <Route path="dictionaries/:id" element={<DictionaryDetailsPage />} />
          <Route path="conditions" element={<ConditionsPage />} />
          <Route path="statuses" element={<StatusesPage />} />
          <Route path="currencies" element={<CurrenciesPage />} />
          <Route path="regions" element={<RegionsPage />} />
          <Route path="cities" element={<CitiesPage />} />
          <Route path="roles" element={<RolesPage />} />
          <Route path="*" element={<Navigate to="/admin" replace />} />
        </Route>
      </Route>

      <Route element={<MainLayout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/category-selection" element={<CategorySelectionPage />} />
        <Route path="/compare" element={<ComparisonPage />} />
        <Route path="/basket" element={<BasketPage />} />

        <Route element={<ProtectedRoute />}>
          <Route
            path="/create-advertisement"
            element={<CreateAdvertisementPage />}
          />
          <Route
            path="/update-advertisement/:id"
            element={<UserUpdateAdvertisementPage />}
          />
          <Route path="/checkout" element={<CheckoutPage />} />
          <Route path="/payment" element={<PaymentPage />} />

          <Route path="/profile" element={<ProfileLayout />}>
            <Route index element={<GeneralTab />} />
            <Route path="favorites" element={<FavoritesTab />} />
            <Route path="orders" element={<OrdersTab />} />
            <Route path="statistics" element={<StatisticsTab />} />
          </Route>

          <Route path="/notifications" element={<NotificationsLayout />}>
            <Route index element={<NotificationsGeneral />} />
            <Route path="videos" element={<NotificationsVideos />} />
            <Route path="orders" element={<NotificationsOrders />} />
            <Route path="comments" element={<NotificationsComments />} />
            <Route path="discounts" element={<NotificationsDiscounts />} />
          </Route>
          <Route
            path="/notifications/settings"
            element={<NotificationSettingsPage />}
          />
        </Route>

        <Route path="/catalog" element={<CatalogLayout />}>
          <Route index element={<CatalogMainPage />} />
          <Route
            path=":productTypeCode"
            element={<CatalogCategorySelectionPage />}
          />
          <Route
            path="category/:categoryCode"
            element={<CategoryAdvertisementsPage />}
          />
        </Route>

        <Route path="/support" element={<SupportPage />} />
        <Route path="/about" element={<AboutPage />} />
        <Route path="/warranty" element={<WarrantyPage />} />
        <Route path="/privacy-policy" element={<PrivacyPolicyPage />} />
        <Route path="/delivery-terms" element={<DeliveryTermsPage />} />
        <Route path="/payment-methods" element={<PaymentMethodsPage />} />
        <Route path="/order-tracking" element={<OrderTrackingPage />} />
        <Route path="/reference" element={<ReferencePage />} />

        <Route path="/advertisement/:id" element={<AdvertisementPage />} />
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    </Routes>
  );
}
