import React, { useState, useEffect } from "react";
import api, { logout } from "./services/api";
import Login from "./components/Login";
import Register from "./components/Register";
import CommentList from "./components/CommentList";
import CommentForm from "./components/CommentForm";

export default function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  const [refresh, setRefresh] = useState(0);
  const [showRegister, setShowRegister] = useState(false);

  useEffect(() => {
    api
      .get("auth/me")
      .then(() => setLoggedIn(true))
      .catch(() => setLoggedIn(false));
  }, []);

  if (!loggedIn) {
    return showRegister ? (
      <Register onRegister={() => setShowRegister(false)} />
    ) : (
      <>
        <Login onLogin={() => setLoggedIn(true)} />
        <button onClick={() => setShowRegister(true)}>Register</button>
      </>
    );
  }

  return (
    <div>
      <h2>Comments</h2>
      <CommentForm onPosted={() => setRefresh((x) => x + 1)} />
      <CommentList key={refresh} />
      <button
        onClick={() => {
          logout().then(() => setLoggedIn(false));
        }}
      >
        Logout
      </button>
    </div>
  );
}
