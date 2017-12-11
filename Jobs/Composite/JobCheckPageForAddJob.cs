using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{	
	public class JobCheckPageForAddJob : JobComposite
	{		
		protected DateTime _startTime;
		protected int _judgeTime;
		Dictionary<string, List<Job>> _addJobSet = new Dictionary<string, List<Job>>();

		private bool _addingMode = true;

		public JobCheckPageForAddJob(
			MainForm form, Dictionary<string, List<Job>> addJobSet = null, int judgeTime = 2000)
			:
			base(JobTypes.CheckForAddJob, form)
		{
			if (addJobSet != null)
			{
				foreach (var j in addJobSet)
				{
					if (j.Value != null)
					{ 
						_addJobSet.Add(j.Key, j.Value);
					}
				}
			}

			_judgeTime = judgeTime;
		}

		public override void Activate()
		{
			_status = State.Active;
			_startTime = DateTime.Now;
		}

		public override State Process()
		{
			ActivateIfInActive();

			var pageName = _form.CurrentPageName;
			DateTime endTime = DateTime.Now;

			if (_addingMode)
			{
				var elapsed = (endTime - _startTime).TotalMilliseconds;
				if (elapsed > _judgeTime)
				{
					return _status = State.Failed;
				}

				if (CheckPageAndAddJob(pageName))
				{
					_addingMode = false;
				}
				return _status = State.Active;
			}
			else
			{
				return _status = ProcessSubJobs();
			}
		}

		public override void Terminate() { }

		private bool CheckPageAndAddJob(string pageName)
		{
			List<Job> jobs = null;
			if (_addJobSet.TryGetValue(pageName, out jobs))
			{
				foreach (var j in jobs)
				{
					AddSubJob(j);
				}

				return true;
			}
			return false;
		}
	}
}
