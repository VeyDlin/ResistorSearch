using ConsoleTables;
using static ResistorSearch.Solver;

namespace ResistorSearch;


public class Utility {

    static public string TryReadLine(string hit) {
        string? line = null;
        while (line is null) {
            Console.Write(hit);
            line = Console.ReadLine();
        }

        return line;
    }





    static public void WriteInfo(List<Info> infoList, int max = 200) {
        var table = new ConsoleTable("Vout", "R1", "R2");

        foreach (var info in infoList) {
            table.AddRow(
                info.v.ToString("F" + "2".PadLeft(2, '0')).Replace(",", "."), 
                FormatResistance(info.x), 
                FormatResistance(info.y)
            );

            if (--max <= 0) {
                break;
            }
        }

        table.Write(Format.Minimal);
    }





    static public string FormatResistance(double ohms) {
        string res;

        if (ohms >= 1000.0) {
            res = $"{Math.Round(ohms / 1000.0, 2)}k";
        } else if (ohms >= 100000.0) {
            res = $"{Math.Round(ohms / 100000.0, 2)}M";
        } else {
            res = $"{Math.Round(ohms, 2)}";
        }

        return res.Replace(",", ".");
    }
}
