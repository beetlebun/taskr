export const headers = () => {
  return {
    headers: {
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
  };
};

export const url = "http://localhost:5032/api";
