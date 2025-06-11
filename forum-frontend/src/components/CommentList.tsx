import { useEffect, useState } from "react";
import { getComments } from "../services/api";

type Comment = {
  id: number;
  content: string;
  createdAt: string;
  user: string;
};

export default function CommentList() {
  const [comments, setComments] = useState<Comment[]>([]);

  useEffect(() => {
    getComments().then(res => setComments(res.data));
  }, []);

  return (
    <ul>
      {comments.map(c => (
        <li key={c.id}>
          <b>{c.user}:</b> {c.content}
          <div style={{ fontSize: "smaller" }}>{new Date(c.createdAt).toLocaleString()}</div>
        </li>
      ))}
    </ul>
  );
}