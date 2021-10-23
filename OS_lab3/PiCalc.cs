using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_lab3
{
	public interface IIterationProvider
	{
		/// <summary>
		/// returns false if no more iterations are available. 
		/// </summary>
		bool StartNewIterationBlock(IOperationBlock block);
	}

	public class PiCalc : IIterationProvider
	{

		int totalIterations, blockSize, currentIteration = 0, operationBlockCount;
		List<IOperationBlock> operationBlocks = new List<IOperationBlock>();
		IThreadProvider threadProvider;
		IOperationBlockFactory operationBlockFactory;
		public PiCalc(int totalIterations, int blockSize, int operationBlockCount, 
			IThreadProvider threadProvider, IOperationBlockFactory operationBlockFactory)
		{
			this.totalIterations = totalIterations;
			this.blockSize = blockSize - 1;
			this.operationBlockCount = operationBlockCount;
			this.threadProvider = threadProvider;
			this.operationBlockFactory = operationBlockFactory;
			for (int i = 0; i < operationBlockCount; i++)
				operationBlocks.Add(operationBlockFactory.CreateInstance(this));
		}

		public void StartCalculation()
		{
			for (int i = 0; i < operationBlockCount; i++)
			{
				//avoid thread parameter error
				var j = i;
				threadProvider.StartNewThread(operationBlocks[j].StartCalculate);
			}
		}

		public bool StartNewIterationBlock(IOperationBlock block)
		{
			int currentStart, currentEnd;
			int currentBlock = Math.Min(blockSize, totalIterations - currentIteration);
			if (currentBlock <= 0)
				return false;
			lock (this)
			{
				currentStart = currentIteration;
				currentIteration += currentBlock + 1;
				currentEnd = currentIteration - 1;
			}
			block.Calculate(currentStart, currentEnd, totalIterations + 1);
			return true;
		}

		public double GetResult()
		{
			double result = 0;
			foreach (var op in operationBlocks)
			{
				while (!op.Finished)
					;
				result += op.Result;
			}
			threadProvider.TerminateAllThreads();
			return result/(totalIterations+1);
		}

	}
}