namespace FootballBetting.Models
{
    using FootballBetting.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        [Key]
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string Id { get; set; }

        public PositionDescription Description { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
