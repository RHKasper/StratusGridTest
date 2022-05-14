using System.IO;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PokemonAnalyzer
{
	public class WebRequestManager
	{
		private const string PokeApi = "https://pokeapi.co/api/v2";
		public static PokemonListQuery RequestPokemonList(int limit, int offset)
		{
			string uri = $"{PokeApi}/pokemon?limit={limit}&offset={offset}";
			return RequestJsonObject<PokemonListQuery>(uri);
		}
		
		public static T RequestJsonObject<T>(string uri)
		{
			string rawJson = RunWebRequest(uri);
			T deserializedObject = JsonConvert.DeserializeObject<T>(rawJson);
			return deserializedObject;
		}

		// Todo: find the source of this code. I got it from my BeatSaberUnzipper project, and unfortunately didn't save the link to the source
		public static string RunWebRequest(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.Proxy = null;

			using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using(Stream stream = response.GetResponseStream())
			using(StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		#region Tests
		
		[Test]
		public static void TestPokemonDataRequest()
		{
			PokemonData pokemonData = RequestJsonObject<PokemonData>("https://pokeapi.co/api/v2/pokemon/2/");
			Assert.IsTrue(pokemonData.name == "ivysaur");
		}
		
		[Test]
		public static void TestRequestPokemonList()
		{
			PokemonListQuery pokemonList = RequestPokemonList(1, 0);
			Assert.IsTrue(pokemonList.results[0].name == "bulbasaur");
		}

		#endregion
	}
}