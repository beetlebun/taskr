import { handleError } from "@/utils/error_handler";
import axios from "axios";
import { url, headers } from "./api.js";

export const getAll = async () =>
  axios
    .get(`${url}/project`, headers())
    .then((response) => response.data)
    .catch((error) => handleError(error));

// export const getById = async (id: string) => {
//   try {
//     const data = await axios.get(`${api}/project/${id}`);
//     return data.data;
//   } catch (error) {
//     handleError(error);
//   }
// };
