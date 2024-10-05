using AdventOfCode.Runner;

#if RELEASE
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<BenchmarkSolutionRunner>();
#else
await new SolutionRunner().RunAsync();
#endif
