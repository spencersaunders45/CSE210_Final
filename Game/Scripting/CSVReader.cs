namespace CSE210_Final.Game.Scripting;

public class CSVReader
{
    public string[][] ReadCSV(String path)
    {
        StreamReader sr = new StreamReader(path);
        var lines = new List<string[]>();
        int Row = 0;
        while (!sr.EndOfStream)
        {
            string[] Line = sr.ReadLine().Split(',');
            lines.Add(Line);
            Row++;
            Console.WriteLine(Row);
        }

        var data = lines.ToArray();

        return data;
    }

    public bool CheckList(string[] list, string input)
    {
        foreach (string str in list)
        {
            if (input == str)
                return true;
        }

        return false;
    }
}