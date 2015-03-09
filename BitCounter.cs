using System;

namespace SalsaBias
{
    public class BitCounter
    {
        private readonly uint[] _counts;
        private uint _total;
        public uint[] Counts { get { return _counts; } }
        public uint Total { get { return _total; } }
        public double ExpectancyValue { get { return _total * 0.5; } }

        public BitCounter()
        {
            _counts = new uint[State.SizeInBits];
        }

        public void Increment(State state)
        {
            var counts = _counts;
            {
                _total++;
                int i = 0;
                for (int wordIndex = 0; wordIndex < state.Data.Length; wordIndex++)
                {
                    var wordValue = state.Data[wordIndex];
                    for (int bitIndex = 0; bitIndex < State.WordSizeInBits; bitIndex++)
                    {
                        counts[i] += wordValue & 1;
                        wordValue >>= 1;
                        i++;
                    }
                }
            }
        }


        public Tuple<double, double>[] GetBiases()
        {
            // second value is the standard deviation
            return Array.ConvertAll(_counts, count => Tuple.Create((count - ExpectancyValue) / ExpectancyValue, Math.Sqrt(count) / ExpectancyValue));
        }
    }
}
