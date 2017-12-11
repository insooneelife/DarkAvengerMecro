using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobWaitTime : Job
	{
		protected int _waitTime;
		protected DateTime _startTime;

		public JobWaitTime(MainForm form, int waitTime, bool noise = true)
			:
			base(JobTypes.WaitTime, form)
		{
			int noiseTime = 0;
			if (noise)
			{
				noiseTime = form.Random.Next() % 50 - 25;
			}

			_waitTime = waitTime + noiseTime;
		}

		public override void Activate()
		{
			_status = State.Active;
			_startTime = DateTime.Now;
		}

		public override State Process()
		{
			ActivateIfInActive();
			
			//Console.WriteLine("[" + _form.CurrentPageName + "] " + Type + " waiting ..");

			DateTime endTime = DateTime.Now;
			var elapsed = (endTime - _startTime).TotalMilliseconds;
			//Console.WriteLine(elapsed);

			if (elapsed > _waitTime)
			{
				return _status = State.Completed;
			}

			return _status = State.Active;
		}

		public override void Terminate() { }

	}
}