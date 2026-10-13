import HomePage from "@/pages/home-page";
import { Routes, Route } from "react-router-dom";
import NotFoundPage from "@/pages/not-found-page";
import AdvertisementPage from "@/pages/advertisement/advertisement-page";
import { PublicRoute } from "./PublicRoute";
import UserUpdateAdvertisementPage from "@/pages/advertisement/update-advertisement-page";
import AdminAdvertisementPage from "@/pages/admin/advertisements/update-advertisement-page";
import { ProtectedRoute } from "./ProtectedRoute";
import { AdminRoute } from "./AdminRoute";
import { AdminLayout, MainLayout } from "../layouts";
import DashboardPage from "@/pages/admin/dashboard/dashboard-page/DashboardPage";
import UsersPage from "@/pages/admin/users/users-page/UsersPage";
import AdvertisementsPage from "@/pages/admin/advertisements/advertisements-page";
import CategoriesPage from "@/pages/admin/categories/categories-page/CategoriesPage";
import UpdateUserPage from "@/pages/admin/users/update-user-page/UpdateUserPage";
import UpdateCategoriesPage from "@/pages/admin/categories/update-categories-page/UpdateCategoriesPage";

import { GeneralTab } from "@/widgets/profile-general";
import { VideosTab } from "@/widgets/profile-videos";
import { FavoritesTab } from "@/widgets/profile-favorites";
import { OrdersTab } from "@/widgets/profile-orders";
import ProfileLayout from "../layouts/ProfileLayout";
import { StatisticsTab } from "@/widgets/profile-statistics";
import { AuthPage } from "@/pages/auth";
import ForgotPasswordPage from "@/pages/auth/ForgotPasswordPage";
import CategorySelectionPage from "@/pages/category-selection/CategorySelectionPage";
import VerifyPage from "@/pages/verify-page";
import CheckoutPage from "@/pages/checkout";
import PaymentPage from "@/pages/payment";
import BasketPage from "@/pages/basket";
import NotificationSettingsPage from "@/pages/notification-settings/NotificationSettingsPage";
import NotificationsLayout from "../layouts/NotificationsLayout";
import NotificationsGeneral from "@/widgets/notifications/notifications-generel/ui/NotificationsGeneral";
import NotificationsVideos from "@/widgets/notifications/notifications-videos";
import NotificationsOrders from "@/widgets/notifications/notifications-orders";
import NotificationsComments from "@/widgets/notifications/notifications-comments";
import NotificationsDiscounts from "@/widgets/notifications/notifications-discounts";

export default function Router() {
  return (
    <>
      <Routes>
        <Route
          path="/authentication"
          element={
            <PublicRoute>
              <AuthPage />
            </PublicRoute>
          }
        />

        <Route path="/category-selection" element={<CategorySelectionPage />} />

        <Route path="/authentication/verify" element={<VerifyPage />} />

        <Route path="/forgot-password" element={<ForgotPasswordPage />} />

        <Route
          path="/admin"
          element={
            <AdminRoute>
              <AdminLayout />
            </AdminRoute>
          }
        >
          <Route index element={<DashboardPage />} />
          <Route path="users" element={<UsersPage />} />
          <Route path="advertisements" element={<AdvertisementsPage />} />
          <Route
            path="update-advertisement/:id"
            element={<AdminAdvertisementPage />}
          />
          <Route path="update-user/:id" element={<UpdateUserPage />} />
          <Route path="categories" element={<CategoriesPage />} />
          <Route
            path="update-category/:id"
            element={<UpdateCategoriesPage />}
          />
        </Route>

        <Route element={<MainLayout />}>
          <Route path="/" element={<HomePage />} />
          <Route
            path="/profile"
            element={
              <ProtectedRoute>
                <ProfileLayout />
              </ProtectedRoute>
            }
          >
            <Route index element={<GeneralTab />} />
            <Route path="videos" element={<VideosTab />} />
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
            path="/payment"
            element={
              <ProtectedRoute>
                <PaymentPage />
              </ProtectedRoute>
            }
          />

          <Route
            path="/checkout"
            element={
              <ProtectedRoute>
                <CheckoutPage />
              </ProtectedRoute>
            }
          />

          <Route
            path="/basket"
            element={
              <ProtectedRoute>
                <BasketPage />
              </ProtectedRoute>
            }
          />

          <Route
            path="/update-advertisement/:id"
            element={
              <ProtectedRoute>
                <UserUpdateAdvertisementPage />
              </ProtectedRoute>
            }
          />
          <Route
            path="/notifications/settings"
            element={<NotificationSettingsPage />}
          />
          <Route path="/advertisement/:id" element={<AdvertisementPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </>
  );
}
