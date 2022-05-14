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
			
			Console.WriteLine($"Average Height: {Math.Round(analysis.AvgHeight,1)}m");
			Console.WriteLine($"Average Weight: {Math.Round(analysis.AvgWeight,1)}Kg");
			Console.WriteLine();

			foreach (string typeName in analysis.AvgHeightByType.Keys)
			{
				Console.WriteLine($"Average {typeName}-Type Height: {Math.Round(analysis.AvgHeightByType[typeName],1)}m");
				Console.WriteLine($"Average {typeName}-Type Weight: {Math.Round(analysis.AvgWeightByType[typeName],1)}Kg");
				Console.WriteLine();
			}

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