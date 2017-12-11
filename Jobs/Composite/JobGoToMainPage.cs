using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	
	public class JobGoToMainPage : JobComposite
	{
		public JobGoToMainPage(MainForm form)
			:
			base(JobTypes.GoToMainPage, form)
		{
			

			//AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(form, JobGenerator.Cowork));
			//AddSubJob(new JobWaitTime(form, 500));
			//AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(form, JobGenerator.Party));
			//AddSubJob(new JobWaitTime(form, 500));
			//AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(form, JobGenerator.StartParty));
		}

		public override void Activate()
		{
			_status = State.Active;
		}

		public override State Process()
		{
			ActivateIfInActive();

			var pageName = _form.CurrentPageName;

			if (pageName == Globals.UnknownPage)
			{ 
				return _status = State.Active;
			}
			else if (pageName == Globals.MainPage)
			{ 
				return _status = State.Completed;
			}
			else 
			{
				return _status = ProcessSubJobs();
			}
		}

		public override void Terminate() { }
	}
}
