import { renderMessage } from "@/utils/response_handler.js";
import axios from "axios";
import { url, headers } from "./api.js";

export const getAll = async () =>
  axios
    .get(`${url}/project`, headers())
    .then((response) => response.data)
    .catch((error) => renderMessage(error));

export const post = async (name: string) =>
  axios
    .post(`${url}/project`, { name }, headers())
    .then((response) => response.data)
    .catch((error) => renderMessage(error));

export const put = async (id: string, name: string) =>
  axios
    .put(`${url}/project/${id}`, { name }, headers())
    .then((response) => response.data)
    .catch((error) => renderMessage(error));

export const remove = async (id: string) =>
  axios
    .delete(`${url}/project/${id}`, headers())
    .then((response) => response.data)
    .catch((error) => renderMessage(error));
