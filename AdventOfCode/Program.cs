﻿using AdventOfCode;
using AdventOfCode.Calendar;

// TODO: Get these as commandline arguments
var year = "2023";
var day = "04";

// Each day's solution uses a class named Solution but under a unique
// namespace. The way to instantiate the specified day is to use its
// namespace.

var ns = new List<string>
{
    nameof(AdventOfCode),
    nameof(AdventOfCode.Calendar),
    $"_{year}",
    $"Day{day}",
    nameof(AdventOfCode.Calendar._2023.Day01.Solution)
};

var className = string.Join(".", ns);
var type = Type.GetType(className) ?? throw new NullReferenceException($"Could not find type {className}");
var solution = Activator.CreateInstance(type) as BaseSolution ?? throw new NullReferenceException();

Console.WriteLine("Part 1: {0}", await solution.Run(RunMode.PartOne));
Console.WriteLine("Part 2: {0}", await solution.Run(RunMode.PartTwo));
