using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonAnalyzer
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
		
			int limit = Convert.ToInt32(args[0]);
			int offset = Convert.ToInt32(args[1]);
			
			PokemonListQuery pokemonListQuery = WebRequestManager.RequestPokemonList(limit, offset);
			PokemonData pokemonData = WebRequestManager.RequestJsonObject<PokemonData>(pokemonListQuery.results[0].url);

			Console.WriteLine($"{pokemonData.name}\nHeight: {pokemonData.height} Weight: {pokemonData.weight}");
			

			Console.WriteLine($"Program took {stopwatch.Elapsed.TotalSeconds} seconds.");
		}
	}
}