using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ConsoleEngine.Tools
{
    public class NumbersPairInt
    {
        public int X = 0, Y = 0;

        public NumbersPairInt(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public NumbersPairInt()
        {
            this.X = 0;
            this.Y = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPairInt operator +(NumbersPairInt a, NumbersPairInt b)
        {
            return new NumbersPairInt((int)(a.X + b.X), (int)(a.Y + b.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPairInt operator -(NumbersPairInt a, NumbersPairInt b)
        {
            return new NumbersPairInt((int)(a.X - b.X), (int)(a.Y - b.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPairInt operator *(NumbersPairInt a, NumbersPairInt b)
        {
            return new NumbersPairInt((int)(a.X * b.X), (int)(a.Y * b.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NumbersPairInt operator /(NumbersPairInt a, NumbersPairInt b)
        {
            return new NumbersPairInt((int)(a.X / b.X), (int)(a.Y / b.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NumbersPairInt a, NumbersPairInt b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NumbersPairInt a, NumbersPairInt b)
        {
            return !(a.X == b.X && a.Y == b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            if (!(other is NumbersPairInt)) return false;

            return Equals((NumbersPairInt)other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NumbersPairInt other)
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

        public NumbersPair ToNumbersPair()
        {
            return new NumbersPair(this.X, this.Y);
        }
    }
}
