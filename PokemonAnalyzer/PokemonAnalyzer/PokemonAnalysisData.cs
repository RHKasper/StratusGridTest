using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonAnalyzer
{
	public class PokemonAnalysisData
	{
		public double AvgHeight;
		public double AvgWeight;

		public Dictionary<Type, double> AvgHeightByType;
		public Dictionary<Type, double> AvgWeightByType;

		public PokemonAnalysisData(List<PokemonData> pokemonDataList)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			
			int heightAccumulator = 0;
			int weightAccumulator = 0;

			foreach (PokemonData data in pokemonDataList)
			{
				heightAccumulator += data.height;
				weightAccumulator += data.weight;
			}

			// Note that Height and Weight come back from the PokeAPI in decimeters (meters/10) and hectograms (kg/10)
			AvgHeight = heightAccumulator / (10.0 * pokemonDataList.Count);
			AvgWeight = weightAccumulator / (10.0 * pokemonDataList.Count);
			
			Console.WriteLine($"Analysis took: {stopwatch.Elapsed.TotalSeconds} seconds");
		}
	}
}