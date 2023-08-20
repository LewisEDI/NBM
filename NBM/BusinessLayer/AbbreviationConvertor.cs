namespace NBM.Filters;
using System.Collections.Generic;
using System.IO;

class AbbreviationConverter
{
    private Dictionary<string, string> abbreviationDict;
    private string csvFilePath = @"..\..\..\textwords.csv";

    public AbbreviationConverter()
    {
        abbreviationDict = new Dictionary<string, string>();
        LoadAbbreviationsFromCsv(csvFilePath);
    }

    private void LoadAbbreviationsFromCsv(string csvFilePath)
    {
        using (StreamReader reader = new StreamReader(csvFilePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string abbreviation = parts[0].Trim();
                    string realWords = parts[1].Trim();
                    abbreviationDict[abbreviation] = realWords;
                }
            }
        }
    }

    public string ConvertAbbreviations(string input)
    {
        foreach (KeyValuePair<string, string> entry in abbreviationDict)
        {
            input = input.Replace(entry.Key, $"<{entry.Value}>");
        }

        return input;
    }
}

