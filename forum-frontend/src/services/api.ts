import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5047/api/",
  withCredentials: true,
});

// Auth endpoints
export const login = (username: string, password: string) =>
  api.post("auth/login", { username, password });

export const register = (username: string, email: string, password: string) =>
  api.post("auth/register", { username, email, password });

export const logout = () => api.post("auth/logout");

export const getUserInfo = () => api.get("auth/me");

// Comment endpoints
export const getComments = () => api.get("comment");

export const postComment = (content: string) =>
  api.post("comment", { content });

// Moderator endpoints
export const getFlaggedComments = () => api.get("moderator/flagged");

export const approveComment = (id: number) => api.post(`moderator/approve/${id}`);

export const deleteComment = (id: number) => api.post(`moderator/delete/${id}`);

// Admin endpoints
export const getUsers = () => api.get("admin/users");

export const toggleModerator = (id: string) => api.post(`admin/toggleModerator/${id}`);

export default api;