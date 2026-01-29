import { useEffect, useState } from "react";
import "./App.css";

type ScoreResponse = {
  teamKey: string;
  aspect: string;
  overallScore: number;
  evaluatedAt: string;
};

function App() {
  const [score, setScore] = useState<ScoreResponse | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch("http://localhost:5264/api/teams/team-orders/scores/current")
      .then((res) => {
        if (!res.ok) {
          throw new Error("Failed to fetch score");
        }
        return res.json();
      })
      .then((data) => setScore(data))
      .catch((err) => setError(err.message));
  }, []);

  return (
    <div style={{ padding: "2rem", fontFamily: "Arial" }}>
      <h1>Craftsmanship Dashboard</h1>

      {error && <p style={{ color: "red" }}>{error}</p>}

      {!score && !error && <p>Loading score...</p>}

      {score && (
        <div>
          <h2>Team: {score.teamKey}</h2>
          <p>Aspect: {score.aspect}</p>
          <p>
            <strong>Overall Score:</strong> {score.overallScore}
          </p>
          <p>
            Evaluated At:{" "}
            {new Date(score.evaluatedAt).toLocaleString()}
          </p>
        </div>
      )}
    </div>
  );
}

export default App;