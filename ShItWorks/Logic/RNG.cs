using System;

namespace ShItWorks.Logic
{
    public static class RNG
    {
        private static Random rng = new Random();

        public static Random GetRandom() { return rng; }

        public static float Range(float min, float max)
        {
            return min + (float)rng.NextDouble() * (max-min);
        }
    }
}
