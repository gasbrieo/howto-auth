import { AxiosError } from "axios";

export type ProblemDetails = {
  title: string;
  errors?: string[];
};

export type HttpApiError = AxiosError & {
  problemDetails: ProblemDetails;
};
