import axios, { AxiosError, AxiosResponse, InternalAxiosRequestConfig } from "axios";

import { ProblemDetails } from "@/types/api";

const onRequest = (config: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {
  return config;
};

const onRequestError = (error: AxiosError): Promise<AxiosError> => {
  return Promise.reject(error);
};

const onResponse = (response: AxiosResponse): AxiosResponse => {
  return response;
};

const onResponseError = async (error: AxiosError): Promise<AxiosResponse> => {
  let problemDetails: ProblemDetails = {
    title: "Something went wrong while processing the request.",
  };

  if (error.response?.data) {
    problemDetails = error?.response?.data as ProblemDetails;
  }

  return Promise.reject({
    ...error,
    problemDetails,
  });
};

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
  timeout: 10000,
});

apiClient.interceptors.request.use(onRequest, onRequestError);
apiClient.interceptors.response.use(onResponse, onResponseError);

export { apiClient };
