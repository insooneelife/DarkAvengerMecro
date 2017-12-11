using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobTakeAbrasive : JobSequence
	{
		public JobTakeAbrasive(MainForm form)
			:
			base(JobTypes.TakeAbrasive, form)
		{
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
		}

		protected override void OnJobFailed()
		{
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
				
			ActionSequenceTree fromMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMenu]");
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.MenuToForge, Globals.ForgePage));
				});


			ActionSequenceTree fromForge =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromForge]");
					AddSubJob(new JobMovePage(_form, Globals.ForgePage, Globals.ToAbrasive, Globals.ForgeAbrasivePage));
				});

			
			ActionSequenceTree checkTakeAbrasive =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[checkTakeAbrasive]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.TakeAbrasiveImg));
				});

			ActionSequenceTree takeAbrasive =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[takeAbrasive]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.TakeAbrasive, true));
					AddSubJob(new JobWaitTime(_form, 500, true));
				});

			ActionSequenceTree checkMakeAbrasive =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[checkMakeAbrasive]");
					AddSubJob(new JobWaitTime(_form, 750, true));
					AddSubJob(new JobTryFindSubImage(_form, Globals.MakeAbrasiveImg, 5, 500));
					AddSubJob(new JobWaitTime(_form, 750, true));
				});

			ActionSequenceTree makeAbrasive =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[makeAbrasive]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.TakeAbrasive, true));
					AddSubJob(new JobWaitTime(_form, 500, true));
				});

			ActionSequenceTree backToForge =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToForge]");
					AddSubJob(new JobMovePage(_form, Globals.ForgeAbrasivePage, Globals.BackInForgeAbrasive, Globals.ForgePage));
				});

			ActionSequenceTree backToMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToMenu]");
					AddSubJob(new JobMovePage(_form, Globals.ForgePage, Globals.BackInForge, Globals.MenuPage));
				});

			ActionSequenceTree backToMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToMain]");
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.BackInMenu, Globals.MainPage));
				});

			// on repair
			ActionSequenceTree exitForgeAbrasiveGem =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitForgeAbrasiveGem]");
					AddSubJob(new JobMovePage(_form, Globals.ForgeAbrasiveGemPage, Globals.NoInForgeAbrasiveGemPage, Globals.ForgeAbrasivePage));
				});

			ActionSequenceTree exitForgeAbrasiveRestrict =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitForgeAbrasiveRestrict]");
					AddSubJob(new JobMovePage(_form, Globals.ForgeAbrasiveRestrictedPage, Globals.NoInForgeAbrasiveRestrictedPage, Globals.ForgeAbrasivePage));
				});

			fromMain.Completed = fromMenu;
			fromMenu.Completed = fromForge;
			fromForge.Completed = checkTakeAbrasive;
			checkTakeAbrasive.Completed = takeAbrasive;
			checkTakeAbrasive.ActionOnCompleted = () => { _returnState = State.Completed; };
			checkTakeAbrasive.Failed = checkMakeAbrasive;
			takeAbrasive.Completed = checkMakeAbrasive;
			checkMakeAbrasive.Completed = makeAbrasive;
			checkMakeAbrasive.Failed = backToForge;
			makeAbrasive.Completed = backToForge;
			backToForge.Completed = backToMenu;
			backToMenu.Completed = backToMain;
			
			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.MenuPage] = fromMenu;
			_repairMap[Globals.ForgePage] = fromForge;
			_repairMap[Globals.ForgeAbrasivePage] = checkTakeAbrasive;
			_repairMap[Globals.ForgeAbrasiveGemPage] = exitForgeAbrasiveGem;
			_repairMap[Globals.ForgeAbrasiveRestrictedPage] = exitForgeAbrasiveRestrict;
		}
	}
}
