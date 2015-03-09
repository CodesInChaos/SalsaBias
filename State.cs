using System;
using System.Diagnostics;

namespace SalsaBias
{
    using Word = UInt32;

    public struct State
    {
        public const int Log2WordSize = 5;
        public const int WordSizeInBits = 1 << Log2WordSize;
        public const int WordCount = 16;
        public const int SizeInBits = WordSizeInBits * WordCount;
        private Word[] _data;
        public Word[] Data { get { return _data; } }

        public static State Create()
        {
            State result;
            result._data = new Word[WordCount];
            return result;
        }

        public Word GetBit(int index)
        {
            int wordIndex = index >> Log2WordSize;
            int bitIndex = index & (WordSizeInBits - 1);
            return (_data[wordIndex] >> bitIndex) & 1;
        }

        public void SetBit(int index, Word value)
        {
            Debug.Assert(value <= 1);
            int wordIndex = index >> Log2WordSize;
            int bitIndex = index & (WordSizeInBits - 1);
            value = value << bitIndex;
            _data[wordIndex] = _data[wordIndex] & ~value | value;
        }

        public void FlipBit(int index)
        {
            int wordIndex = index >> Log2WordSize;
            int bitIndex = index & (WordSizeInBits - 1);
            _data[wordIndex] ^= 1u << bitIndex;
        }

        public static void Copy(State output, State input)
        {
            Array.Copy(input._data, output._data, input._data.Length);
        }

        public static void Xor(State output, State input1, State input2)
        {
            for (int i = 0; i < WordCount; i++)
            {
                output._data[i] = input1._data[i] ^ input2._data[i];
            }
        }
    }
}
