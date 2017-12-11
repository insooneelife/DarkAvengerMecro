using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobCompeteBattle : JobSequence
	{
		public JobCompeteBattle(MainForm form)
			:
			base(JobTypes.CompeteBattle, form)
		{
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.CompeteCompletedCnt = _form.CompeteCompletedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.CompeteFailedCnt = _form.CompeteFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Fight, Globals.FightPage));
				});

			ActionSequenceTree fromFight =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromFight]");
					AddSubJob(new JobMovePage(_form, Globals.FightPage, Globals.ToCompeteBattle, Globals.CompeteBattlePage));
				});

			ActionSequenceTree fromCompete =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromCompete]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.RewardCompeteBattle));
					AddSubJob(new JobWaitTime(_form, 1000, true));

					AddSubJob(new JobCheckPage(_form, Globals.CompeteBattleRewardPage));
				});

			ActionSequenceTree exitReward =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitReward]");
					AddSubJob(new JobMovePage(_form, Globals.CompeteBattleRewardPage, Globals.ToCompeteBattleFromReward, Globals.CompeteBattlePage));
				});

			ActionSequenceTree rewardRanking =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[rewardRanking]");
					AddSubJob(new JobWaitTime(_form, 500, true));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.RewardRankingCompeteBattle));

					AddSubJob(new JobWaitTime(_form, 1000, true));
					AddSubJob(new JobCheckPage(_form, Globals.CompeteBattleRewardRankingPage));
				});

			ActionSequenceTree exitRewardRanking =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitRewardRanking]");
					AddSubJob(new JobMovePage(_form, Globals.CompeteBattleRewardRankingPage, Globals.ToCompeteBattleFromRewardRanking, Globals.CompeteBattlePage));
				});

			ActionSequenceTree startCompete =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[startCompete]");
					AddSubJob(
						new JobMovePage(
							_form, 
							Globals.CompeteBattlePage, 
							Globals.StartCompeteBattle2, 
							new string[] { Globals.CompeteBattleRestrictedPage, Globals.UnknownPage }));
				});

			ActionSequenceTree checkRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkRestricted]");
					AddSubJob(new JobWaitTime(_form, 1500, true));
					AddSubJob(new JobCheckPage(_form, Globals.CompeteBattleRestrictedPage));
				});

			ActionSequenceTree exitRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitRaidRestrictedPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.CompeteBattleRestrictedPage,
							Globals.NoInCompeteBattleRestrictedPage, Globals.CompeteBattlePage));
				});

			ActionSequenceTree exitCompeteBattle =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitCompeteBattle]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.CompeteBattlePage,
							Globals.ToMainFromCompeteBattle, Globals.MainPage));
				});


			ActionSequenceTree fromStarted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromStarted]");
					AddSubJob(
						new JobCheckUntilMeetPage(
							_form, new string[] { Globals.CompeteBattleResultPage }));

				});

			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");
					AddSubJob(new JobMovePage(_form, Globals.CompeteBattleResultPage, Globals.ToMainFromCompeteBattleResult, Globals.MainPage));
				});

			fromMain.Completed = fromFight;
			fromFight.Completed = fromCompete;
			fromCompete.Completed = exitReward;
			exitReward.Completed = rewardRanking;
			fromCompete.Failed = rewardRanking;
			rewardRanking.Completed = exitRewardRanking;
			rewardRanking.Failed = startCompete;
			exitRewardRanking.Completed = startCompete;
			startCompete.Completed = checkRestricted;
			checkRestricted.Completed = exitRestricted;
			checkRestricted.Failed = fromStarted;
			exitRestricted.Completed = exitCompeteBattle;
			exitCompeteBattle.ActionOnFailed = () => { _returnState = State.Failed; };
			fromStarted.Completed = fromResult;
			fromResult.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.FightPage] = fromFight;
			_repairMap[Globals.CompeteBattlePage] = fromCompete;
			_repairMap[Globals.CompeteBattleRewardPage] = exitReward;
			_repairMap[Globals.CompeteBattleResultPage] = fromResult;
			_repairMap[Globals.CompeteBattleRestrictedPage] = exitRestricted;
		}
	}
}
