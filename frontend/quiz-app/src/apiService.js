import axios from "axios";

const baseURL =
  import.meta.env.MODE === "production"
    ? "production-url"
    : "http://localhost:5021/api";

const apiClient = axios.create({
  baseURL: baseURL,
});

export default apiClient;
