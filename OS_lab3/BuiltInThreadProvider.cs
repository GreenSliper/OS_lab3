using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OS_lab3
{
	class BuiltInThreadProvider : IThreadProvider
	{
		public void StartNewThread(Action func)
		{
			Thread t = new Thread(func.Invoke);
			t.Start();
		}

		public void TerminateAllThreads()
		{
			//no need to do anything
		}
	}
}
