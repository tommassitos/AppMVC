using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AppMVC.Controllers
{
    public class PerformanceController : Controller
    {
        const int size = 1024 * 1024;

        static List<WeakReference<long[]>> weakListArrayPool =
             new List<WeakReference<long[]>>();

        static List<WeakReference<long[]>> weakListNoArrayPool =
             new List<WeakReference<long[]>>();

        public IActionResult ArrayPool()
        {
            var arrPool = ArrayPool<List<double>>.Shared;
            var list = arrPool.Rent(1);

            var samePool = ArrayPool<long>.Shared;
            var buffer = samePool.Rent(size);
            try
            {
                for (var i = 0; i < buffer.Length; i++)
                {
                    buffer[i]++;
                }
            }
            finally
            {
                samePool.Return(buffer, true);
            }

            lock (weakListArrayPool)
                weakListArrayPool.Add(new WeakReference<long[]>(buffer));

            return Ok(GC.GetGeneration(buffer));
        }

        public IActionResult NoArrayPool()
        {
            var buffer = new long[size];

            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i]++;
            }

            lock (weakListNoArrayPool)
                weakListNoArrayPool.Add(new WeakReference<long[]>(buffer));

            return Ok(GC.GetGeneration(buffer));
        }

        public IActionResult Load()
        {
            for (var i = 0; i < 100; i++)
            {
                ArrayPool();
                NoArrayPool();
            }

            return Ok();
        }

        public IActionResult Stats()
        {
            int ArrayPoolCount = 0;
            int NoArrayPoolCount = 0;

            lock (weakListArrayPool)
            {
                weakListArrayPool = weakListArrayPool.Distinct(new WeakReferenceComparer()).ToList();

                foreach (var i in weakListArrayPool)
                {
                    if (i.TryGetTarget(out var val))
                    {
                        ArrayPoolCount++;
                    }
                }
            }

            lock (weakListNoArrayPool)
            {
                weakListNoArrayPool = weakListNoArrayPool.Distinct(new WeakReferenceComparer()).ToList();

                foreach (var i in weakListNoArrayPool.Distinct(new WeakReferenceComparer()))
                {
                    if (i.TryGetTarget(out var val))
                    {
                        NoArrayPoolCount++;
                    }
                }
            }

            return Content($"ArrayPoolCount  {ArrayPoolCount}, NoArrayPoolCount {NoArrayPoolCount}");
        }
    }


    class WeakReferenceComparer : IEqualityComparer<WeakReference<long[]>>
    {
        public bool Equals(WeakReference<long[]> x, WeakReference<long[]> y)
        {
            if (!x.TryGetTarget(out var val_x) | !y.TryGetTarget(out var val_y))
                return false;

            return ReferenceEquals(val_x, val_y);
        }

        public int GetHashCode(WeakReference<long[]> obj)
        {
            if (obj.TryGetTarget(out var val))
                return val.GetHashCode();

            return int.MinValue;
        }
    }

}