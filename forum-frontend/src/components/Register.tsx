import React, { useState } from "react";
import { register } from "../services/api";

export default function Register({ onRegister }: { onRegister: () => void }) {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [msg, setMsg] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await register(username, email, password, "User"); // Добавяне на Role с стойност "User"
      setMsg("Registered! Please log in.");
      onRegister();
    } catch (err: any) {
      if (err.response && err.response.data) {
        setMsg(JSON.stringify(err.response.data));
      } else {
        setMsg("Failed to register.");
      }
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input value={username} onChange={e => setUsername(e.target.value)} placeholder="Username" />
      <input value={email} type="email" onChange={e => setEmail(e.target.value)} placeholder="Email" />
      <input value={password} type="password" onChange={e => setPassword(e.target.value)} placeholder="Password" />
      <button type="submit">Register</button>
      <div>{msg}</div>
    </form>
  );
}