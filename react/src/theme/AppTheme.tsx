import { FC, ReactNode, useMemo } from "react";

import { createTheme, ThemeProvider } from "@mui/material/styles";

interface AppThemeProps {
  children: ReactNode;
}

const AppTheme: FC<AppThemeProps> = ({ children }) => {
  const theme = useMemo(() => createTheme(), []);

  return (
    <ThemeProvider
      disableTransitionOnChange
      theme={theme}
    >
      {children}
    </ThemeProvider>
  );
};

export default AppTheme;
