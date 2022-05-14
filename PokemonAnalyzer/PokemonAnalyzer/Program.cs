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
			
			GetAvgHeightAndWeight(pokemonDataList, out double avgHeight, out double avgWeight);
			
			Console.WriteLine($"Avg Height: {Math.Round(avgHeight,1)}m");
			Console.WriteLine($"Avg Weight: {Math.Round(avgWeight,1)}Kg");

			Console.WriteLine($"Program took {stopwatch.Elapsed.TotalSeconds} seconds.");
		}
		
				
		/// <summary>
		/// Calculates the average height and weight of all pokemon in a list of <see cref="PokemonData"/>. 
		/// Note that Weight comes back from the PokeAPI in hectograms (kg/10)
		/// and height comes back from the PokeAPI in decimeters (meters/10)
		/// </summary>
		/// <returns>Height in Meters and weight in Kg.</returns>
		static void GetAvgHeightAndWeight(List<PokemonData> pokemonDataList, out double avgHeight, out double avgWeight)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			
			int heightAccumulator = 0;
			int weightAccumulator = 0;
			
			foreach (PokemonData data in pokemonDataList)
			{
				heightAccumulator += data.height;
				weightAccumulator += data.weight;
			}

			avgHeight = heightAccumulator / (10.0 * pokemonDataList.Count);
			avgWeight = weightAccumulator / (10.0 * pokemonDataList.Count);
			//Console.WriteLine($"Calculating avg height and weight took: {stopwatch.Elapsed.TotalSeconds} seconds");
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