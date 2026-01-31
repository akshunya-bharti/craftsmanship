namespace Craftsmanship.Domain.Teams
{
    public class Team
    {
        public string TeamKey { get; }
        public string Name { get; }

        public Team(string teamKey, string name)
        {
            TeamKey = teamKey;
            Name = name;
        }
    }
}
