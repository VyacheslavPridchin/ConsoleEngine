using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ConsoleEngine.Tools
{
    public class NumbersPair
    {
        public double X = 0, Y = 0;

        public NumbersPair(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public NumbersPair()
        {
            this.X = 0;
            this.Y = 0;
        }

        public NumbersPair(NumbersPair NumbersPair)
        {
            this.X = NumbersPair.X;
            this.Y = NumbersPair.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPair operator +(NumbersPair a, NumbersPair b)
        {
            return new NumbersPair(a.X + b.X, a.Y + b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPair operator -(NumbersPair a, NumbersPair b)
        {
            return new NumbersPair(a.X - b.X, a.Y - b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPair operator *(NumbersPair a, NumbersPair b)
        {
            return new NumbersPair(a.X * b.X, a.Y * b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPair operator /(NumbersPair a, NumbersPair b)
        {
            return new NumbersPair(a.X / b.X, a.Y / b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NumbersPair a, NumbersPair b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NumbersPair a, NumbersPair b)
        {
            return !(a.X == b.X && a.Y == b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            if (!(other is NumbersPair)) return false;

            return Equals((NumbersPair)other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NumbersPair other)
        {
            return this == other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2);
        }

        public override string ToString()
        {
            return X.ToString(null, null) + " " + Y.ToString(null, null);
        }

        public string ToString(string format)
        {
            return X.ToString(format, null) + " " + Y.ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return X.ToString(format, formatProvider) + " " + Y.ToString(format, formatProvider);
        }
    }
}
