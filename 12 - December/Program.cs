﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static Dictionary<string, List<string>> neighbours = new Dictionary<string, List<string>>();
        public static List<string> paths = new List<string>();
        public static int totalSmallCavePaths = 0;

        static void Main(string[] args)
        {
            // Read the first line of input.
            string currLine = Console.ReadLine();

            // Check when all input is read.
            while (currLine != ":q")
            {
                string[] caves = currLine.Split('-');
                if (neighbours.ContainsKey(caves[0]))
                    neighbours[caves[0]].Add(caves[1]);
                else
                    neighbours.Add(caves[0], new List<string>() { caves[1] });
                if (neighbours.ContainsKey(caves[1]))
                    neighbours[caves[1]].Add(caves[0]);
                else
                    neighbours.Add(caves[1], new List<string>() { caves[0] });

                // Read new line
                currLine = Console.ReadLine();
            }

            allPaths("start", "end");

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + paths.Count());
        }

        public static void allPaths(string start, string end)
        { 
            List<string> smallCavesVisited = new List<string>();
            List<string> visited = new List<string>();
            visited.Add(start);
            smallCavesVisited.Add(start);

            allPathsFrom(start, end, smallCavesVisited, visited, false);
        }

        public static void allPathsFrom(string curr, string end, List<string> smallCavesVisited, List<string> visited, bool visitedTwice)
        {
            // Check if this is the end node.
            if (curr == end)
            {
                string newPath = string.Join(",", visited);
                Console.WriteLine(newPath);
                paths.Add(newPath);
                return;
            }

            // Check if it is a small cave.
            if (!isAllUpper(curr))
                smallCavesVisited.Add(curr);

            // Check all the neighbours of the current cave.
            foreach (string cave in neighbours[curr]) 
            {
                if (!visitedTwice && smallCavesVisited.Contains(cave) && cave != "start")
                {
                    visited.Add(cave);
                    allPathsFrom(cave, end, smallCavesVisited, visited, true);
                    visited.RemoveAt(visited.Count - 1);

                } else if (!smallCavesVisited.Contains(cave))
                {
                    visited.Add(cave);
                    allPathsFrom(cave, end, smallCavesVisited, visited, visitedTwice);
                    visited.RemoveAt(visited.Count - 1);
                }
            }

            smallCavesVisited.Remove(curr);
        }

        public static bool isAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            return true;
        }
    }
}
