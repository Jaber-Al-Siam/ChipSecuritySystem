using System;
using System.Collections.Generic;
using System.Text;

namespace ChipSecuritySystem
{
    public class GraphSolver
    {
        private readonly List<ColorChip> _colorChips;
        private List<ColorChip> _maxChipSequence;
        private Dictionary<Color, List<int>> _graph;

        public GraphSolver(List<ColorChip> colorChips) 
        {
            _colorChips = colorChips;
            _maxChipSequence = new List<ColorChip>();
            ConstructGraph();
        }

        private void ConstructGraph()
        {
            _graph = new Dictionary<Color, List<int>>();

            for (int i = 0; i < _colorChips.Count; i++)
            {
                if (!_graph.ContainsKey(_colorChips[i].StartColor))
                {
                    _graph.Add(_colorChips[i].StartColor, new List<int>());
                }

                _graph[_colorChips[i].StartColor].Add(i);
            }
        }

        public void SolveSequence(Color currentColor, HashSet<int> usedChipIndex, List<ColorChip> chipSequence)
        {
            if (currentColor == Color.Green)
            {
                if (chipSequence.Count > _maxChipSequence.Count)
                {
                    _maxChipSequence = new List<ColorChip>(chipSequence);
                }
            }

            if (usedChipIndex.Count == _colorChips.Count)
            {
                // No ColorChip left
                return;
            }

            if(_graph.ContainsKey(currentColor))
            {
                foreach (var chipIndex in _graph[currentColor])
                {
                    if (!usedChipIndex.Contains(chipIndex))
                    {
                        usedChipIndex.Add(chipIndex);
                        chipSequence.Add(_colorChips[chipIndex]);
                        SolveSequence(_colorChips[chipIndex].EndColor, usedChipIndex, chipSequence);
                        usedChipIndex.Remove(chipIndex);
                        chipSequence.RemoveAt(chipSequence.Count - 1);
                    }
                }
            }
        }

        public void PrintSequence()
        {
            if (_maxChipSequence.Count == 0)
            {
                Console.WriteLine(Constants.ErrorMessage);
                return;
            }

            var sb = new StringBuilder("Blue");

            foreach (var colorChip in _maxChipSequence)
            {
                sb.Append( $" [{colorChip}]");
            }

            sb.Append(" Green");
            Console.WriteLine(sb.ToString());
        }
    }
}
