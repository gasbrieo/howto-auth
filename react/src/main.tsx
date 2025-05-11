import { StrictMode } from "react";
import { createRoot } from "react-dom/client";

import App from "./App";

const start = async () => {
  if (import.meta.env.VITE_API_MOCKING === "enabled") {
    const { worker } = await import("./mocks/browser");
    await worker.start();
  }

  const rootElement = document.getElementById("root")!;
  if (!rootElement.innerHTML) {
    const root = createRoot(rootElement);
    root.render(
      <StrictMode>
        <App />
      </StrictMode>
    );
  }
};

start();
