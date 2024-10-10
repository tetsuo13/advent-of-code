using AdventOfCode.Runner;

#if RELEASE
BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkSolutionRunner>();
#else
await new SolutionRunner().RunAsync();
#endif
