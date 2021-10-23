using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OS_lab3
{

	public interface IOperationBlock
	{
		void Calculate(int startIter, int endIter, int total);
		void StartCalculate();
		double Result { get; }
		bool Finished { get; }
	}
	public class OperationBlock : IOperationBlock
	{
		IIterationProvider iterationProvider;
		public OperationBlock(IIterationProvider iterationProvider)
		{
			this.iterationProvider = iterationProvider;
		}
		static double CalcIteration(int iteration, int total)
		{
			var x_i = (iteration + 0.5d) / (total);
			return 4d / (1 + (x_i * x_i));
		}

		double sum = 0;

		//inclusive at the end: [start, end]
		public void Calculate(int startIter, int endIter, int total)
		{
			while (startIter <= endIter)
				sum += CalcIteration(startIter++, total);
		}

		public void StartCalculate()
		{
			if (iterationProvider == null)
				return;
			while (iterationProvider.StartNewIterationBlock(this)) ;
			Finished = true;
		}
		public bool Finished { get; private set; } = false;
		public double Result => sum;
	}
}
