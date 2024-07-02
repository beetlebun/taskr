import { jwtDecode } from "jwt-decode";
import { handleError } from "@/utils/error_handler";
import type { UserProfileToken } from "../models/User";
import axios from "axios";
import { url } from "./api";
import "vue3-toastify/dist/index.css";

export const login = async (username: string, password: string) =>
  axios
    .post<UserProfileToken>(`${url}/account/login`, {
      username,
      password,
    })
    .then((response) => {
      sessionStorage.setItem("token", response.data.token);
    })
    .catch((error) => handleError(error));

export const logout = async () => sessionStorage.removeItem("token");

export const is_logged_in = () => {
  const token = sessionStorage.getItem("token");

  if (!token) {
    return false;
  }

  try {
    const { exp: expiration } = jwtDecode(token);
    const is_expired = !!expiration && Date.now() > expiration * 1000;

    if (is_expired) {
      return false;
    }

    return true;
  } catch (error) {
    return false;
  }
};
