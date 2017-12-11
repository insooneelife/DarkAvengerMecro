using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobKillProcess : JobComposite
	{
		private string _processName;
		private bool _success;

		public JobKillProcess(MainForm owner, string processName)
			:
			base(JobTypes.KillProcess, owner)
		{
			_processName = processName;
		}

		public override void Activate()
		{
			_status = State.Active;

			_success = Utils.KillProcess(_processName);

			AddSubJob(new JobWaitTime(_form, 1000));
		}

		public override State Process()
		{
			ActivateIfInActive();

			var status = ProcessSubJobs();
			

			return _status = status;
		}

		public override void Terminate() { }
	}
	
}
