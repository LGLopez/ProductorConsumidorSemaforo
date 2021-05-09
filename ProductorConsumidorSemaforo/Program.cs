using System;
using System.Threading;

namespace ProductorConsumidorSemaforo
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore producer = new Semaphore(0, 1);
            Semaphore consumer = new Semaphore(0, 1);
            int[] buffer = new int[20];

            Random rand = new Random();
            bool isDone = false;
            int i = 0, consumerCounter = 0;

            void produce()
            {
                producer.WaitOne();
                int count = 0;
                for (int i = 0; count < 20; i++, count++)
                {
                    if (i >= 20)
                    {
                        i = 0;
                        buffer[0] = 0;
                        Thread.Sleep(1000);
                    }
                        
                    buffer[i] = rand.Next(1, 99);
                }
                isDone = false;
            }
            
            void consume()
            {
                consumer.WaitOne();
                while (isDone)
                {
                    if (consumerCounter >= 20)
                    {

                    }
                }
            }

            void availabilty()
            {/*
                while (!isDone)
                {*/
                    if (i == 0 && buffer[0] == 0)
                    {
                        producer.Release();
                    }
                    if (buffer[consumerCounter] == 0)
                    {

                    }
                /*}*/
                
            }

            availabilty();
            produce();

            for (int j = 0; j < 20; j++)
            {
                Console.WriteLine("Buffer {0}: ", j);
                Console.WriteLine(buffer[j]);
            }
        }
    }
}
