using System;
using System.Diagnostics;

namespace OS_lab3
{
	class Program
	{
		static IMenu menu = new Menu("LAB 3: Multithreading",
			new IMenuItem[]
			{
				new MenuItem("Calculate Pi via WinApi threads", CalcViaWinApi),
				new MenuItem("Calculate Pi via c# (CLR) built-in threading system", CalcViaBuiltIn)
			}
			);

		static int N = 100000000,
				   blockSize = 10*3;

		static int ReadThreadCount()
		{
			int tc = 0;
			do
			{
				Console.WriteLine("Input thread count:");
				if (!int.TryParse(Console.ReadLine(), out tc) || tc <=0)
				{
					Console.WriteLine("Please, input an integer greater than zero!");
				}
			} while (tc <= 0);
			return tc;
		}

		static void Calc(IThreadProvider threadProvider)
		{
			int tc = ReadThreadCount();
			//foreach (int tc in new int[] { 1, 2, 4, 8, 16, 32})
			{
				PiCalc calc = new PiCalc(N - 1, blockSize, tc, threadProvider, new OperationBlockFactory());
				Stopwatch watch = new Stopwatch();
				watch.Start();
				calc.StartCalculation();
				double result = calc.GetResult();
				watch.Stop();
				Console.WriteLine($"Result: {result};\nTime: {watch.ElapsedMilliseconds} ms\nThreads: {tc}");
			}
		}
		static void CalcViaWinApi()
		{
			Calc(new WinThreadProvider());
		}

		static void CalcViaBuiltIn()
		{
			Calc(new BuiltInThreadProvider());
		}

		static void Main(string[] args)
		{
			menu.Select();
		}
	}
}
