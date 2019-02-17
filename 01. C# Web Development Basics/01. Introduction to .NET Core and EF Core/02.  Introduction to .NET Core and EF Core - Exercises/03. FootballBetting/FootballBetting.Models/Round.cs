namespace FootballBetting.Models
{
    using FootballBetting.Models.Enums;
    using System.Collections.Generic;

    public class Round
    {
        public int Id { get; set; }

        public RoundType Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
