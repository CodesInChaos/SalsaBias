using System;
using System.Diagnostics;

namespace SalsaBias
{
    internal class SalsaCore
    {
        public static void HSalsa(State output, State input, int rounds)
        {
            HSalsa(output.Data, input.Data, rounds);
        }

        public static void HSalsa(UInt32[] output, UInt32[] input, int rounds)
        {
            Debug.Assert(rounds % 2 == 0, "Number of salsa rounds must be even");

            int doubleRounds = rounds / 2;

            UInt32 x0 = input[0];
            UInt32 x1 = input[1];
            UInt32 x2 = input[2];
            UInt32 x3 = input[3];
            UInt32 x4 = input[4];
            UInt32 x5 = input[5];
            UInt32 x6 = input[6];
            UInt32 x7 = input[7];
            UInt32 x8 = input[8];
            UInt32 x9 = input[9];
            UInt32 x10 = input[10];
            UInt32 x11 = input[11];
            UInt32 x12 = input[12];
            UInt32 x13 = input[13];
            UInt32 x14 = input[14];
            UInt32 x15 = input[15];

            unchecked
            {
                for (int i = 0; i < doubleRounds; i++)
                {
                    UInt32 y;

                    // row 0
                    y = x0 + x12;
                    x4 ^= (y << 7) | (y >> (32 - 7));
                    y = x4 + x0;
                    x8 ^= (y << 9) | (y >> (32 - 9));
                    y = x8 + x4;
                    x12 ^= (y << 13) | (y >> (32 - 13));
                    y = x12 + x8;
                    x0 ^= (y << 18) | (y >> (32 - 18));

                    // row 1
                    y = x5 + x1;
                    x9 ^= (y << 7) | (y >> (32 - 7));
                    y = x9 + x5;
                    x13 ^= (y << 9) | (y >> (32 - 9));
                    y = x13 + x9;
                    x1 ^= (y << 13) | (y >> (32 - 13));
                    y = x1 + x13;
                    x5 ^= (y << 18) | (y >> (32 - 18));

                    // row 2
                    y = x10 + x6;
                    x14 ^= (y << 7) | (y >> (32 - 7));
                    y = x14 + x10;
                    x2 ^= (y << 9) | (y >> (32 - 9));
                    y = x2 + x14;
                    x6 ^= (y << 13) | (y >> (32 - 13));
                    y = x6 + x2;
                    x10 ^= (y << 18) | (y >> (32 - 18));

                    // row 3
                    y = x15 + x11;
                    x3 ^= (y << 7) | (y >> (32 - 7));
                    y = x3 + x15;
                    x7 ^= (y << 9) | (y >> (32 - 9));
                    y = x7 + x3;
                    x11 ^= (y << 13) | (y >> (32 - 13));
                    y = x11 + x7;
                    x15 ^= (y << 18) | (y >> (32 - 18));

                    // column 0
                    y = x0 + x3;
                    x1 ^= (y << 7) | (y >> (32 - 7));
                    y = x1 + x0;
                    x2 ^= (y << 9) | (y >> (32 - 9));
                    y = x2 + x1;
                    x3 ^= (y << 13) | (y >> (32 - 13));
                    y = x3 + x2;
                    x0 ^= (y << 18) | (y >> (32 - 18));

                    // column 1
                    y = x5 + x4;
                    x6 ^= (y << 7) | (y >> (32 - 7));
                    y = x6 + x5;
                    x7 ^= (y << 9) | (y >> (32 - 9));
                    y = x7 + x6;
                    x4 ^= (y << 13) | (y >> (32 - 13));
                    y = x4 + x7;
                    x5 ^= (y << 18) | (y >> (32 - 18));

                    // column 2
                    y = x10 + x9;
                    x11 ^= (y << 7) | (y >> (32 - 7));
                    y = x11 + x10;
                    x8 ^= (y << 9) | (y >> (32 - 9));
                    y = x8 + x11;
                    x9 ^= (y << 13) | (y >> (32 - 13));
                    y = x9 + x8;
                    x10 ^= (y << 18) | (y >> (32 - 18));

                    // column 3
                    y = x15 + x14;
                    x12 ^= (y << 7) | (y >> (32 - 7));
                    y = x12 + x15;
                    x13 ^= (y << 9) | (y >> (32 - 9));
                    y = x13 + x12;
                    x14 ^= (y << 13) | (y >> (32 - 13));
                    y = x14 + x13;
                    x15 ^= (y << 18) | (y >> (32 - 18));
                }
            }

            output[0] = x0;
            output[1] = x1;
            output[2] = x2;
            output[3] = x3;
            output[4] = x4;
            output[5] = x5;
            output[6] = x6;
            output[7] = x7;
            output[8] = x8;
            output[9] = x9;
            output[10] = x10;
            output[11] = x11;
            output[12] = x12;
            output[13] = x13;
            output[14] = x14;
            output[15] = x15;
        }
    }
}
