using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobClearQuest : JobSequence
	{
		public JobClearQuest(MainForm form)
			:
			base(JobTypes.ClearQuest, form)
		{
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.QuestClearedCnt = _form.QuestClearedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.QuestFailedCnt = _form.QuestFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			// make action sequence tree

			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Menu, Globals.MenuPage));
				});

			ActionSequenceTree checkMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkMenu]");
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.MenuPage }));
				});

			ActionSequenceTree fromMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMenu]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.MenuToQuest));
					AddSubJob(new JobWaitTime(_form, 500, true));
				});

			ActionSequenceTree afterClickQuest =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[afterClickQuest]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.QuestUISetImg));
				});

			ActionSequenceTree fromWeekQuest =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromWeekQuest]");
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.MenuToWeekQuest, Globals.WeekQuestPage));
				});

			ActionSequenceTree checkCleared =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[checkCleared]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.MissionCompletedImg));
				});

			ActionSequenceTree getAward =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[getAward]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.MissionCompleted, true));
					AddSubJob(new JobWaitTime(_form, 500, true));
				});

			ActionSequenceTree checkMission =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[checkMission]");
					AddSubJob(new JobWaitTime(_form, 750, true));
					AddSubJob(new JobTryFindSubImage(_form, Globals.GetMissionImg, 5, 500));
					AddSubJob(new JobWaitTime(_form, 750, true));
				});

			ActionSequenceTree getMission =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[getMission]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.GetMission, true));
					AddSubJob(new JobWaitTime(_form, 500, true));
				});

			ActionSequenceTree backToMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToMenu]");
					AddSubJob(new JobMovePage(_form, Globals.WeekQuestPage, Globals.BackInWeekQuest, Globals.MenuPage));
				});

			ActionSequenceTree backToMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToMain]");
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.BackInMenu, Globals.MainPage));
				});


			fromMain.Completed = checkMenu;
			checkMenu.Completed = fromMenu;
			fromMenu.Completed = afterClickQuest;
			afterClickQuest.Completed = fromWeekQuest;
			afterClickQuest.Failed = backToMain;
			fromWeekQuest.Completed = checkCleared;
			checkCleared.Completed = getAward;
			checkCleared.Failed = checkMission;
			getAward.Completed = checkMission;
			getAward.ActionOnCompleted = () => { _returnState = State.Completed; };
			checkMission.Completed = getMission;
			checkMission.Failed = backToMenu;
			getMission.Completed = backToMenu;
			backToMenu.Completed = backToMain;

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.MenuPage] = fromMenu;
			_repairMap[Globals.WeekQuestPage] = fromWeekQuest;
		}
	}

	
}
