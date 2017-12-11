using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobPlayRaid : JobSequence
	{
		private string _level;

		public JobPlayRaid(MainForm form, string level)
			:
			base(JobTypes.PlayRaid, form)
		{
			_level = level;
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.RaidClearedCnt = _form.RaidClearedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.RaidFailedCnt = _form.RaidFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Cowork, Globals.CoworkPage));
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.CoworkPage }));
					AddSubJob(new JobMovePage(_form, Globals.CoworkPage, Globals.Raid, Globals.RaidPage));
				});

			ActionSequenceTree moveBoss =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[moveBoss]");
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.Demian, true));
					AddSubJob(new JobWaitTime(_form, 2000));
				});

			

			ActionSequenceTree drag =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[drag]");
					OpenCvSharp.Rect left;
					bool leftFlag =
						_form.AllUIRects.TryGetValue(Globals.RaidDragLeftPos, out left);

					OpenCvSharp.Rect right;
					bool rightFlag =
						_form.AllUIRects.TryGetValue(Globals.RaidDragRightPos, out right);

					if (leftFlag && rightFlag)
					{
						var start = Utils.RectToPos(right.X, right.Y, right.Width, right.Height);
						var end = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
						AddSubJob(new JobDrag(_form, start, end));
						
					}
				});
					

			ActionSequenceTree clickLevel =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[clickLevel]");
					AddSubJob(new JobWaitTime(_form, 1000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, _level, true));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree fromRaid =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromRaid]");
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.RaidAward, true));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree checkAwardPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkAwardPage]");

					AddSubJob(new JobCheckPage(_form, Globals.RaidRepeatAwardPage));
				});

			// if checkAwardPage completed
			ActionSequenceTree exitAwardPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitAwardPage]");
					AddSubJob(
						new JobMovePage(
							_form, 
							Globals.RaidRepeatAwardPage, 
							Globals.OutFromRepeatAward, Globals.RaidPage));
				});

			ActionSequenceTree clickStart =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickStart]");
					AddSubJob(new JobMovePage(_form, Globals.RaidPage, Globals.StartRaid, new string[] { Globals.RaidRestrictedPage, Globals.UnknownPage }));
				});

			ActionSequenceTree checkRaidRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkRaidRestricted]");
					AddSubJob(new JobCheckPage(_form, Globals.RaidRestrictedPage));
				});

			// if checkRaidRestricted completed
			ActionSequenceTree exitRaidRestrictedPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitRaidRestrictedPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.RaidRestrictedPage,
							Globals.NoInRaidRestrictedPage, Globals.RaidPage));
				});

			ActionSequenceTree exitRaidPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitRaidPage]");
					AddSubJob(new JobMovePage(_form, Globals.RaidPage, Globals.BackInRaid, Globals.MainPage));
				});

			ActionSequenceTree fromStarted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromStarted]");
					AddSubJob(
						new JobCheckUntilMeetPage(
							_form, new string[] { Globals.RaidResultPage }));
						
				});

			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");
					AddSubJob(new JobMovePage(_form, Globals.RaidResultPage, Globals.ToMainFromRaidResult, Globals.MainPage));
				});

			fromMain.Completed = moveBoss;

			moveBoss.Completed = drag;
			drag.Completed = clickLevel;
			clickLevel.Completed = fromRaid;
			
			fromRaid.Completed = checkAwardPage;
			checkAwardPage.Completed = exitAwardPage;
			checkAwardPage.Failed = clickStart;
			exitAwardPage.Completed = clickStart;
			clickStart.Completed = checkRaidRestricted;
			checkRaidRestricted.Completed = exitRaidRestrictedPage;
			checkRaidRestricted.Failed = fromStarted;
			exitRaidRestrictedPage.Completed = exitRaidPage;
			fromStarted.Completed = fromResult;
			fromResult.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.CoworkPage] = fromMain;
			_repairMap[Globals.RaidPage] = moveBoss;
			_repairMap[Globals.RaidRepeatAwardPage] = exitAwardPage;
			_repairMap[Globals.RaidRestrictedPage] = exitRaidRestrictedPage;
			_repairMap[Globals.RaidResultPage] = fromResult;
		}
	}
}
