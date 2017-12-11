using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobCheckUntilMeetPage : Job
	{
		string[] _pages;		

		public JobCheckUntilMeetPage(
			MainForm form, string [] pages)
			:
			base(JobTypes.CheckCurrentPage, form)
		{
			_pages = pages;
		}

		public override void Activate()
		{
			_status = State.Active;
		}

		public override State Process()
		{
			ActivateIfInActive();

			var pageName = _form.CurrentPageName;

			DateTime endTime = DateTime.Now;

			string str = "";
			foreach (var p in _pages)
			{
				str += p + " ";
			}

			//Console.WriteLine("[" + pageName + "] " + Type + " check hard : " + _checkHard + "  " + str);

			if (pageName == Globals.UnknownPage)
			{
				return _status = State.Active;
			}
			else if (Check(pageName))
			{
				Console.WriteLine("[" + pageName + "]  found in  " + str);
				return _status = State.Completed;
			}

			return _status = State.Active;
		}

		public override void Terminate() 
		{
		}


		private bool Check(string pageName)
		{
			return CheckAtLeastOne(pageName);
		}

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
