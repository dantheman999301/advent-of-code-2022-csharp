﻿using FluentResults;

namespace AdventOfCode.Shared.Days.One;

public class DayOne : IDayBase
{
    public async Task<Result<string>> RunPartOne(Stream input)
    {
        var elves = await GetElfTotals(input);
        return Result.Ok(elves.Last().Calories.ToString());
    }

    public async Task<Result<string>> RunPartTwo(Stream input)
    {
        var elves = await GetElfTotals(input);
        return Result.Ok(elves.TakeLast(3).Sum(e => e.Calories).ToString());
    }
    
    private static async Task<List<CalorieElf>> GetElfTotals(Stream input)
    {
        using var streamReader = new StreamReader(input, leaveOpen: true);

        var elves = new List<CalorieElf>();
        var currentElf = 1;
        var runningCalorieCount = 0;

        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();

            if (int.TryParse(line, out var calories))
            {
                runningCalorieCount += calories;
                continue;
            }

            if (line is not "") continue;

            var elf = new CalorieElf(currentElf, runningCalorieCount);
            elves.Add(elf);

            currentElf++;
            runningCalorieCount = 0;
        }

        elves.Sort((elf, elf1) => elf.Calories.CompareTo(elf1.Calories));
        return elves;
    }
}
