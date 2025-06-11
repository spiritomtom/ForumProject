import { useState } from "react";
import { postComment } from "../services/api";

export default function CommentForm({ onPosted }: { onPosted: () => void }) {
  const [content, setContent] = useState("");
  const [msg, setMsg] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await postComment(content);
      setMsg(res.data.message);
      setContent("");
      onPosted(); // refresh comments
    } catch {
      setMsg("Failed to post comment.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <textarea value={content} onChange={e => setContent(e.target.value)} />
      <button type="submit">Post</button>
      <div>{msg}</div>
    </form>
  );
}