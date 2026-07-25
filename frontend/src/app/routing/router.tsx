import HomePage from "@/pages/HomePage";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
import { Routes, Route } from "react-router-dom";
import MainLayout from "../layouts";
import NotFoundPage from "@/pages/NotFoundPage";
import AdvertisementPage from "@/pages/AdvertisementPage";
import { PublicRoute } from "./PublicRoute";
import CreateAdvertisementPage from "@/pages/CreateAdvertisementPage/CreateAdvertisementPage";
import UpdateAdvertisementPage from "@/pages/UpdateAdvertisementPage";
import FavoritesPage from "@/pages/FavoritesPage";
import { ProtectedRoute } from "./ProtectedRoute";

export default function Router() {
  return (
    <>
      <Routes>
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
            element={<UpdateAdvertisementPage />}
          />
          <Route path="/advertisement/:id" element={<AdvertisementPage />} />
        </Route>

        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    </>
  );
}
