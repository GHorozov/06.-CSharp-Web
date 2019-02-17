namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        public int Id { get; set; }

        [Required]
        public decimal BetMoney { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<BetGame> Games { get; set; } = new List<BetGame>();
    }
}
