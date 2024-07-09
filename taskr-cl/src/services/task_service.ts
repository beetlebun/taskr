import { renderMessage } from "@/utils/response_handler.js";
import axios from "axios";
import { url, headers } from "./api.js";

export const getAll = async () =>
  axios
    .get(`${url}/task`, headers())
    .then((response) => response.data)
    .catch((error) => renderMessage(error));