using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace SamplesCS.Jobs
{

	public class JobPlayTraverse : JobSequence
	{
		private string _level;
		private int _clearCnt = 0;
		private int _clearWaves = 1;

		public JobPlayTraverse(MainForm form, string level, int clearWaves = 2)
			:
			base(JobTypes.PlayTraverse, form)
		{
			_level = level;
			_clearWaves = clearWaves;
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			//_form.TraverseClearedCnt = _form.TraverseClearedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.TraverseFailedCnt = _form.TraverseFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");

					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Traverse, Globals.TraversePage));
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.TraversePage }));
				});

			ActionSequenceTree fromTraverse =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromTraverse]");
					AddSubJob(new JobMovePage(_form, Globals.TraversePage, Globals.TraverseDungeon, Globals.TraverseDungeonPage));
				});

			ActionSequenceTree checkForTraverseDungeonBlankPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkForBlankTraversePage]");
					AddSubJob(new JobCheckPage(_form, Globals.TraverseDungeonBlankPage));
				});

			ActionSequenceTree moveLeft =
				new ActionSequenceTree(() => 
				{
					Console.WriteLine("[moveLeft]");
					new JobMovePage(_form, Globals.TraverseDungeonBlankPage, Globals.LeftInTraverse, Globals.TraverseDungeonPage);
				});

			ActionSequenceTree selectLevel = 
				new ActionSequenceTree(() => 
				{
					Console.WriteLine("[selectLevel]");
					AddSubJob(new JobMovePage(_form, Globals.TraverseDungeonPage, _level, Globals.TraverseStagePage));
				});

			ActionSequenceTree fromTraverseStage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromTraverseStage]");
					AddSubJob(new JobMovePage(_form, Globals.TraverseStagePage, Globals.StartTraverse, new string[] { Globals.TraverseRestrictedPage, Globals.UnknownPage }));
				});

			ActionSequenceTree checkTraverseRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkTraverseRestricted]");
					AddSubJob(new JobCheckPage(_form, Globals.TraverseRestrictedPage));
				});

			ActionSequenceTree exitTraverseRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitTraverseRestricted]");
					AddSubJob(new JobMovePage(_form, Globals.TraverseRestrictedPage, Globals.NoInTraverseRestrictedPage, Globals.TraverseStagePage));
				});

			ActionSequenceTree exitTraverseStage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitTraverseStage]");
					AddSubJob(new JobMovePage(_form, Globals.TraverseStagePage, Globals.BackInTraverseStage, Globals.TraverseDungeonPage));					
				});

			ActionSequenceTree exitTraverseDungeon =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitTraverseDungeon]");
					AddSubJob(new JobMovePage(_form, Globals.TraverseDungeonPage, Globals.BackInTraverseDungeon, Globals.MainPage));
				});

			ActionSequenceTree fromStarted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromStarted]");
					AddSubJob(new JobCheckUntilMeetPage(
						_form,
						new string[] { Globals.TraverseResultPage }));
				});

			ActionSequenceTree checkClearCnt =
				new ActionSequenceTree(() =>
				{
					AddSubJob(new JobCheckCondition(_form, () => { return _clearCnt >= _clearWaves; }));
				});


			ActionSequenceTree retry =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[retry]");

					AddSubJob(new JobMovePage(
						_form,
						Globals.TraverseResultPage,
						Globals.RetryInTraverseResultPage,
						Globals.TraverseStagePage));
				});

			ActionSequenceTree toMainFromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[toMainFromResult]");
					
						AddSubJob(new JobMovePage(
							_form,
							Globals.TraverseResultPage,
							Globals.ToMainFromTraverseResultPage, 
							Globals.MainPage));
				});

			ActionSequenceTree outFromInGameStop =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[outFromInGameStop]");

					var job = JobGenerator.CreateJobMoveAndClickMouse(
						_form, Globals.KeepGoingInStopPage, true);
					job.AddSubJob(new JobWaitTime(_form, 750));

					AddSubJob(job);
				});

		
			fromMain.Completed = fromTraverse;
			fromTraverse.Completed = checkForTraverseDungeonBlankPage;
			checkForTraverseDungeonBlankPage.Completed = moveLeft;
			checkForTraverseDungeonBlankPage.Failed = selectLevel;
			moveLeft.Completed = selectLevel;
			selectLevel.Completed = fromTraverseStage;
			fromTraverseStage.Completed = checkTraverseRestricted;
			checkTraverseRestricted.Completed = exitTraverseRestricted;
			exitTraverseRestricted.Completed = exitTraverseStage;
			exitTraverseStage.Completed = exitTraverseDungeon;
			checkTraverseRestricted.Failed = fromStarted;
			fromStarted.Completed = checkClearCnt;

			checkClearCnt.Completed = toMainFromResult;
			checkClearCnt.Failed = retry;
			checkClearCnt.ActionOnCompleted = () => { _clearCnt++; _form.TraverseClearedCnt = _form.TraverseClearedCnt + 1; };
			checkClearCnt.ActionOnFailed = () => { _clearCnt++; _form.TraverseClearedCnt = _form.TraverseClearedCnt + 1; };

			retry.Completed = fromTraverseStage;
			toMainFromResult.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.TraversePage] = fromTraverse;
			_repairMap[Globals.TraverseDungeonPage] = checkForTraverseDungeonBlankPage;
			_repairMap[Globals.TraverseDungeonBlankPage] = checkForTraverseDungeonBlankPage;
			_repairMap[Globals.TraverseStagePage] = fromTraverseStage;
			_repairMap[Globals.TraverseRestrictedPage] = checkTraverseRestricted;
			_repairMap[Globals.TraverseResultPage] = checkClearCnt;

			//_repairMap[Globals.UnknownPage] = outFromInGameStop;
		}
	}
}