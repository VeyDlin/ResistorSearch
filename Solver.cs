using AngouriMath;
using AngouriMath.Core;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using static AngouriMath.Entity.Set;

namespace ResistorSearch;


public class Solver {
    public struct Info {
        public double x;        // R1
        public double y;        // R2
        public double v;        // Voltage
        public Info(double x, double y, double v) {
            this.x = x; 
            this.y = y;
            this.v = v;
        }
    }

    public required List<double> resistorValues { get; set; }





    public List<Info> SolveAll(string formula, double v) {
        (var main, var equality) = NormalizeFormula(formula);

        var solveList = GetSolvedList(main, equality, v);
        var sortedList = Sort(solveList, v);

        return sortedList;
    }





    private List<Info> GetSolvedList(string mainPart, string equalityPart, double v) {
        ConcurrentBag<Info> resultsBag = new();
        Entity mainFormula = mainPart;

        Entity fullFormula = $"{equalityPart} = {mainPart}";
        Entity voltageFormula = ((FiniteSet)fullFormula.Solve("v")).Elements.First();

        resistorValues.AsParallel().ForAll(r1 => {
            var mainFn = mainFormula.Compile("x", "y", "v");
            var voltageFn = voltageFormula.Compile("x", "y");

            foreach (var r2 in resistorValues) {
                double mainSolve = mainFn.Call(r1, r2, v).Real;
                double voltageSolve = mainSolve;

                if (equalityPart != "v") {
                    voltageSolve = voltageFn.Call(r1, r2).Real;
                }

                resultsBag.Add(new(r1, r2, voltageSolve));
            }
        });

        return resultsBag.ToList();
    }





    private List<Info> Sort(List<Info> values, double v) {
        return values
            .OrderBy(p => Math.Abs(p.v - v))
            .ToList();
    }





    private (string, string) NormalizeFormula(string formula) {
        formula = formula.ToLower().Replace("r1", "x").Replace("r2", "y");


        var sides = formula
            .Split('=')
            .Select(c => c.Trim())
            .ToArray();

        if (sides.Count() != 2) {
            throw new Exception("Incorrect formula");
        }


        bool IsMainPart(string side) => !Regex.IsMatch(side, @"^[xyv]$");
        bool[] types = { 
            IsMainPart(sides[0]),
            IsMainPart(sides[1]) 
        };

        if (types.Count(c => c) != 1) {
            throw new Exception("Incorrect formula");
        }


        return types[0] ? (sides[0], sides[1]) : (sides[1], sides[0]);
    }



}
