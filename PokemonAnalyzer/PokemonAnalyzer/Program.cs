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

			int limit = args.Length > 0 ? Convert.ToInt32(args[0]) : 1000000;
			int offset = args.Length > 1 ? Convert.ToInt32(args[1]) : 0;

			Console.WriteLine($"Downloading pokemon data from PokeAPI. limit = {limit}; offset = {offset}");

			try
			{
				PokemonListQuery pokemonListQuery = WebRequestManager.GetPokemonList(limit, offset);
				List<PokemonData> pokemonDataList = GetPokemonDataList(pokemonListQuery.results);

				Console.WriteLine($"Download took {stopwatch.Elapsed.TotalSeconds} seconds.");

				PokemonAnalysisData analysis = new PokemonAnalysisData(pokemonDataList);

				Console.WriteLine("\nAll Pokemon:");
				Console.WriteLine($"\tAverage Height: {Math.Round(analysis.AvgHeight, 1)}m");
				Console.WriteLine($"\tAverage Weight: {Math.Round(analysis.AvgWeight, 1)}Kg");

				foreach (string typeName in analysis.AvgHeightByType.Keys)
				{
					string capitalizedTypeName = char.ToUpper(typeName[0]) + typeName.Substring(1);
					Console.WriteLine($"\n{capitalizedTypeName}:");
					Console.WriteLine($"\tAverage Height: {Math.Round(analysis.AvgHeightByType[typeName], 1)}m");
					Console.WriteLine($"\tAverage Weight: {Math.Round(analysis.AvgWeightByType[typeName], 1)}Kg");
				}
			}
			catch
			{
				Console.WriteLine("Downloading or processing data failed");
				throw;
			}

			Console.WriteLine($"\nProgram took {stopwatch.Elapsed.TotalSeconds} seconds.");

			Console.WriteLine("Press Enter to exit");
			Console.ReadLine();
		}


		private static List<PokemonData> GetPokemonDataList(List<PokemonEntry> pokemonEntries)
		{
			var pokemonUriList = pokemonEntries.Select(d => d.url).ToList();
			var pokemonDataList = WebRequestManager.GetPokemonDataCollection(pokemonUriList);
			return pokemonDataList;
		}
	}
}