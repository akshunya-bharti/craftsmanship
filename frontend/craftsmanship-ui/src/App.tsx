import "./App.css";

type AspectScore = {
  name: string;
  score: number;
};

function DashboardPage() {
  // Static mock data (Step 2 of Thinking in React)
  const overallScore = 78;
  const level = "Level 2";

  const aspects: AspectScore[] = [
    { name: "Code Quality", score: 78 },
    { name: "Test Quality", score: 85 },
    { name: "Build Stability", score: 91 },
    { name: "Architecture Health", score: 73 }
  ];

  return (
    <div className="dashboard-container">
      <Header />
      <ScoreSummary overallScore={overallScore} level={level} />
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

function ScoreSummary({ overallScore, level }: { overallScore: number; level: string }) {
  return (
    <div className="summary-card">
      <div className="overall-score">{overallScore}</div>
      <div className="level-badge">{level}</div>
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
