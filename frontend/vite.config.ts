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
    allowedHosts: [
      "www.dmarketly.com",
      "dmarketly.com",
    ],
    proxy: {
      "/api": {
        target: "api-www.dmarketly.com",
        changeOrigin: true,
        secure: false,
      },
      "/auth": {
        target: "api-www.dmarketly.com",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
