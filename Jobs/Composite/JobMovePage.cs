using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobMovePage : JobComposite
	{
		private string _from;
		private string _ui;
		private string[] _to;
		private int _startDelay;
		private int _clickDelay;
		private bool _waitFinished = false;
		private DateTime _startTime;
		private string _debugToPageString = "";

		public JobMovePage(
			MainForm form, string from, string ui, string to, int startDelay = 1000, int clickDelay = 1500)
			:
			base(JobTypes.MovePage, form)
		{
			_from = from;
			_ui = ui;
			_to = new string[] { to };
			_startDelay = startDelay;
			_clickDelay = clickDelay;

			foreach (var e in _to)
			{
				_debugToPageString += e + " ";
			}
		}

		public JobMovePage(
			MainForm form, string from, string ui, string[] to, int startDelay = 1000, int clickDelay = 1500)
			:
			base(JobTypes.MovePage, form)
		{
			_from = from;
			_ui = ui;
			_to = to;
			_startDelay = startDelay;
			_clickDelay = clickDelay;
			
			foreach (var e in _to)
			{
				_debugToPageString += e + " ";
			}
		}

		public override void Activate()
		{
			_status = State.Active;
			_startTime = DateTime.Now;
			if (_startDelay > 0)
			{
				AddSubJob(new JobWaitTime(_form, _startDelay));
			}
		}

		public override State Process()
		{
			ActivateIfInActive();

			var endTime = DateTime.Now;
			double elapsed = (endTime - _startTime).TotalMilliseconds;

			if (elapsed > 300000 && _subJobs.Count > 0)
			{
				string term = "[JobMovePage] have no action for too long time!!!! remove self  time : " + endTime + "\n";

				return _status = State.Failed;
			}

				if (!_waitFinished)
			{
				var status = ProcessSubJobs();

				if (status == State.Completed)
				{
					_waitFinished = true; 
				}

				return _status = State.Active;
			}
			else
			{
				var pageName = _form.CurrentPageName;

				Console.WriteLine("[" + pageName + "] " + Type + " from : \"" + _from + "\"   clickUI : \"" + _ui + "\"   to : " + _debugToPageString);

				if (Check(pageName))
				{
					return _status = State.Completed;
				}

				if (_subJobs.Count == 0 && pageName == _from)
				{
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, _ui, true));
					AddSubJob(new JobWaitTime(_form, _clickDelay));
				}

				ProcessSubJobs();

				return _status = State.Active;
			}
		}

		public override void Terminate() { }

		private bool Check(string currentPage)
		{
			foreach (var p in _to)
			{
				if (p == currentPage)
				{
					return true;
				}
			}
			return false;
		}
	}
}
