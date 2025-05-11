import { FC } from "react";
import { ToastContainer } from "react-toastify";

import CssBaseline from "@mui/material/CssBaseline";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { createRouter, RouterProvider } from "@tanstack/react-router";

import { useAuthStore } from "./stores/auth";
import AppTheme from "./theme/AppTheme";

import { routeTree } from "./routeTree.gen";

import "react-toastify/dist/ReactToastify.css";

import "@fontsource/roboto/300.css";
import "@fontsource/roboto/400.css";
import "@fontsource/roboto/500.css";
import "@fontsource/roboto/700.css";

const router = createRouter({
  context: {
    auth: undefined!,
  },
  routeTree,
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

const queryClient = new QueryClient();

const App: FC = () => {
  const auth = useAuthStore((state) => state);

  return (
    <AppTheme>
      <CssBaseline enableColorScheme />
      <ToastContainer stacked />
      <QueryClientProvider client={queryClient}>
        <ReactQueryDevtools initialIsOpen={false} />
        <RouterProvider
          context={{ auth }}
          router={router}
        />
      </QueryClientProvider>
    </AppTheme>
  );
};

export default App;
