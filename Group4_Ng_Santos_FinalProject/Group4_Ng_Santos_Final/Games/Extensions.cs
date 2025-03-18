using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public static class Extensions // Extension class for Random class
    {
        public static float NextFloat(this Random rand, float minValue, float maxValue) // Method to generate random float values
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
