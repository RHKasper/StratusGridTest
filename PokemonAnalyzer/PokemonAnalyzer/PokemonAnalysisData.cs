using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PokemonAnalyzer
{
	public class PokemonAnalysisData
	{
		public double AvgHeight;
		public double AvgWeight;

		public Dictionary<string, double> AvgHeightByType;
		public Dictionary<string, double> AvgWeightByType;

		public PokemonAnalysisData(List<PokemonData> pokemonDataList)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			
			int heightAccumulator = 0;
			int weightAccumulator = 0;

			Dictionary<string, int> numPokemonByType = new Dictionary<string, int>();
			AvgHeightByType = new Dictionary<string, double>();
			AvgWeightByType = new Dictionary<string, double>();

			foreach (PokemonData data in pokemonDataList)
			{
				heightAccumulator += data.height;
				weightAccumulator += data.weight;

				foreach (Type type in data.types)
				{
					string typeName = type.type.name;
					if (numPokemonByType.ContainsKey(typeName) == false)
					{
						numPokemonByType.Add(typeName, 0);
						AvgHeightByType.Add(typeName, 0);
						AvgWeightByType.Add(typeName, 0);
					}

					numPokemonByType[type.type.name]++;
					
					// Use avg dictionaries as accumulators for now, then divide later.
					AvgHeightByType[typeName] += data.height;
					AvgWeightByType[typeName] += data.weight;
				}
			}

			// Finish calculation for each type 
			foreach (string typeName in numPokemonByType.Keys)
			{
				AvgHeightByType[typeName] /= (10.0 * numPokemonByType[typeName]);
				AvgWeightByType[typeName] /= (10.0 * numPokemonByType[typeName]);
			}
			

			// Note that Height and Weight come back from the PokeAPI in decimeters (meters/10) and hectograms (kg/10)
			AvgHeight = heightAccumulator / (10.0 * pokemonDataList.Count);
			AvgWeight = weightAccumulator / (10.0 * pokemonDataList.Count);
			
			Console.WriteLine($"Analysis took: {stopwatch.Elapsed.TotalSeconds} seconds");
		}
	}
}