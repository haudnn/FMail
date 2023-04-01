namespace Workdo.Helpers;
using System.Collections.Generic;
using System;

public class GenerateColorHelper
{
    public static string RandomColor() {
        List<string> colors = new List<string>();
        colors.Add("#ffcdd2");
        colors.Add("#d32f2f");
        colors.Add("#b71c1c");
        colors.Add("#fce4ec");
        colors.Add("#d81b60");
        colors.Add("#ff80ab");
        colors.Add("#ce93d8");
        colors.Add("#4a148c");
        colors.Add("#9fa8da");
        colors.Add("#1a237e");
        colors.Add("#82b1ff");
        colors.Add("#43a047");
        colors.Add("#69f0ae");
        colors.Add("#f57f17");
        colors.Add("#6d4c41");
        Random random = new Random();
        int index = random.Next(0, colors.Count);
        return colors[index];
    }
}