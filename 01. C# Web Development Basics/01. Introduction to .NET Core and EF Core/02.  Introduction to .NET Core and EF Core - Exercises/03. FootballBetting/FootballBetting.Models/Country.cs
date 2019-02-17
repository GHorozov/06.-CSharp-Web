namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ContinentId { get; set; }
        public Continent Continent { get; set; }

        public List<Town> Towns { get; set; } = new List<Town>();
        public List<CountryContinent> Continents { get; set; } = new List<CountryContinent>();
    }
}
