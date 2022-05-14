﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;

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
			
			RequestPokemonData(limit, offset);

			Console.WriteLine($"Program took {stopwatch.Elapsed.TotalSeconds} seconds.");
			Console.WriteLine("Press enter exit");
			Console.Read();
		}

		static string RequestPokemonData(int limit, int offset)
		{
			string uri = $"{PokeApi}/pokemon?limit={limit}&offset={offset}";
			return RunWebRequest(uri);
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
	}
}