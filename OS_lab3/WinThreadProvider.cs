using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OS_lab3
{
    class WinThreadProvider : IThreadProvider
    {
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private unsafe static extern uint CreateThread(
            uint* lpThreadAttributes,
            uint dwStackSize,
            ThreadStart lpStartAddress,
            uint* lpParameter,
            uint dwCreationFlags,
            out uint lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(uint hObject);

        List<uint> threadHandles = new List<uint>();
        const int stackSize = 1024;
        public unsafe uint StartThread(Action func, int StackSize)
        {
            uint a = 0;
            uint* lpThrAtt = &a;
            uint i = 0;
            uint* lpParam = &i;
            uint lpThreadID = 0;

            uint dwHandle = CreateThread(null, (uint)StackSize, ()=>func(), lpParam, 0, out lpThreadID);
            if (dwHandle == 0) 
                throw new Exception("Unable to create thread!");
            threadHandles.Add(dwHandle);
            return dwHandle;
        }
        public void StartNewThread(Action func)
        {
            StartThread(func, stackSize);
        }
        public void TerminateAllThreads()
		{
            foreach (var h in threadHandles)
                CloseHandle(h);
		}
	}
}
