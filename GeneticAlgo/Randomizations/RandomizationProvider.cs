using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Randomizations
{
    public static class RandomizationProvider
    {
        static RandomizationProvider()
        {
            Current = new FastRandom(DateTime.Now.Millisecond);
        }

        public static FastRandom Current { get; set; }
    }
}

//namespace GeneticAlgo.Randomizations
//{
//    public static class RandomizationProvider
//    {
//        private static readonly object _globalLock = new object();
//        static RandomizationProvider()
//        {
//            ThreadLocal<FastRandom> threadLocal = new ThreadLocal<FastRandom>(NewRandom);
//            //Current = new FastRandom(DateTime.Now.Millisecond);
//            Current = threadLocal.Value;
//        }

//        private static FastRandom NewRandom()
//        {
//            lock (_globalLock)
//            {
//                return new FastRandom(DateTime.Now.Microsecond);
//            }
//        }

//        public static FastRandom Current { get; set; }
//    }
//}
