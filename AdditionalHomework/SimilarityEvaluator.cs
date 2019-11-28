﻿using System;
using System.Globalization;

namespace AdditionalHomework
{
    internal static class SimilarityEvaluator
    {
        public static string GetPercent(string source, string target)
        {
            var percent = CalculateProbableSimilarity(source, target);
            return percent.ToString("P");
        }

        private static double CalculateProbableSimilarity(string source, string target)
        {
            if (source == null || target == null) return 0;
            if (source.Length == 0 || target.Length == 0) return 0;
            if (source == target) return 1;

            var stepsToSame = ComputeLevenshteinDistance(source, target);
            return 1 - (stepsToSame / (double)Math.Max(source.Length, target.Length));
        }

        // Returns the number of steps required to transform the source string
        // into the target string.
        private static int ComputeLevenshteinDistance(string source, string target)
        {
            // Preparations
            source = source.ToLower();
            target = target.ToLower();

            var sourceWordCount = source.Length;
            var targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
            {
                return targetWordCount;
            }

            if (targetWordCount == 0)
            {
                return sourceWordCount;
            }

            // Creating measurement table
            var distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (var i = 0; i <= sourceWordCount;)
            {
                distance[i, 0] = i++;
            }

            for (var j = 0; j <= targetWordCount;)
            {
                distance[0, j] = j++;
            }

            for (var i = 1; i <= sourceWordCount; i++)
            {
                for (var j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                                              distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }
    }
}