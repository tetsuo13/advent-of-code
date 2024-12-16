using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day09;

[PuzzleInfo(2024, 9, "Disk Fragmenter")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();
        var diskMap = PrepareDiskMap(inputLines[0]);

        return runMode switch
        {
            RunMode.PartOne => CalculateChecksum(DefragBlockMethod(diskMap)),
            RunMode.PartTwo => CalculateChecksum(DefragFileMethod(diskMap)),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static List<int?> DefragBlockMethod(List<int?> blocks)
    {
        var leftIndex = 0;
        var rightIndex = blocks.Count - 1;

        while (leftIndex <= rightIndex)
        {
            if (blocks[leftIndex] is not null)
            {
                leftIndex++;
                continue;
            }

            while (blocks[rightIndex] is null)
            {
                rightIndex--;
            }

            if (rightIndex > leftIndex)
            {
                (blocks[leftIndex], blocks[rightIndex]) = (blocks[rightIndex], null);
            }
        }

        return blocks;
    }

    private static Dictionary<int, int> MapFreeSpaceBlocks(List<int?> blocks)
    {
        var freeSpaceBlocks = new Dictionary<int, int>();
        var blockStartIndex = blocks.IndexOf(null);

        for (var i = blockStartIndex + 1; i < blocks.Count; i++)
        {
            if (blocks[i] is null)
            {
                continue;
            }

            freeSpaceBlocks.Add(blockStartIndex, i - blockStartIndex);
            blockStartIndex = blocks.IndexOf(null, i);

            if (blockStartIndex == -1)
            {
                break;
            }

            i = blockStartIndex;
        }

        return freeSpaceBlocks;
    }

    private static List<int?> DefragFileMethod(List<int?> blocks)
    {
        var freeSpaceBlocks = MapFreeSpaceBlocks(blocks);

        var leftIndex = blocks.IndexOf(null);
        var rightIndex = FindEndingIndex(blocks.Count - 1);
        var fileLength = 0;
        var lastFileId = blocks[rightIndex];

        while (rightIndex >= 0)
        {
            if (blocks[rightIndex] == lastFileId)
            {
                rightIndex--;
                fileLength++;
                continue;
            }

            // Found a whole file.

            // rightIndex points to the start of a new file block. Use a
            // temporary pointer for the file being moved. Since the blocks
            // are being moved RTL, point to the end of the file.
            var fileIndex = rightIndex + fileLength;

            var movedFile = false;

            // Try to find a span of free space LTR large enough to fit.
            foreach ((int index, int length) in freeSpaceBlocks)
            {
                // Searched all available free space blocks, none were found
                // large enough to accomodate.
                if (index >= fileIndex)
                {
                    break;
                }

                if (length < fileLength)
                {
                    continue;
                }

                for (var i = 0; i < fileLength; i++)
                {
                    (blocks[index + i], blocks[fileIndex - i]) = (blocks[fileIndex - i], null);
                }

                movedFile = true;

                // Need to update this free space block reference.
                freeSpaceBlocks.Remove(index);

                // If the file block fit didn't fit perfectly, create a new
                // free space block reference starting next to where the file
                // block ended.
                if (length != fileLength)
                {
                    freeSpaceBlocks.Add(index + fileLength, length - fileLength);
                }

                break;
            }

            if (movedFile)
            {
                leftIndex = blocks.IndexOf(null, leftIndex + 1);
            }

            rightIndex = FindEndingIndex(rightIndex);
            lastFileId = blocks[rightIndex];
            fileLength = 0;
        }

        return blocks;

        int FindEndingIndex(int i)
        {
            for (var j = i; j >= 0; j--)
            {
                if (blocks[j] is not null)
                {
                    return j;
                }
            }

            return 0;
        }
    }

    private static long CalculateChecksum(List<int?> blocks)
    {
        var checksum = 0L;

        for (var fileId = 0; fileId < blocks.Count; fileId++)
        {
            // Part two may have gaps. The first null block for part one
            // indicates the end of processing. No distinction is made here.
            if (blocks[fileId] is null)
            {
                continue;
            }

            checksum += fileId * blocks[fileId]!.Value;
        }

        return checksum;
    }

    private static List<int?> PrepareDiskMap(string diskMap)
    {
        var blocks = new List<int?>(); // TODO: byte for better memory footprint? Only need 0-9
        var insertingFreeSpaceBlock = false;
        var fileBlockId = 0;

        foreach (var digit in diskMap)
        {
            // TODO: Is this more efficient than a Convert.ToInt32() or something?
            var numBlocks = digit switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => throw new ArgumentOutOfRangeException(nameof(digit))
            };
            int? block = insertingFreeSpaceBlock ? null : fileBlockId;

            for (int j = 0; j < numBlocks; j++)
            {
                blocks.Add(block);
            }

            if (!insertingFreeSpaceBlock)
            {
                fileBlockId++;
            }

            insertingFreeSpaceBlock = !insertingFreeSpaceBlock;
        }

        return blocks;
    }
}
