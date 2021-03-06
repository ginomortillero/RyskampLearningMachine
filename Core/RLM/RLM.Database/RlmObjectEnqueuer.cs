﻿using RLM.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLM.Database
{
    public static class RlmObjectEnqueuer
    {
        public static void QueueObjects<T>(ConcurrentQueue<T> cache, BlockingCollection<T> bc)
        {
            int index = 0;
            while (true)
            {
                int batch = 10;
                int max = batch + index;
                //var values = cache.Skip(index).Take(batch);
                //index += values.Count();
                //foreach(var item in values)
                //{
                //    bc.Add(item);
                //}
                //for (int i = index; i < max; i++)
                //{
                //    if (i <= cache.Count - 1)
                //    {
                //        bc.Add(cache.ElementAt(i));
                //        index++;
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}

                for (int i = 0; i < batch; i++)
                {
                    T item;
                    if (cache.TryDequeue(out item))
                    {
                        bc.TryAdd(item);
                    }
                }

                Task.Delay(50).Wait();
            }
        }
    }
}
