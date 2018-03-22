using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace SyncVsAsync
{

    /// <summary>
    /// Pi Calculator. 
    /// Algorithm shamelessly stolen and translated/refactored to vb.net from this Blog:
    /// http://latkin.org/blog/2012/11/03/the-bailey-borwein-plouffe-algorithm-in-c-and-f/
    /// </summary>
    public sealed class PiCalc
    {

        private const int NumHexDigits = 16;
        private const double Epsilon = 1.0E-17;
        private const int NumTwoPowers = 25;
        private const int PORTIONS_SIZE = 6;

        private static double[] twoPowers = new double[NumTwoPowers];

        private Dictionary<string, int> hexChars = new Dictionary<string, int>()
    {
        {"0", 0},
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"A", 10},
        {"B", 11},
        {"C", 12},
        {"D", 13},
        {"E", 14},
        {"F", 15}
    };

        private Action<int, string> myProgressCallback;

        public PiCalc()
        {
            InitializeTwoPowers();
        }

        public string CalculatePiHex(int digitPosition, int numHexDigits)
        {

            double pid = 0;
            double s1 = 0;
            double s2 = 0;
            double s3 = 0;
            double s4 = 0;
            string hexDigits = null;


            //  Digits generated follow immediately after digitPosition.
            s1 = Series(1, digitPosition);
            s2 = Series(4, digitPosition);
            s3 = Series(5, digitPosition);
            s4 = Series(6, digitPosition);

            pid = 4.0D * s1 - 2.0D * s2 - s3 - s4;
            pid = pid - Convert.ToInt32((int)(pid)) + 1.0D;
            hexDigits = ConvertToHexDigits(pid, numHexDigits);
            return hexDigits;
        }

        // Returns the first NumHexDigits hex digits of the fraction of x.
        private static string ConvertToHexDigits(double x, int numDigits)
        {
            string hexChars = "0123456789ABCDEF";
            StringBuilder sb = new StringBuilder(numDigits);
            double y = Math.Abs(x);

            for (int i = 0; i < numDigits; i++)
            {
                y = 16.0D * (y - Math.Floor(y));
                sb.Append(hexChars[(int)(y)]);
            }

            return sb.ToString();
        }

        // This routine evaluates the series  sum_k 16^(n-k)/(8*k+m) 
        //    using the modular exponentiation technique.
        private static double Series(int m, int n)
        {
            double denom = 0;
            double pow = 0;
            double sum = 0D;
            double term = 0;

            //  Sum the series up to n.
            for (int k = 0; k < n; k++)
            {
                denom = 8 * k + m;
                pow = n - k;
                term = ModPow16(pow, denom);
                sum = sum + term / denom;
                sum = sum - Convert.ToInt32((int)(sum));
            }

            //  Compute a few terms where k >= n.
            for (int k = n; k <= n + 100; k++)
            {
                denom = 8 * k + m;
                term = Math.Pow(16.0D, (double)(n - k)) / denom;

                if (term < Epsilon)
                {
                    break;
                }

                sum = sum + term;
                sum = sum - (int)(sum);
            }

            return sum;
        }

        // Fill the power of two table
        private static void InitializeTwoPowers()
        {
            twoPowers[0] = 1.0D;

            for (int i = 1; i < NumTwoPowers; i++)
            {
                twoPowers[i] = 2.0D * twoPowers[i - 1];
            }
        }

        // ModPow16 = 16^p mod m.  This routine uses the left-to-right binary 
        //  exponentiation scheme.
        private static double ModPow16(double p, double m)
        {
            int i = 0;
            double pow1 = 0;
            double pow2 = 0;
            double result = 0;

            if (m == 1.0D)
            {
                return 0D;
            }

            // Find the greatest power of two less than or equal to p.
            for (i = 0; i < NumTwoPowers; i++)
            {
                if (twoPowers[i] > p)
                {
                    break;
                }
            }

            pow2 = twoPowers[i - 1];
            pow1 = p;
            result = 1.0D;

            // Perform binary exponentiation algorithm modulo m.
            for (int j = 1; j <= i; j++)
            {
                if (pow1 >= pow2)
                {
                    result = 16.0D * result;
                    result = result - (int)(result / m) * m;
                    pow1 = pow1 - pow2;
                }

                pow2 = 0.5 * pow2;

                if (pow2 >= 1.0D)
                {
                    result = result * result;
                    result = result - (int)(result / m) * m;
                }
            }
            return result;
        }

        public string CalculatePiDecimalString(int digitsCount, IProgress<Tuple<int, int>> progress = null)
        {

            //We can only precisly calculte a limitted number of digits in one go,
            //so we portion the floating point digits in chunks of n (PORTION_SIZE).
            var portions = digitsCount / PORTIONS_SIZE;
            var lastRunDigitsCount = digitsCount % PORTIONS_SIZE;
            var currentDigitsCount = 0;
            StringBuilder sb = new StringBuilder();
            int updatecounter = 0;

            var returnValue = new System.Numerics.BigInteger();

            for (var portionsCount = 0; portionsCount <= portions; portionsCount++)
            {
                var thisRunDigitsCount = (portionsCount == portions) ? lastRunDigitsCount : PORTIONS_SIZE;

                var piHex = CalculatePiHex(currentDigitsCount, thisRunDigitsCount);

                for (var count = 0; count < piHex.Length; count++)
                {
                    returnValue += hexChars[piHex[count].ToString()];
                    returnValue = returnValue << 4;
                }

                currentDigitsCount += thisRunDigitsCount;
                updatecounter += 1;

                if (updatecounter == 10)
                {
                    updatecounter = 0;
                    if (progress != null)
                    {
                        progress.Report(new Tuple<int, int>(portionsCount * 1000 / portions, portionsCount * PORTIONS_SIZE));
                    }
                }

            }
            returnValue = returnValue >> 4;

            var decimalDigits = returnValue.ToString().Length;

            for (var count = 1; count <= decimalDigits; count++)
            {
                returnValue *= 10;
            }

            for (var count = 0; count < digitsCount; count++)
            {
                returnValue = returnValue >> 4;
            }

            return "3." + returnValue.ToString();

        }

        public string CalculatePiDirectly(int maxDigits, IProgress<Tuple<int, string>> progress = null)
        {
            BigInteger TWO = 2;
            BigInteger TEN = 10;
            BigInteger k = 2;
            BigInteger a = 4;
            BigInteger b = 1;
            BigInteger a1 = 12;
            BigInteger b1 = 4;

            StringBuilder sb = new StringBuilder();

            var digitCount = -1;

            do
            {
                BigInteger p = k * k;
                var q = TWO * k + BigInteger.One;
                k = k + BigInteger.One;
                var tempa1 = a1;
                var tempb1 = b1;

                a1 = p * a + q * a1;
                b1 = p * b + q * b1;
                a = tempa1;
                b = tempb1;
                BigInteger d = a / b;
                BigInteger d1 = a1 / b1;

                while (d == d1)
                {
                    if (digitCount == -1)
                    {
                        sb.Append(" ");
                        sb.Append(d.ToString());
                        digitCount += 1;
                    }
                    else
                    {
                        sb.Append(d.ToString());
                        digitCount += 1;
                    }

                    if (digitCount % 10 == 0)
                    {
                        sb.Append(" ");
                    }

                    progress?.Report(new Tuple<int, string>(digitCount, d.ToString()));

                    a = TEN * (a % b);
                    a1 = TEN * (a1 % b1);
                    d = a / b;
                    d1 = a1 / b1;
                }
                if (digitCount == maxDigits)
                {
                    break;
                }
            } while (true);

            return sb.ToString();

        }

    }
}
