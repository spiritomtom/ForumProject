import React, { useEffect, useState } from "react";
import { getUsers, toggleModerator } from "../services/api";

type User = {
  id: string;
  userName: string;
  email: string;
  roles: string[];
};

const AdminPanel: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    const response = await getUsers();
    setUsers(response.data);
  };

  const handleToggleModerator = async (id: string) => {
    await toggleModerator(id);
    fetchUsers();
  };

  return (
    <div>
      <h2>User Management</h2>
      <ul>
        {users.map(user => (
          <li key={user.id}>
            <b>{user.userName}</b> ({user.email})
            <div>
              Roles: {user.roles.join(", ")}
              <button onClick={() => handleToggleModerator(user.id)}>
                {user.roles.includes("Moderator") ? "Remove Moderator" : "Add Moderator"}
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AdminPanel;