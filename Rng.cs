using System;
using System.Security.Cryptography;

namespace SalsaBias
{
    class Rng
    {
        readonly RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();
        private readonly byte[] _buffer;

        public void GetWords(State state)
        {
            _provider.GetBytes(_buffer);
            Buffer.BlockCopy(_buffer, 0, state.Data, 0, _buffer.Length);
        }

        public Rng()
        {
            _buffer = new byte[State.SizeInBits / 8];
        }
    }
}
