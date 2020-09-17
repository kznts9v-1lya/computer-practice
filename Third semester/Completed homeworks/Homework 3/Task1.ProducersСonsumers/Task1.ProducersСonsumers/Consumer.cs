﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1.ProducersСonsumers
{
    public class Consumer
    {
        Thread thread;
        Semaphore semaphore;
        List<string> storage;
        static Random random = new Random();

        public bool IsPurchasing { private get; set; } = true;

        public Consumer(string name, List<string> storage, Semaphore semaphore)
        {
            thread = new Thread(UploadProduct);
            thread.Name = name;
            this.semaphore = semaphore;
            this.storage = storage;
            thread.Start();
        }

        void UploadProduct()
        {
            //A postcondition loop allows the queue to run to the end.
            do
            {
                semaphore.WaitOne();
                Console.WriteLine(thread.Name + " arrived at the storage.");

                string product = string.Empty;

                if (storage.Count != 0)
                {
                    int productNumber = random.Next(storage.Count);
                    product = storage[productNumber];
                    storage.RemoveAt(productNumber);
                }
                
                Console.WriteLine(thread.Name + " purchase " + (product.Equals(string.Empty)? "nothing" : product) + " and left the storage.");
                Thread.Sleep(500);
                semaphore.Release();
            }
            while (IsPurchasing == true);
        }
    }
}