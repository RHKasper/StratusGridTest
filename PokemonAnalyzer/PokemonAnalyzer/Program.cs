using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PokemonAnalyzer
{
	class Program
	{
		private const string PokeApi = "https://pokeapi.co/api/v2";
		static void Main(string[] args)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
		
			int limit = Convert.ToInt32(args[0]);
			int offset = Convert.ToInt32(args[1]);
			
			PokemonListQuery pokemonListQuery = RequestPokemonList(limit, offset);
			PokemonData pokemonData = RequestJsonObject<PokemonData>(pokemonListQuery.results[0].url);

			Console.WriteLine($"{pokemonData.name}\nHeight: {pokemonData.height} Weight: {pokemonData.weight}");
			

			Console.WriteLine($"Program took {stopwatch.Elapsed.TotalSeconds} seconds.");
		}

		static PokemonListQuery RequestPokemonList(int limit, int offset)
		{
			string uri = $"{PokeApi}/pokemon?limit={limit}&offset={offset}";
			return RequestJsonObject<PokemonListQuery>(uri);
		}
		
		static T RequestJsonObject<T>(string uri)
		{
			string rawJson = RunWebRequest(uri);
			T deserializedObject = JsonConvert.DeserializeObject<T>(rawJson);
			return deserializedObject;
		}

		// Todo: find the source of this code. I got it from my BeatSaberUnzipper project, and unfortunately didn't save the link to the source
		static string RunWebRequest(string uri)
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