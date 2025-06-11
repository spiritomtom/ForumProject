import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5047/api/",
  withCredentials: true,
});

// Auth
export const login = (username: string, password: string) =>
  api.post("auth/login", { username, password });

export const register = (username: string, email: string, password: string) =>
  api.post("auth/register", { username, email, password });

export const logout = () => api.post("auth/logout");

// Comments
export const getComments = () => api.get("comment");
export const postComment = (content: string) =>
  api.post("comment", { content });

export default api;