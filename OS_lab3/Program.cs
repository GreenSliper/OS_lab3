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
		static void CalcViaWinApi()
		{
			PiCalc calc = new PiCalc(N-1, blockSize, 4, new WinThreadProvider(), new OperationBlockFactory());
			Stopwatch watch = new Stopwatch();
			watch.Start();
			calc.StartCalculation();
			double result = calc.GetResult();
			watch.Stop();
			Console.WriteLine($"Result: {result}; Time: {watch.ElapsedMilliseconds} ms");
		}

		static void CalcViaBuiltIn()
		{
			PiCalc calc = new PiCalc(N - 1, blockSize, 4, new BuiltInThreadProvider(), new OperationBlockFactory());
			Stopwatch watch = new Stopwatch();
			watch.Start();
			calc.StartCalculation();
			double result = calc.GetResult();
			watch.Stop();
			Console.WriteLine($"Result: {result}; Time: {watch.ElapsedMilliseconds} ms");
		}

		static void Main(string[] args)
		{
			menu.Select();
		}
	}
}
