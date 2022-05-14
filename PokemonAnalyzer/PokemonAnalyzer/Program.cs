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
			GetAvgHeightAndWeight(pokemonListQuery.results, out double avgHeight, out double avgWeight);
			
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
		static void GetAvgHeightAndWeight(List<PokemonEntry> pokemonEntries, out double avgHeight, out double avgWeight)
		{
			var pokemonUriList = pokemonEntries.Select(d => d.url).ToList();
			
			int heightAccumulator = 0;
			int weightAccumulator = 0;
			
			var pokemonDataList = WebRequestManager.GetPokemonDataCollection(pokemonUriList);
			foreach (PokemonData data in pokemonDataList)
			{
				heightAccumulator += data.height;
				weightAccumulator += data.weight;
			}

			avgHeight = heightAccumulator / (10.0 * pokemonEntries.Count);
			avgWeight = weightAccumulator / (10.0 * pokemonEntries.Count);
		}
	}
}