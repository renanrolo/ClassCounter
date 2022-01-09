using System.Collections.Generic;

namespace ClassCounter.Models
{
    public class PokemonApiResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public ICollection<Pokemon> Results { get; set; }
    }
}
