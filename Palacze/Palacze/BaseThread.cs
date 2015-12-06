using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palacze
{
	abstract class BaseThread
	{
		private Thread thread;

		protected BaseThread()
		{
			thread = new Thread(new ThreadStart(RunThread));
		}

		public void Start()
		{
			thread.Start();
		}

		public abstract void RunThread();
	}
}
