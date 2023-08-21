using ResistorSearch;


while (true) {
    Console.Clear();

    Console.WriteLine(@"
* Use ""r1"", ""r2"" and ""v"" in equality
* Equality must have a ""="" sign
* The left and right parts of equality can be swapped, it does not matter
* The case does not matter

* Examples of formulas
*   For MT3608: v = 0.6 * (1 + (r1 / r2))
*   For TPS61088: r1 = ((v - 1.204) * r2) / 1.204
");

    string formula = Utility.TryReadLine("Formula: ");

    string voltage = Utility.TryReadLine("Out voltage: ").Replace(".", ",");

    var eSeries = Utility.TryReadLine("E-series [3, 6, 12, 24, 48, 96, 192] (default 192): ");
    var resistorValues = eSeries.Trim() switch {
        "3" => ResistorValues.E3,
        "6" => ResistorValues.E6,
        "12" => ResistorValues.E12,
        "24" => ResistorValues.E24,
        "48" => ResistorValues.E48,
        "96" => ResistorValues.E96,
        "192" => ResistorValues.E192,
        _ => ResistorValues.E192
    };

    var solver = new Solver() {
        resistorValues = ResistorValues.GetRange(resistorValues)
    };



    Console.Clear();

    Console.WriteLine("Search...");

    var result = solver.SolveAll(formula, double.Parse(voltage));

    Console.Clear();


    int max = 200;
    foreach (var item in result) {
        Utility.WriteInfo(item);
        if (--max < 0) {
            break;
        }
    }

    Console.WriteLine("----------------------------------");
    Console.WriteLine("Press any key to restart");
    Console.ReadKey();
}

