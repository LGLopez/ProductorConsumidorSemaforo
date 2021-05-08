using System;
using System.Threading;

namespace ProductorConsumidorSemaforo
{
    class Program
    {
        static void Main(string[] args)
        {
            Semaphore producer = new Semaphore(1, 1);
            Semaphore consumer = new Semaphore(0, 1);
            int[] buffer = new int[20];

            void produce()
            {

            }

            void initializeBuffer()
            {
                for (int i = 0; i < 20; i++)
                {
                    buffer[i] = 0;
                }
            }

            initializeBuffer();

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Buffer {0}: ", i);
                Console.WriteLine(buffer[i]);
            }
        }
    }
}
