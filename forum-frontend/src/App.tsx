import React, { useState, useEffect } from "react";
import api, { logout } from "./services/api";
import Login from "./components/Login";
import Register from "./components/Register";
import CommentList from "./components/CommentList";
import CommentForm from "./components/CommentForm";
import Moderation from "./components/Moderation";
import AdminPanel from "./components/AdminPanel";

export default function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  const [roles, setRoles] = useState<string[]>([]);
  const [refresh, setRefresh] = useState(0);
  const [showRegister, setShowRegister] = useState(false);
  const [showModeration, setShowModeration] = useState(false);

  useEffect(() => {
    api.get("auth/me")
      .then(response => {
        setLoggedIn(true);
        setRoles(response.data.roles); // Simplified role mapping
      })
      .catch(error => {
        console.error("Error fetching user info:", error);
        setLoggedIn(false);
      });
  }, []);

  const isModerator = roles.includes("Moderator");
  const isAdmin = roles.includes("Admin");

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
      <CommentForm onPosted={() => setRefresh(x => x + 1)} />
      <CommentList key={refresh} />
      <button onClick={() => {
        logout().then(() => setLoggedIn(false));
      }}>Logout</button>
      {isModerator && (
        <div>
          <button onClick={() => setShowModeration(!showModeration)}>
            {showModeration ? "Hide Moderation" : "Show Moderation"}
          </button>
          {showModeration && <Moderation />}
        </div>
      )}
      {isAdmin && <AdminPanel />}
    </div>
  );
}