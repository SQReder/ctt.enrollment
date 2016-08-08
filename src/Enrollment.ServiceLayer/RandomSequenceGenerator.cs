using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.ServiceLayer
{
    public class RandomIdGenerator: IRandomIdGenerator
    {
        private readonly int _maxIdValue;
        private readonly RandomNumberGenerator _randomProvider;

        public RandomIdGenerator(int maxIdValue = 999999)
        {
            _maxIdValue = maxIdValue;
            _randomProvider = RandomNumberGenerator.Create();
        }

        private int RandomInteger()
        {
            var buffer = new byte[4];
            _randomProvider.GetBytes(buffer);
            var value = BitConverter.ToInt32(buffer, 0);
            return value;
        }

        public int Generate()
        {
            var id = RandomInteger();

            if (id < 0) id = -id;

            id %= _maxIdValue;

            return id;
        }
    }
}
