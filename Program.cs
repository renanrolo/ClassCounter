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
        public const string pokemonApiBaseUrl = "https://pokeapi.co/api/v2/";
        public static Random random = new Random();

        static async Task Main(string[] args)
        {
            await InformeAboutPokemons();
            await InformeAboutPokemonsAbilities();

            await Task.Delay(5000);

            Console.WriteLine("Press any key to close this");
            Console.ReadKey();
        }


        private static async Task InformeAboutPokemonsAbilities()
        {
            var abilities = await GetPokemonsAbilities();

            Console.WriteLine($"You got '{abilities.Count}' pokemons abilities");

            if (!abilities.Any())
                return;

            Console.WriteLine("The abilities you got are:");

            var abilitiesName = abilities.Select(x => x.Name);

            var joinedAbilitiesName = string.Join(", ", abilitiesName);

            Console.WriteLine($"[{joinedAbilitiesName}]");
        }

        private static async Task InformeAboutPokemons()
        {
            var pokemons = await GetPokemons();

            Console.WriteLine($"You got '{pokemons.Count}' pokemons");

            if (!pokemons.Any())
                return;

            Console.WriteLine("The pokemons you got are:");

            var pokemonNames = pokemons.Select(x => x.Name);

            var joinedPokemonNames = string.Join(", ", pokemonNames);

            Console.WriteLine($"[{joinedPokemonNames}]");
        }

        private static async Task<ICollection<Ability>> GetPokemonsAbilities()
        {
            var randomLimitValue = random.Next(5, 10);

            var result = await pokemonApiBaseUrl.AppendPathSegment("ability")
                                                .SetQueryParams(new { limit = randomLimitValue })
                                                .GetJsonAsync<PokemonApiResponse<Ability>>();

            return result.Results;
        }

        private static async Task<ICollection<Pokemon>> GetPokemons()
        {
            var randomLimitValue = random.Next(5, 10);

            var result = await pokemonApiBaseUrl.AppendPathSegment("pokemon")
                                                .SetQueryParams(new { limit = randomLimitValue })
                                                .GetJsonAsync<PokemonApiResponse<Pokemon>>();

            return result.Results;
        }
    }
}
