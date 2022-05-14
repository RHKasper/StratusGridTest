using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PokemonAnalyzer
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
		
			int limit = Convert.ToInt32(args[0]);
			int offset = Convert.ToInt32(args[1]);
			
			PokemonListQuery pokemonListQuery = WebRequestManager.GetPokemonList(limit, offset);
			List<PokemonData> pokemonDataList = GetPokemonDataList(pokemonListQuery.results);
			
			Console.WriteLine($"Download took {stopwatch.Elapsed.TotalSeconds} seconds.");

			PokemonAnalysisData analysis = new PokemonAnalysisData(pokemonDataList);
			
			Console.WriteLine($"Avg Height: {Math.Round(analysis.AvgHeight,1)}m");
			Console.WriteLine($"Avg Weight: {Math.Round(analysis.AvgWeight,1)}Kg");

			Console.WriteLine($"Program took {stopwatch.Elapsed.TotalSeconds} seconds.");
		}
		

		private static List<PokemonData> GetPokemonDataList(List<PokemonEntry> pokemonEntries)
		{
			// Get data from web api
			var pokemonUriList = pokemonEntries.Select(d => d.url).ToList();
			var pokemonDataList = WebRequestManager.GetPokemonDataCollection(pokemonUriList);
			return pokemonDataList;
		}
	}
}