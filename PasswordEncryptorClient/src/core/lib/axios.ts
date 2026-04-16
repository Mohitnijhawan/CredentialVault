// apiClient.ts
import axios from "axios";
import { toast } from "react-toastify";
import { ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY } from "../../constants/storage-names";
import { refreshTokenlogin } from "../services/authService";

export const API_URL = "http://localhost:5139/api/";
export const apiClient = axios.create({ baseURL: API_URL });

let isRefreshing = false;
let failedQueue: any[] = [];

const processQueue = (error: any, token: string | null = null) => {
  failedQueue.forEach(prom => (error ? prom.reject(error) : prom.resolve(token)));
  failedQueue = [];
};

// 🔐 Request Interceptor
apiClient.interceptors.request.use(config => {
  const token = localStorage.getItem(ACCESS_TOKEN_KEY);
  if (token) config.headers["Authorization"] = `Bearer ${token}`;
  return config;
});

// 🔁 Response Interceptor
apiClient.interceptors.response.use(
  response => response,
  async error => {
    const originalRequest = error.config;

    // Ignore refresh-token endpoint to prevent loop
    if (originalRequest.url?.includes("refresh-token")) return Promise.reject(error);

    if (error.response?.status === 401) {
      if (originalRequest._retry) return Promise.reject(error);
      originalRequest._retry = true;

      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        }).then(token => {
          originalRequest.headers["Authorization"] = `Bearer ${token}`;
          return apiClient(originalRequest);
        }).catch(err => Promise.reject(err));
      }

      isRefreshing = true;

      try {
        const refreshToken = localStorage.getItem(REFRESH_TOKEN_KEY);
        if (!refreshToken) throw new Error("No refresh token found");

        const res = await refreshTokenlogin({ refreshToken });
        if (!res?.isSuccess) throw new Error("Refresh token failed");

        const newAccessToken = res.data.accessToken;
        const newRefreshToken = res.data.refreshToken;

        // Update localStorage
        localStorage.setItem(ACCESS_TOKEN_KEY, newAccessToken);
        localStorage.setItem(REFRESH_TOKEN_KEY, newRefreshToken);

        processQueue(null, newAccessToken);

        // Retry original request
        originalRequest.headers["Authorization"] = `Bearer ${newAccessToken}`;
        return apiClient(originalRequest);
      } catch (err) {
        processQueue(err, null);
        localStorage.removeItem(ACCESS_TOKEN_KEY);
        localStorage.removeItem(REFRESH_TOKEN_KEY);
        toast.error("Session expired. Please login again.");
        window.location.href = "/login";
        return Promise.reject(err);
      } finally {
        isRefreshing = false;
      }
    }

    return Promise.reject(error);
  }
);