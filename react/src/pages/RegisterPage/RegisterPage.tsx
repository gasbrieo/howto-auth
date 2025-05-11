import { FC } from "react";

import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import Typography from "@mui/material/Typography";
import { useForm } from "@tanstack/react-form";
import { useNavigate } from "@tanstack/react-router";

import FormTextField from "@/components/FormTextField";
import Link from "@/components/Link";
import { notify } from "@/libs/toast";
import { registerSchema } from "@/schemas/registerSchema";
import { postRegister } from "@/services/auth";
import { useAuthStore } from "@/stores/auth";
import { HttpApiError } from "@/types/api";

const RegisterPage: FC = () => {
  const login = useAuthStore((state) => state.login);
  const navigate = useNavigate();

  const form = useForm({
    defaultValues: {
      email: "",
      password: "",
      confirmPassword: "",
    },
    validators: {
      onChange: registerSchema,
    },
    onSubmit: async ({ value }) => {
      await postRegister(value)
        .then((response) => {
          login(response);
          navigate({ to: "/" });
        })
        .catch((error: HttpApiError) => {
          console.log(error.problemDetails);
          notify.error(error.problemDetails.title);
        });
    },
  });

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        e.stopPropagation();
        form.handleSubmit();
      }}
    >
      <Stack spacing={2}>
        <Typography variant="h5">Register</Typography>

        <form.Field name="email">
          {(field) => (
            <FormTextField
              label="Email"
              type="email"
              field={field}
            />
          )}
        </form.Field>

        <form.Field name="password">
          {(field) => (
            <FormTextField
              label="Password"
              type="password"
              field={field}
            />
          )}
        </form.Field>

        <form.Field name="confirmPassword">
          {(field) => (
            <FormTextField
              label="Confirm Password"
              type="password"
              field={field}
            />
          )}
        </form.Field>

        <form.Subscribe
          selector={(state) => [state.canSubmit, state.isSubmitting]}
          children={([canSubmit, isSubmitting]) => (
            <Button
              type="submit"
              variant="contained"
              disabled={!canSubmit}
              loading={isSubmitting}
            >
              Register
            </Button>
          )}
        />

        <Typography variant="body2">
          Already have an account? <Link to="/auth/login">Login</Link>
        </Typography>
      </Stack>
    </form>
  );
};

export default RegisterPage;
