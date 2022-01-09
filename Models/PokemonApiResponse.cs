using System.Collections.Generic;

namespace ClassCounter.Models
{
    public class PokemonApiResponse<T> where T : class
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public ICollection<T> Results { get; set; }
    }
}
