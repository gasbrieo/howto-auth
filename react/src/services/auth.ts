import { apiClient } from "@/libs/axios";
import {
  LoginUserRequest,
  LoginUserResponse,
  RegisterUserRequest,
  RegisterUserResponse,
} from "@/types/auth";

export const postLogin = async (request: LoginUserRequest): Promise<LoginUserResponse> => {
  const response = await apiClient.post("/auth/login", request);
  return response.data;
};

export const postRegister = async (request: RegisterUserRequest): Promise<RegisterUserResponse> => {
  const response = await apiClient.post("/auth/register", request);
  return response.data;
};
