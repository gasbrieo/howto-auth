import { TextField } from "@mui/material";

type FormTextFieldProps<TField extends { state: any; handleChange: (value: any) => void }> = {
  label: string;
  type?: string;
  field: TField;
};

const FormTextField = <TField extends { state: any; handleChange: (value: any) => void }>({
  label,
  type = "text",
  field,
}: FormTextFieldProps<TField>) => {
  const error = field.state.meta.errors[0]?.message;

  return (
    <TextField
      label={label}
      type={type}
      value={field.state.value}
      onChange={(e) => field.handleChange(e.target.value)}
      error={!!error}
      helperText={error}
      fullWidth
    />
  );
};

export default FormTextField;
