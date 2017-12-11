using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class ActionSequenceTree
	{
		private Action _action = null;

		private Action _actionOnCompleted = null;
		private Action _actionOnFailed = null;

		private ActionSequenceTree _completed = null;
		private ActionSequenceTree _failed = null;

		public Action Action
		{
			get { return _action; }
		}

		public ActionSequenceTree Completed
		{
			get { return _completed; }
			set { _completed = value; }
		}

		public ActionSequenceTree Failed
		{
			get { return _failed; }
			set { _failed = value; }
		}

		public Action ActionOnCompleted
		{
			get { return _actionOnCompleted; }
			set { _actionOnCompleted = value; }
		}

		public Action ActionOnFailed
		{
			get { return _actionOnFailed; }
			set { _actionOnFailed = value; }
		}

		public ActionSequenceTree(Action action)
		{
			_action = action;
		}

		public void OnCompleted()
		{
			if (_actionOnCompleted != null)
			{
				_actionOnCompleted();
			}
		}

		public void OnFailed()
		{
			if (_actionOnFailed != null)
			{
				_actionOnFailed();
			}
		}
	}
}
