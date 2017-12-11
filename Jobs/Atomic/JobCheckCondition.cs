using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobCheckCondition : Job
	{
		private Func<bool> _condition = null;
		private bool _result = false;

		public JobCheckCondition(MainForm form, Func<bool> condition)
			:
			base(JobTypes.CheckCondition, form)
		{
			_condition = condition;
		}

		public override void Activate()
		{
			_status = State.Active;
			if (_condition != null)
			{
				_result = _condition();
			}
		}

		public override State Process()
		{
			ActivateIfInActive();

			if (_result)
			{
				Console.WriteLine("[JobCheckCondition] true!!!!!!!!!!!!");
				return _status = State.Completed;
			}
			else
			{
				Console.WriteLine("[JobCheckCondition] false!!!!!!!!!!!!");
				return _status = State.Failed;
			}
		}

		public override void Terminate() { }

	}
}
