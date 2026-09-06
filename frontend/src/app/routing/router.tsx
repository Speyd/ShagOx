import HomePage from "@/pages/home-page";
import LoginPage from "@/pages/auth/login-page";
import RegisterPage from "@/pages/auth/register-page";
import { Routes, Route } from "react-router-dom";
import NotFoundPage from "@/pages/not-found-page";
import AdvertisementPage from "@/pages/advertisement/advertisement-page";
import { PublicRoute } from "./PublicRoute";
import CreateAdvertisementPage from "@/pages/advertisement/create-advertisement-page/CreateAdvertisementPage";
import UserUpdateAdvertisementPage from "@/pages/advertisement/update-advertisement-page";
import AdminAdvertisementPage from "@/pages/admin/advertisements/update-advertisement-page";
import FavoritesPage from "@/pages/favorites-page";
import { ProtectedRoute } from "./ProtectedRoute";
import { AdminRoute } from "./AdminRoute";
import { AdminLayout, MainLayout } from "../layouts";
import DashboardPage from "@/pages/admin/dashboard/dashboard-page/DashboardPage";
import UsersPage from "@/pages/admin/users/users-page/UsersPage";
import AdvertisementsPage from "@/pages/admin/advertisements/advertisements-page";
import CategoriesPage from "@/pages/admin/categories/categories-page/CategoriesPage";
import UpdateUserPage from "@/pages/admin/users/update-user-page/UpdateUserPage";
import UpdateCategoriesPage from "@/pages/admin/categories/update-categories-page/UpdateCategoriesPage";

export default function Router() {
  return (
    <>
      <Routes>
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
            path="/favorite"
            element={
              <ProtectedRoute>
                <FavoritesPage />
              </ProtectedRoute>
            }
          />

          <Route
            path="/login"
            element={
              <PublicRoute>
                <LoginPage />
              </PublicRoute>
            }
          />

          <Route
            path="/register"
            element={
              <PublicRoute>
                <RegisterPage />
              </PublicRoute>
            }
          />
          <Route
            path="/create-advertisement"
            element={<CreateAdvertisementPage />}
          />
          <Route
            path="/update-advertisement/:id"
            element={<UserUpdateAdvertisementPage />}
          />
          <Route path="/advertisement/:id" element={<AdvertisementPage />} />
          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
    </>
  );
}
