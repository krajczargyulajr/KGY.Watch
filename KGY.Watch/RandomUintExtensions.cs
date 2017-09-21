using System;

namespace KGY.Watch
{
    public static class RandomUintExtensions
    {
        public static uint Next(this Random random, uint maxValue)
        {
            return (uint)Math.Abs(random.Next((int)maxValue));
        }

        public static uint Next(this Random random, uint minValue, uint maxValue)
        {
            return (uint)Math.Abs(random.Next((int)minValue, (int)maxValue));
        }
    }
}
