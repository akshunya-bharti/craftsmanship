import "./App.css";

type AspectScore = {
  name: string;
  score: number;
};

function DashboardPage() {
  // Static mock data (Step 2 of Thinking in React)
  const overallScore = 78;
  const level = "Level 2";
  const teamName = "Payments Team";
  const evaluatedChangeset = 1145256;

  const aspects: AspectScore[] = [
    { name: "Code Quality", score: 78 },
    { name: "Test Quality", score: 85 },
    { name: "Build Stability", score: 91 },
    { name: "Architecture Health", score: 73 },
    { name: "Engineering Discipline", score: 73 }
  ];

  return (
    <div className="dashboard-container">
      <Header />
      <TeamDetails teamName={teamName} evaluatedChangeset={evaluatedChangeset}/>
      <ScoreSummary overallScore={overallScore} level={level}  />
      <AspectGrid aspects={aspects} />
    </div>
  );
}

function Header() {
  return (
    <div className="header">
      <div className="title">Craftsmanship Dashboard</div>
      <select>
        <option>Payments Team</option>
        <option>Orders Team</option>
      </select>
    </div>
  );
}

function TeamDetails({teamName, evaluatedChangeset}:{teamName: string; evaluatedChangeset: number}) {
  return (
    <div className="team-details">
        <div className="team-name">{ teamName }</div>
        <div className="evaluated-changeset">CS: { evaluatedChangeset }</div>
    </div>
  )
}

function ScoreSummary({ overallScore, level }: { overallScore: number; level: string; }) {

  const getColor = (score: number) => {
    if (score >= 85) return "#22c55e";
    if (score >= 70) return "#facc15";
    return "#ef4444";
  };

  const getSummaryText = (score: number) => {
    if (score >= 85) return "Strong engineering quality";
    if (score >= 70) return "Needs improvement in some areas";
    return "Immediate attention required";
  };

  return (
    <div className="summary-card">
      <div className="summary-label">Overall Score</div>

      <div className="overall-score" style={{ color: getColor(overallScore) }}>
        {overallScore}
      </div>

      <div className="summary-subtext">
        {level} • {getSummaryText(overallScore)}
      </div>

      <div className="progress-bar-container">
        <div
          className="progress-bar-fill"
          style={{
            width: `${overallScore}%`,
            backgroundColor: getColor(overallScore)
          }}
        />
      </div>
    </div>
  );
}

function AspectGrid({ aspects }: { aspects: AspectScore[] }) {
  return (
    <div className="aspect-grid">
      {aspects.map((aspect) => (
        <AspectCard key={aspect.name} name={aspect.name} score={aspect.score} />
      ))}
    </div>
  );
}

function AspectCard({ name, score }: { name: string; score: number }) {
  const getColor = (score: number) => {
    if (score >= 85) return "#22c55e";
    if (score >= 70) return "#facc15";
    return "#ef4444";
  };

  return (
    <div className="aspect-card">
      <div className="aspect-title">{name}</div>
      <div className="aspect-score" style={{ color: getColor(score) }}>
        {score}
      </div>
    </div>
  );
}

export default DashboardPage;
