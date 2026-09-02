using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Multithreading21
{
    internal class Program
    {
        const int n = 10;
        static int[,] path = new int[n, n];
        static readonly object locker = new object();
        static void Main(string[] args)
        {
            //Имеется пустой участок земли(двумерный массив) и план сада,
            //который необходимо реализовать.Эту задачу выполняют два садовника,
            //которые не хотят встречаться друг с другом.
            //Первый садовник начинает работу с верхнего левого угла сада
            //и перемещается слева направо, сделав ряд, он спускается вниз.
            //Второй садовник начинает работу с нижнего правого угла сада
            //и перемещается снизу вверх, сделав ряд, он перемещается влево.
            //Если садовник видит, что участок сада
            //уже выполнен другим садовником, он идет дальше.
            //Садовники должны работать параллельно.
            //Создать многопоточное приложение, моделирующее работу садовников.
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    path[i, j] = rand.Next(0, n);
                }
            }
            ThreadStart threadStart = new ThreadStart(Gardner1);
            Thread thread = new Thread(threadStart);
            thread.Start();

            Gardner2();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < n; j++)
                {
                    Console.Write($" {path[i, j]} ");
                }
            }
            Console.ReadKey();
        }
        static void Gardner1()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    lock (locker)
                    {
                        if (path[i, j] >= 0) 
                        {
                            Thread.Sleep(1);
                            path[i, j] = -1;
                        }
                    }
                }
            }
        }
        static void Gardner2()
        {
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    lock (locker)
                    {
                        if (path[i, j] >= 0)
                        {
                            Thread.Sleep(1);
                            path[i, j] = 2;
                        }
                    }
                }
            }
        }
    }
}
