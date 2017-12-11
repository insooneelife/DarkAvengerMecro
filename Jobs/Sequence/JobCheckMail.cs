using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobCheckMail : JobSequence
	{
		public JobCheckMail(MainForm form)
			:
			base(JobTypes.CheckMail, form)
		{
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.MailCheckedCnt = _form.MailCheckedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.MailCheckFailedCnt = _form.MailCheckFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Mail, Globals.MailPage));
				});

			ActionSequenceTree fromMail =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMail]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.RecvAllInMailPage));
					AddSubJob(new JobWaitTime(_form, 500, true));

					AddSubJob(new JobMovePage(_form, Globals.MailPage, Globals.OutFromMailPage, Globals.MainPage));
				});


			fromMain.Completed = fromMail;
			fromMain.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;
			
			// make repair map
			_repairMap[Globals.MailPage] = fromMail;		
		}
	}
}
