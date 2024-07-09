import axios from "axios";
import { toast } from "vue3-toastify";

export const renderMessage = (message: any) => {
  if (axios.isAxiosError(message)) {
    const error = message.response;

    if (Array.isArray(error?.data.errors)) {
      // eslint-disable-next-line no-unsafe-optional-chaining
      for (const value of error?.data.errors) {
        toast.warning(value.description);
      }
    } else if (typeof error?.data.errors === "object") {
      for (const err in error?.data.errors) {
        toast.warning(error.data.errors[err][0]);
      }
    } else if (error?.data) {
      toast.warning(error.data);
    } else if (error?.status == 401) {
      toast.warning("Por favor, faça login");
    } else if (error) {
      toast.warning(error?.data);
    }
  } else {
    toast.success(message);
  }
};
