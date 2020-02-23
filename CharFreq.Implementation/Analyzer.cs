using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CharFreq.Implementation
{
    public class Analyzer : IAnalyzer
    {
        private static readonly char[]
            AllSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefjhijklmnopqrstuvwxyz".ToCharArray();

        public AnalyzeResult Analyze(string folder)
        {
            var fileNames = Directory.GetFiles(folder);
            var occurrencesInfos = new List<OccurrencesInfo>();
            var occurrencesInFiles = new Dictionary<string, Dictionary<char, float>>();
            foreach (var fileName in fileNames)
                if (fileName.EndsWith(".txt"))
                    occurrencesInFiles[fileName] = AnalyzeFile(fileName);

            foreach (var symbol in AllSymbols)
            {
                var occurrencesInfo = new OccurrencesInfo {Symbol = symbol, Occurrences = new float[fileNames.Length]};

                foreach (var (fileName, occurrencesInFile) in occurrencesInFiles)
                    if (occurrencesInFile.ContainsKey(symbol))
                        occurrencesInfo.Occurrences[Array.IndexOf(fileNames, fileName)] = occurrencesInFile[symbol];

                var occurrences = occurrencesInfo.Occurrences;
                var avg = occurrences.Average();
                var dispersion =
                    (float) Math.Sqrt(occurrences.Select(x => Math.Pow(x - avg, 2)).Sum() / occurrences.Length);
                occurrencesInfo.OccurrencesAverage = avg;
                occurrencesInfo.OccurrencesDispersion = dispersion;

                occurrencesInfos.Add(occurrencesInfo);
            }


            return new AnalyzeResult
            {
                FileNames = fileNames.Select(filename => filename.Substring(folder.Length)).ToArray(),
                OccurrencesInfos = occurrencesInfos.Where(info => info.OccurrencesAverage > 0)
                    .OrderByDescending(info => info.OccurrencesAverage).ToArray()
            };
        }

        private static Dictionary<char, float> AnalyzeFile(string fileName)
        {
            var entryCount = new Dictionary<char, float>();
            using var file = new StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
                foreach (var symbol in line.Where(char.IsLetter))
                    entryCount[symbol] = entryCount.ContainsKey(symbol) ? entryCount[symbol] + 1 : 1;

            var lettersSum = entryCount.Values.Sum();
            foreach (var symbol in entryCount.Keys.ToList())
                entryCount[symbol] /= lettersSum;

            return entryCount;
        }
    }
}