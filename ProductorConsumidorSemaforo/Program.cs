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
            bool isProducerIn = true, finish = false ;

            void produce()
            {
                int count = 0;
                for (int i = 0; count < 20; )
                {
                    if (isProducerIn)
                    {
                        producer.WaitOne();
                    
                        if (i >= 20)
                            i = 0;

                        if (buffer[i] == 0)
                        {
                            int num = rand.Next(1, 99);
                            buffer[i] = num;
                            Console.WriteLine("El productor agrego el {0} al buffer en la posicion {1}", num, i);
                        }
                        drawBuffer();
                        i++;
                        count++;
                        isProducerIn = false;
                        Console.WriteLine("El productor esta dormido");
                    }
                }
                producer.WaitOne();
                isDone = true;
            }
            
            void consume()
            {
                while (!isDone)
                {
                    if (!isProducerIn)
                    {
                       consumer.WaitOne();
                        if (consumerCounter >= 20)
                            consumerCounter = 0;

                        if (buffer[consumerCounter] != 0)
                        {
                            int numTaken = buffer[consumerCounter];
                            buffer[consumerCounter] = 0;
                            Console.WriteLine("El consumidor saco el valor {0} de la posicion {1}", numTaken, consumerCounter);
                        }
                        drawBuffer();
                        consumerCounter++;
                        if (consumerCounter >= 20)
                            consumerCounter = 0;
                        isProducerIn = true;
                        Console.WriteLine("El consumidor esta dormido");
                    }
                }
            }

            void availabilty()
            {
                do
                {
                    Thread.Sleep(rand.Next(1, 9) * 200);
                    if (buffer[i] == 0)
                        producer.Release();
                    Thread.Sleep(rand.Next(1, 9) * 200);
                    if (buffer[consumerCounter] != 0)
                        consumer.Release();

                } while (!isDone);
            }
            
            void stop()
            {
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey();
                } while (cki.Key != ConsoleKey.Escape);
                finish = true;
                Environment.Exit(0);
            }

            void drawBuffer()
            {
                string results = "";
                for (int i = 0; i < 20; i++)
                {
                    if (buffer[i] == 0)
                    {
                        results += "_|";
                    }
                    else
                    {
                        results += buffer[i] + "|";
                    }
                }
                Console.WriteLine(results);
            }

            Console.WriteLine("Presione ESC para terminar el programa en cualquier momento\n");

            Thread producerThread = new Thread(produce);
            Thread consumerThread = new Thread(consume);
            Thread availabilityThread = new Thread(availabilty);
            Thread stopCode = new Thread(stop);

            producerThread.Start();
            consumerThread.Start();
            availabilityThread.Start();

            stopCode.Start();

            producerThread.Join();
            consumerThread.Join();
            availabilityThread.Join();

            Console.WriteLine("Presione ESC para salir");

            stopCode.Join();
        }
    }
}
