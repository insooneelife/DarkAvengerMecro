using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobCheckPage : Job
	{
		protected DateTime _startTime;
		protected int _judgeTime;
		protected string[] _pages;

		protected string _debugPages = "";

		public JobCheckPage(
			MainForm form, string[] pages, int judgeTime = 2000)
			:
			base(JobTypes.CheckPage, form)
		{
			_pages = pages;
			_judgeTime = judgeTime;

			foreach (var p in _pages)
			{
				_debugPages += p + " ";
			}
		}

		public JobCheckPage(
			MainForm form, string page, int judgeTime = 2000)
			:
			base(JobTypes.CheckPage, form)
		{
			_pages = new string[] { page };
			_judgeTime = judgeTime;

			foreach (var p in _pages)
			{
				_debugPages += p + " ";
			}
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
			var elapsed = (endTime - _startTime).TotalMilliseconds;

			Console.WriteLine("[JobCheckPage]  page : " + _debugPages);

			if (elapsed > _judgeTime)
			{
				return _status = State.Failed;
			}

			if (CheckAtLeastOne(pageName))
			{
				return _status = State.Completed;
			}
			
			return _status = State.Active;
		}

		public override void Terminate() { }

		private bool CheckAtLeastOne(string pageName)
		{
			foreach (var p in _pages)
			{
				if (pageName == p)
				{
					return true;
				}
			}
			return false;
		}
	}
}
