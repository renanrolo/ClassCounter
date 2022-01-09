using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassCounter.Models;
using Flurl;
using Flurl.Http;

namespace ClassCounter
{
    public class Program
    {
        public const string pokemonApiUrl = "https://pokeapi.co/api/v2/pokemon";

        static async Task Main(string[] args)
        {
            var pokemons = await GetPokemons();

            Console.WriteLine($"You got '{pokemons.Count}' pokemons");

            if (!pokemons.Any())
                return;

            Console.WriteLine("The pokemons you got are:");

            foreach (var pokemon in pokemons)
            {
                Console.WriteLine($"{pokemon.Name}");
            }

            Console.WriteLine("Press any key to close this");
            Console.ReadKey();
        }

        private static async Task<ICollection<Pokemon>> GetPokemons()
        {
            var result = await pokemonApiUrl.SetQueryParams(new { limit = 5 })
                                            .GetJsonAsync<PokemonApiResponse>();

            return result.Results;
        }
    }
}
