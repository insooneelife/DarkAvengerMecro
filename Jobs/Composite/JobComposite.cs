using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public abstract class JobComposite : Job
	{
		protected List<Job> _subJobs = new List<Job>();

		public JobComposite(JobTypes type, MainForm owner, int blackBoardAccessID = BlackBoard.CantAccess)
			:
			base(type, owner, blackBoardAccessID)
		{ }

		public override abstract void Activate();

		public override abstract State Process();

		public override abstract void Terminate();

		public void AddSubJob(Job job)
		{
			if (job == null)
				return;

			_subJobs.Add(job);
		}

		public void RemoveAllSubJobs()
		{
			_subJobs.Clear();
		}


		protected State ProcessSubJobs()
		{
			while (_subJobs.Count > 0 && (_subJobs[0].IsCompleted || _subJobs[0].HasFailed))
			{
				_subJobs[0].Terminate();
				_subJobs.RemoveAt(0);
			}

			if (_subJobs.Count > 0)
			{
				State stateOfSubJobs = _subJobs[0].Process();

				if (stateOfSubJobs == State.Completed && _subJobs.Count > 1)
				{
					return State.Active;
				}
				return stateOfSubJobs;
			}
			else
			{
				return State.Completed;
			}
		}
	}
}