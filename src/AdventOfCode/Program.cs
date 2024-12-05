using AdventOfCode.Runner;

#if RELEASE
BenchmarkDotNet.Running.BenchmarkRunner.Run<BenchmarkSolutionRunner>();
#else
new SolutionRunner().Run();
#endif
