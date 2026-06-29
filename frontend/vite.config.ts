import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  server: {
    proxy: {
      "/api": {
        target: "https://localhost:7263",
        changeOrigin: true,
        secure: false,
      },
      "/auth": {
        target: "https://localhost:7263",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
