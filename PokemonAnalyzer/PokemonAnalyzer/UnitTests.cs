using NUnit.Framework;

namespace PokemonAnalyzer
{
	public class UnitTests
	{
		[Test]
		public static void TestPokemonDataRequest()
		{
			PokemonData pokemonData = WebRequestManager.RequestJsonObject<PokemonData>("https://pokeapi.co/api/v2/pokemon/2/");
			Assert.IsTrue(pokemonData.name == "ivysaur");
		}
		
		[Test]
		public static void TestRequestPokemonList()
		{
			PokemonListQuery pokemonList = WebRequestManager.GetPokemonList(1, 0);
			Assert.IsTrue(pokemonList.results[0].name == "bulbasaur");
		}
	}
}