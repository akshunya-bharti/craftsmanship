import { useEffect, useState } from "react";
import "./App.css";

type Team = {
  teamKey: string;
  name: string;
};

type ScoreResponse = {
  teamKey: string;
  aspect: string;
  overallScore: number;
  evaluatedAt: string;
  subScores: Record<string, number>;
};

function App() {
  const [teams, setTeams] = useState<Team[]>([]);
  const [selectedTeamKey, setSelectedTeamKey] = useState<string>("");
  const [score, setScore] = useState<ScoreResponse | null>(null);
  const [message, setMessage] = useState<string>("");

   // Fetch teams on load
  useEffect(() => {
    fetch("http://localhost:5264/api/teams")
      .then(res => res.json())
      .then(data => setTeams(data));
  }, []);

  // Fetch score when selected team changes
  useEffect(() => {
    if (!selectedTeamKey) return;

    setScore(null);
    setMessage("");

    fetch(`http://localhost:5264/api/teams/${selectedTeamKey}/scores/current`)
      .then(res => {
        if (res.status === 404) {
          setMessage("No scans available for the team to calculate scores");
          return null;
        }
        return res.json();
      })
      .then(data => {
        if (data) setScore(data);
      })
      .catch(() =>
        setMessage("Unable to fetch score data")
      );
  }, [selectedTeamKey]);

  return (
    <div style={{ padding: "2rem", fontFamily: "Arial" }}>
      <h1>Craftsmanship Dashboard</h1>

      <label>
        Select Team:&nbsp;
        <select
          value={selectedTeamKey}
          onChange={(e) => setSelectedTeamKey(e.target.value)}
        >
          <option value="">-- Select a team --</option>
          {teams.map(team => (
            <option key={team.teamKey} value={team.teamKey}>
              {team.name}
            </option>
          ))}
        </select>
      </label>

      <hr />

      {message && <p>{message}</p>}

      {score && (
        <div>
          <h2>Team: {score.teamKey}</h2>
          <p>Aspect: {score.aspect}</p>

          <h3>Overall Score: {score.overallScore}</h3>

          <h4>Sub Scores</h4>
          <ul>
            {Object.entries(score.subScores).map(([key, value]) => (
              <li key={key}>
                {key}: <strong>{value}</strong>
              </li>
            ))}
          </ul>

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