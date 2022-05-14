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
			
			Console.WriteLine("\nAll Pokemon:");
			Console.WriteLine($"\tAverage Height: {Math.Round(analysis.AvgHeight,1)}m");
			Console.WriteLine($"\tAverage Weight: {Math.Round(analysis.AvgWeight,1)}Kg");

			foreach (string typeName in analysis.AvgHeightByType.Keys)
			{
				string capitalizedTypeName = char.ToUpper(typeName[0]) + typeName.Substring(1);
				Console.WriteLine($"\n{capitalizedTypeName}:");
				Console.WriteLine($"\tAverage Height: {Math.Round(analysis.AvgHeightByType[typeName],1)}m");
				Console.WriteLine($"\tAverage Weight: {Math.Round(analysis.AvgWeightByType[typeName],1)}Kg");
			}

			Console.WriteLine($"\nProgram took {stopwatch.Elapsed.TotalSeconds} seconds.");
		}
		

		private static List<PokemonData> GetPokemonDataList(List<PokemonEntry> pokemonEntries)
		{
			var pokemonUriList = pokemonEntries.Select(d => d.url).ToList();
			var pokemonDataList = WebRequestManager.GetPokemonDataCollection(pokemonUriList);
			return pokemonDataList;
		}
	}
}