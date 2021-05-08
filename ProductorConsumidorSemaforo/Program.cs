using System;
using System.Threading;

namespace ProductorConsumidorSemaforo
{
    class Program
    {
        Semaphore producer = new Semaphore(1, 1);
        Semaphore consumer = new Semaphore(0, 1);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
