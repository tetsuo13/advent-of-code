# Advent of Code

[![Continuous integration](https://github.com/tetsuo13/advent-of-code/actions/workflows/ci.yml/badge.svg)](https://github.com/tetsuo13/advent-of-code/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Solutions to the [Advent of Code](https://adventofcode.com/) puzzles.

The console application will run all solutions that have puzzle inputs. Save your puzzle input file, named `input.txt`, in the same directory as the `Solution.cs` file for any years and days that you'd like to run. Launch application to dynamically locate all years and days to run that have inputs. Adding or removing `input.txt` files doesn't require rebuilding.

Use Debug configuration to run the solutions for inputs and write the answers to the console. Use Release configuration to run benchmarks for the same solutions.

Example Debug output when supplying `input.txt` files for two days in 2023:

```
--- 2023-12-02: Cube Conundrum ---
Part 1: 1234 (12 ms)
Part 2: 12345 (3 ms)

--- 2023-12-09: Mirage Maintenance ---
Part 1: 1234567890 (16 ms)
Part 2: 1234 (5 ms)
```

And the Release output summary table for the same days:

```
| Method    | Solution                       | PartToRun | Answer     | Mean       | Gen0     | Gen1    | Allocated  |
|---------- |------------------------------- |---------- |----------- |-----------:|---------:|--------:|-----------:|
| Benchmark | 2023-12-02: Cube Conundrum     | PartOne   | 1234       |   367.8 us |  54.6875 |  5.8594 |  335.73 KB |
| Benchmark | 2023-12-02: Cube Conundrum     | PartTwo   | 12345      |   375.5 us |  56.1523 |  6.8359 |  341.24 KB |
| Benchmark | 2023-12-09: Mirage Maintenance | PartOne   | 1234567890 |   992.4 us | 234.3750 | 68.3594 | 1438.23 KB |
| Benchmark | 2023-12-09: Mirage Maintenance | PartTwo   | 1234       | 1,026.3 us | 234.3750 | 64.4531 | 1438.22 KB |
```

