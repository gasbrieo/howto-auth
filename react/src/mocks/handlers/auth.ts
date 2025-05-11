import { http, HttpResponse } from "msw";

import {
  LoginUserRequest,
  LoginUserResponse,
  RegisterUserRequest,
  RegisterUserResponse,
} from "@/types/auth";
import { ProblemDetails } from "@/types/api";

export const authHandler = [
  http.post("/auth/login", async ({ request }) => {
    await new Promise((r) => setTimeout(r, 1000));

    const body = (await request.json()) as LoginUserRequest;

    if (body.email === "gaberabreu@gmail.com") {
      const response: LoginUserResponse = {
        token: "mocked-token",
      };

      return HttpResponse.json(response);
    }

    const response: ProblemDetails = {
      title: "One or more validation errors occurred.",
      errors: ["Invalid email or password"],
    };

    return HttpResponse.json(response, { status: 400 });
  }),

  http.post("/auth/register", async ({ request }) => {
    await new Promise((r) => setTimeout(r, 1000));

    const body = (await request.json()) as RegisterUserRequest;

    if (body.email === "gaberabreu@gmail.com") {
      const response: RegisterUserResponse = {
        token: "mocked-token",
      };

      return HttpResponse.json(response);
    }

    const response: ProblemDetails = {
      title: "One or more validation errors occurred.",
      errors: ["Email already exists"],
    };

    return HttpResponse.json(response, { status: 400 });
  }),
];
