# Stratus Grid Technical Assessment

## Overview
This is a small CLI app written in C# to download stats about Pokémon from https://pokeapi.co/ and calculate average heights and weights.

## How to Run
[todo]

## Speed Optimizations
I began by downloading each Pokémon's data sequentially, but quickly realized that was impractical, so I switched to a parrallelized approach using `Parallel.ForEach()`. This improved runtime by roughly a factor of 100. 

I considered also parallelizing local data processing and potentially starting those calculations before all downloads finished, but after timing it and finding an execution time of roughly .004 seconds (.1% of the total run time), I decided not to. In a context with more computationally intensive analysis, I would likely do so.

## Unit Tests
Two Unit Tests for network and deserialization code can be found in the UnitTests.cs file.

## References
https://medium.com/swlh/build-a-command-line-interface-cli-program-with-net-core-428c4c85221
https://stackoverflow.com/questions/4277844/multithreading-a-large-number-of-web-requests-in-c-sharp
