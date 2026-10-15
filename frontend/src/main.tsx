import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import App from "./app";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MantineProvider } from "@mantine/core";
import { ModalsProvider } from "@mantine/modals";
import { NuqsAdapter } from "nuqs/adapters/react-router/v7";
import "@mantine/core/styles.css";

const queryClient = new QueryClient();

createRoot(document.getElementById("root")!).render(
  <MantineProvider defaultColorScheme="light">
    <ModalsProvider>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <NuqsAdapter>
            <App />
          </NuqsAdapter>
        </BrowserRouter>
      </QueryClientProvider>
    </ModalsProvider>
  </MantineProvider>,
);