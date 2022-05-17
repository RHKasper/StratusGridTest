# Stratus Grid Technical Assessment
This is a small CLI app written in C# to download stats about Pokémon from https://pokeapi.co/ and calculate average heights and weights.

## How to Run
- Ensure you have .NET 5.0 installed as this app is Framework-Depedent.
- Download the repository
- Open your favorite command line interface (CLI) and navigate to `PokemonAnalyzer\PokemonAnalyzer\bin\Debug\net5.0\` within the downloaded repository
- Run PokemonAnalyzer.exe with 2 int parameters, limit & offset.

## Expected Results
After you run PokemonAnalyzer.exe, you should see a message indicating download has begun, wait roughly 4 seconds, then see the following output:
- Timing data for download and local processing
- Aggregate average height and weight for all the Pokémon queried, followed by averages for each Pokémon type. Pokémon of multiple types are counted in all of their types (i.e. a grass-water type gets counted in the grass averages and the water averages).
- Timing data for the whole program start-to-finish.


## Speed Optimizations
I began by downloading each Pokémon's data sequentially, but quickly realized that was impractical, so I switched to a parrallelized approach using `Parallel.ForEach()`. This improved runtime by roughly a factor of 100. 

I considered also parallelizing local data processing and potentially starting those calculations before all downloads finished, but after timing it and finding an execution time of roughly .004 seconds (.1% of the total run time), I decided not to. In a context with more computationally intensive analysis, I would likely do so.

I considered caching web request results, but chose not to since I don't expect this app to be run more than a few times.

## Unit Tests
Two Unit Tests for network and deserialization code can be found in the PokemonAnalyzer/UnitTests.cs file.

## Limitations & Issues
I intended to make this app self-contained, but when I changed the project settings to do this, it caused issues with the unit tests that I was unable to solve.

## References
https://medium.com/swlh/build-a-command-line-interface-cli-program-with-net-core-428c4c85221
https://stackoverflow.com/questions/4277844/multithreading-a-large-number-of-web-requests-in-c-sharp
