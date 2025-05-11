import { FC } from "react";

import Container from "@mui/material/Container";
import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";

const RouteComponent: FC = () => {
  return (
    <Container
      maxWidth="sm"
      sx={{ py: 4 }}
    >
      <Outlet />
    </Container>
  );
};

export const Route = createFileRoute("/auth")({
  beforeLoad: ({ context }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({
        to: "/",
      });
    }
  },
  component: RouteComponent,
});
