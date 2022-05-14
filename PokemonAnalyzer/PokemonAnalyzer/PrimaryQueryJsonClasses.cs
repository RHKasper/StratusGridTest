using System.Collections.Generic;

namespace PokemonAnalyzer
{
	// All classes generated with https://json2csharp.com/
	public class PokemonEntry
	{
		public string name { get; set; }
		public string url { get; set; }
	}

	public class PokemonListQuery
	{
		public int count { get; set; }
		public string next { get; set; }
		public object previous { get; set; }
		public List<PokemonEntry> results { get; set; }
	}
}