using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Diplom.Core.AntiCheat.Interfaces;
using Diplom.Core.AntiCheat.Models;

namespace Diplom.Core.AntiCheat.Analyzers
{
    public class MousePatternAnalyzer : IActivityAnalyzer
    {
        private readonly double linearityThreshold = 0.98;        
        private readonly double minTotalDistance = 10000;        
        private readonly int minPointsRequired = 50;              

        public bool IsSuspicious(IEnumerable<InputEvent> events)
        {
            var mousePoints = events
                .Where(e => e.EventType == "Mouse")
                .Select(e =>
                {
                    var parts = e.Data.Split(',');
                    if (parts.Length == 2 &&
                        double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var x) &&
                        double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var y))
                    {
                        return (x, y);
                    }
                    return (double.NaN, double.NaN);
                })
                .Where(p => !double.IsNaN(p.Item1) && !double.IsNaN(p.Item2))
                .ToList();

            if (mousePoints.Count < minPointsRequired)
            {
                Console.WriteLine("⚠️ Слишком мало данных для анализа мыши.");
                return false;
            }

            double totalDistance = 0.0;
            for (int i = 1; i < mousePoints.Count; i++)
            {
                totalDistance += Distance(mousePoints[i - 1], mousePoints[i]);
            }

            if (totalDistance < minTotalDistance)
            {
                Console.WriteLine($"⚠️ Недостаточная активность мыши ({totalDistance:F1}px). Пропускаем анализ.");
                return false;
            }

            var start = mousePoints.First();
            var end = mousePoints.Last();
            double straightDistance = Distance(start, end);
            double linearity = straightDistance / totalDistance;

            Console.WriteLine($"🖱️ Mouse linearity R = {linearity:F4}, path = {totalDistance:F1}px");

            return linearity > linearityThreshold;
        }

        private double Distance((double x, double y) p1, (double x, double y) p2)
        {
            double dx = p1.x - p2.x;
            double dy = p1.y - p2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
