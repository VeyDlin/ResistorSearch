﻿using AngouriMath;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace ResistorSearch;


public class Solver {
    public struct Info {
        public double r1;
        public double r2;
        public double result;
        public Info(double r1, double r2, double result) {
            this.r1 = r1; 
            this.r2 = r2; 
            this.result = result;
        }
    }

    public required List<double> resistorValues { get; set; }





    public List<Info> Search(string formula) {
        formula = formula.Replace("r1", "x").Replace("r2", "y");



       



        return new();
    }





    private List<Info> SolveAll(Entity formula) {
        ConcurrentBag<Info> resultsBag = new();

        resistorValues.AsParallel().ForAll(r1 => {
            var searchFn = formula.Compile("x", "y");

            foreach (var r2 in resistorValues) {
                var result = searchFn.Call(r1, r2).Real;
                resultsBag.Add(new(r1, r2, result));
            }
        });

        return resultsBag.ToList();
    }





    private List<Info> Sort(List<Info> values, double reference) {
        return values
            .OrderBy(p => Math.Abs(p.result - reference))
            .ToList();
    }





    private string GetType(string side) {
        side = side.Trim().ToLower();

        if (side == "x" || side == "y") {
            return "xy";
        }

        if (double.TryParse(side, out double _)) {
            return "int";
        }

        return "unknown";
    }



}