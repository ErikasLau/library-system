import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import "./index.css";
import router from "./routes/router";
import Header from "./components/Header";

createRoot(document.getElementById("root")).render(
  <StrictMode>
      <Header />
      <RouterProvider router={router} />
  </StrictMode>
);
