using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var colorChips = new List<ColorChip>();
            string line;

            // Use Ctrl+Z (Windows) or Ctrl+D (Linux/Mac) to signal the end of input
            while ((line = Console.ReadLine()) != null)
            {
                // Process the line
                string[] colors = line.Trim('[', ']').Split(new[] { ", " }, StringSplitOptions.None);

                // Parse colors using Enum.TryParse
                if (Enum.TryParse(colors[0], out Color startColor) && Enum.TryParse(colors[1], out Color endColor))
                {
                    colorChips.Add(new ColorChip(startColor, endColor));
                }
                else
                {
                    Console.WriteLine($"Error: Invalid color in line '{line}'");
                    return;
                }
            }

            var solver = new GraphSolver(colorChips);
            solver.SolveSequence(Color.Blue, new HashSet<int> { }, new List<ColorChip> { });
            solver.PrintSequence();
        }
    }
}
