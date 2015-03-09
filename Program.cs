using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SalsaBias
{
    using Word = UInt32;

    class Program
    {
        private const int SampleCount = 1 << 25;
        private const int Rounds = 4;

        static void Main()
        {
            var watch = new Stopwatch();
            watch.Start();

            var inputOriginal = State.Create();
            var inputModified = State.Create();
            var outputOriginal = State.Create();
            var outputModified = State.Create();
            var outputDelta = State.Create();
            var bitCounters = Enumerable.Repeat(0, State.SizeInBits).Select(_ => new BitCounter()).ToArray();
            var rng = new Rng();

            for (int sampleIndex = 0; sampleIndex < SampleCount; sampleIndex++)
            {
                if (sampleIndex > 0 && sampleIndex % 1000 == 0)
                {
                    var percentDone = (double)sampleIndex / SampleCount;
                    Console.WriteLine("{0} {1:f1} {3} {2:f0}/s", sampleIndex, percentDone * 100, sampleIndex / watch.Elapsed.TotalSeconds, TimeSpan.FromSeconds(Math.Round(watch.Elapsed.TotalSeconds * (1 / percentDone - 1))));
                }
                rng.GetWords(inputOriginal);
                SalsaCore.HSalsa(outputOriginal, inputOriginal, Rounds);
                for (int bit = 0; bit < State.SizeInBits; bit++)
                {
                    State.Copy(inputModified, inputOriginal);
                    inputModified.FlipBit(bit);
                    SalsaCore.HSalsa(outputModified, inputModified, Rounds);
                    State.Xor(outputDelta, outputOriginal, outputModified);
                    bitCounters[bit].Increment(outputDelta);
                }
            }

            var biasTuples = bitCounters.SelectMany((bitCounter, i) => bitCounter.GetBiases().Select((bias, j) => new { i, j, Bias = bias.Item1, Error = bias.Item2 }))
                .OrderByDescending(t => Math.Abs(t.Bias));
            var formattedOutputs = biasTuples.Select(t => string.Format(CultureInfo.InvariantCulture, "{0:f4} err {1:f4}   {2} -> {3}", t.Bias, t.Error, BitName(t.i), BitName(t.j)));
            File.WriteAllLines("biases.txt", formattedOutputs);

            Console.Clear();
            foreach (var s in formattedOutputs.Take(100))
            {
                Console.WriteLine(s);
            }
        }

        public static string BitName(int bit)
        {
            return string.Format("{0},{1}", bit / State.WordSizeInBits, bit % State.WordSizeInBits);
        }
    }
}
