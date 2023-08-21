using AngouriMath;
using ResistorSearch;
using static AngouriMath.Entity.Set;





//Entity expr = "x = ((v - 1.204) * y) / 1.204";


//if (expr.Solve("v") is FiniteSet finiteSet) {
//    var solutions = finiteSet.Elements.First();

//    var f = solutions.Compile("x", "y");

//    Console.WriteLine(f.Call(360, 56).Real);
//}






var values = ResistorValues.GetRange(ResistorValues.E24);
//var formula = "0.6 * (1 + (r1 / r2)) = 12";
var formula = "r1 = ((12 - 1.204) * r2) / 1.204";




var solver = new Solver() { resistorValues = values };

solver.Search(formula);



//var result = Searcher.Search(values, formula, reference, maxResults);

//foreach (var item in result) {

//    var x = Math.Round(item.result, 2);
//    var s = x.ToString("F" + "2".PadLeft(2, '0'));
//    Console.WriteLine($"{s}   (R1 = {item.r1}; R2 = {item.r2})");
//}



Console.ReadKey();


