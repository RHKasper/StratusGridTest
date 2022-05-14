using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PokemonAnalyzer
{
	/// <summary>
	/// Takes care of all web access to the PokeAPI
	/// </summary>
	public class WebRequestManager
	{
		private const string PokeApi = "https://pokeapi.co/api/v2";
		
		public static PokemonListQuery GetPokemonList(int limit, int offset)
		{
			string uri = $"{PokeApi}/pokemon?limit={limit}&offset={offset}";
			return RequestJsonObject<PokemonListQuery>(uri);
		}
		
		/// <summary>
		/// Gets a list of PokemonData from the PokeAPI in parallel
		/// based on this answer: https://stackoverflow.com/a/4278002/9398033
		/// </summary>
		/// <param name="uris"></param>
		/// <returns></returns>
		public static List<PokemonData> GetPokemonDataCollection(List<string> uris)
		{
			ParallelOptions parallelOptions = new ParallelOptions();
			parallelOptions.MaxDegreeOfParallelism = 1000;
			List<PokemonData> dataCollection = new List<PokemonData>(uris.Count);
			
			Parallel.ForEach(uris, parallelOptions, uri =>
			{
				PokemonData result = RequestJsonObject<PokemonData>(uri);
				dataCollection.Add(result);
				//Console.WriteLine($"Got #{result.id}: {result.name}");
			});
			
			return dataCollection;
		}
		
		public static T RequestJsonObject<T>(string uri)
		{
			string rawJson = RunWebRequest(uri);
			T deserializedObject = JsonConvert.DeserializeObject<T>(rawJson);
			return deserializedObject;
		}
		

		// Todo: find the source of this code. I got it from my BeatSaberUnzipper project and unfortunately didn't save the link to the source
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
			PokemonListQuery pokemonList = GetPokemonList(1, 0);
			Assert.IsTrue(pokemonList.results[0].name == "bulbasaur");
		}

		#endregion
	}
}