import React, { useEffect, useState } from "react";
import { getFlaggedComments, approveComment, deleteComment } from "../services/api";

type Comment = {
  id: number;
  content: string;
  user: { userName: string };
  createdAt: string;
};

const Moderation: React.FC = () => {
  const [flaggedComments, setFlaggedComments] = useState<Comment[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchFlaggedComments();
  }, []);

  const fetchFlaggedComments = async () => {
    setLoading(true);
    const response = await getFlaggedComments();
    setFlaggedComments(response.data);
    setLoading(false);
  };

  const handleApprove = async (id: number) => {
    setLoading(true);
    await approveComment(id);
    fetchFlaggedComments();
  };

  const handleDelete = async (id: number) => {
    setLoading(true);
    await deleteComment(id);
    fetchFlaggedComments();
  };

  return (
    <div>
      <h2>Flagged Comments</h2>
      {loading && <p>Loading...</p>}
      <ul>
        {flaggedComments.map(comment => (
          <li key={comment.id}>
            <b>{comment.user.userName}</b>: {comment.content}
            <div style={{ fontSize: "smaller" }}>{new Date(comment.createdAt).toLocaleString()}</div>
            <button onClick={() => handleApprove(comment.id)}>Approve</button>
            <button onClick={() => handleDelete(comment.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Moderation;