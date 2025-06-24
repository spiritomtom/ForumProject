import React, { useEffect, useState } from "react";
import { getUsers, toggleModerator, getUserInfo } from "../services/api";
import Moderation from "./Moderation";

type User = {
  id: string;
  userName: string;
  email: string;
};

type UserWithRoles = {
  user: User;
  roles: string[];
};

type CurrentUserDetails = {
  username: string;
  roles: string[];
};

const AdminPanel: React.FC = () => {
  const [users, setUsers] = useState<UserWithRoles[]>([]);
  const [currentUserDetails, setCurrentUserDetails] = useState<CurrentUserDetails | null>(null);
  const [loadingUsers, setLoadingUsers] = useState(true);
  const [loadingDetails, setLoadingDetails] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchUsers();
    fetchCurrentUserDetails();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await getUsers();
      console.log("Fetched users:", response.data);
      setUsers(response.data);
      setLoadingUsers(false);
    } catch (err) {
      console.error("Failed to fetch users:", err);
      setError("Failed to fetch users");
      setLoadingUsers(false);
    }
  };

  const fetchCurrentUserDetails = async () => {
    try {
      const response = await getUserInfo();
      console.log("Fetched current user details:", response.data);
      setCurrentUserDetails(response.data);
      setLoadingDetails(false);
    } catch (err) {
      console.error("Failed to fetch current user details:", err);
      setError("Failed to fetch current user details");
      setLoadingDetails(false);
    }
  };

  const handleToggleModerator = async (id: string) => {
    try {
      await toggleModerator(id);
      fetchUsers(); // Refresh users after toggling
    } catch (err) {
      console.error("Failed to toggle moderator role:", err);
      setError("Failed to toggle moderator role");
    }
  };

  if (loadingUsers || loadingDetails) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  const isModerator = currentUserDetails?.roles.includes("Moderator");

  return (
    <div>
      <h2>User Management</h2>
      <ul>
        {users.map(userWithRoles => (
          <li key={userWithRoles.user.id}>
            <b>{userWithRoles.user.userName}</b> ({userWithRoles.user.email})
            <div>
              Roles: {userWithRoles.roles && userWithRoles.roles.length > 0 ? userWithRoles.roles.join(", ") : "No roles"}
              <button onClick={() => handleToggleModerator(userWithRoles.user.id)}>
                {userWithRoles.roles && userWithRoles.roles.includes("Moderator") ? "Remove Moderator" : "Add Moderator"}
              </button>
            </div>
          </li>
        ))}
      </ul>

      {isModerator && (
        <div>
          <h2>Moderation</h2>
          <Moderation />
        </div>
      )}
    </div>
  );
};

export default AdminPanel;