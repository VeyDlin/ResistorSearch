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





    static public void WriteInfo(Solver.Info info) {
        var x = FormatResistance(info.x);
        var y = FormatResistance(info.y);
        var v = info.v.ToString("F" + "2".PadLeft(2, '0'));

        Console.WriteLine($"{v}   (R1 = {x}; R2 = {y})");
    }





    static public string FormatResistance(double ohms) {
        if (ohms >= 1000.0) {
            return $"{Math.Round(ohms / 1000.0, 2)}k";
        }
        if (ohms >= 100000.0) {
            return $"{Math.Round(ohms / 100000.0, 2)}M";
        }
        return $"{Math.Round(ohms, 2)}";
    }
}
