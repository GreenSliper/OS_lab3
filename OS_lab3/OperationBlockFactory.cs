using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_lab3
{
	public interface IOperationBlockFactory
	{
		IOperationBlock CreateInstance(IIterationProvider provider);
	}

	class OperationBlockFactory : IOperationBlockFactory
	{
		public IOperationBlock CreateInstance(IIterationProvider provider)
		{
			return new OperationBlock(provider);
		}
	}
}
