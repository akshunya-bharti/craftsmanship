namespace Craftsmanship.Api.Models
{
    public class CreateTeamRequest
    {
        public string TeamKey { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
