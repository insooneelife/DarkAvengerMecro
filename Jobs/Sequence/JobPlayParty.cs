using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobPlayParty : JobSequence
	{
		private string _level;

		public JobPlayParty(MainForm form, string level)
			:
			base(JobTypes.PlayParty, form)
		{
			_level = level;
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.PartyClearedCnt = _form.PartyClearedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.PartyFailedCnt = _form.PartyFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromMain =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromMain]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Cowork, Globals.CoworkPage));
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.CoworkPage }));
					AddSubJob(new JobMovePage(_form, Globals.CoworkPage, Globals.Party, Globals.PartyPage));
				});


		

			ActionSequenceTree dragForFindLevel =
				new ActionSequenceTree(
				() =>
				{
					OpenCvSharp.Rect left;
					bool leftFlag =
						_form.AllUIRects.TryGetValue(Globals.RaidDragLeftPos, out left);

					OpenCvSharp.Rect right;
					bool rightFlag =
						_form.AllUIRects.TryGetValue(Globals.RaidDragRightPos, out right);

					if (leftFlag && rightFlag)
					{
						if (_level == Globals.PLevel1 || _level == Globals.PLevel2)
						{
							var start = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
							var end = Utils.RectToPos(right.X, right.Y, right.Width, right.Height);
							AddSubJob(new JobDrag(_form, start, end));
						}
						else
						{
							var start = Utils.RectToPos(right.X, right.Y, right.Width, right.Height);
							var end = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
							AddSubJob(new JobDrag(_form, start, end));
						}
					}
				});

			ActionSequenceTree findLevel =
				new ActionSequenceTree(
				() =>
				{
					AddSubJob(new JobTryFindSubImage(_form, _level));
				});

			ActionSequenceTree clickLevel =
				new ActionSequenceTree(
				() =>
				{
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);
					var pos = Utils.RectToPos(rect.X, rect.Y, rect.Width, rect.Height);
					AddSubJob(new JobMoveAndClickMouse(_form, pos));
				});



			ActionSequenceTree fromParty =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromParty]");

					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.PartyAward, true));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree checkAwardPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkAwardPage]");

					AddSubJob(new JobCheckPage(_form, Globals.PartyRepeatAwardPage));
				});

			// if checkAwardPage completed
			ActionSequenceTree exitAwardPage =
				new ActionSequenceTree(() =>
				{
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.PartyRepeatAwardPage,
							Globals.OutFromRepeatAward, Globals.PartyPage));
				});

			ActionSequenceTree clickStart =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickStart]");
					AddSubJob(new JobMovePage(_form, Globals.PartyPage, Globals.StartParty, new string[] { Globals.PartyRestrictedPage, Globals.UnknownPage }));
				});

			ActionSequenceTree checkPartyRestricted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkPartyRestricted]");
					AddSubJob(new JobCheckPage(_form, Globals.PartyRestrictedPage));
				});

			// if checkPartyRestricted completed
			ActionSequenceTree exitPartyRestrictedPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitPartyRestrictedPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.PartyRestrictedPage,
							Globals.NoInPartyRestrictedPage, Globals.PartyPage));
				});

			ActionSequenceTree exitPartyPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitPartyPage]");
					AddSubJob(new JobMovePage(_form, Globals.PartyPage, Globals.BackInParty, Globals.MainPage));
				});

			ActionSequenceTree fromStarted =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromStarted]");
					AddSubJob(
						new JobCheckUntilMeetPage(
							_form, new string[] { Globals.PartyResultPage }));

				});

			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");
					AddSubJob(new JobMovePage(_form, Globals.PartyResultPage, Globals.ToMainFromPartyResult, Globals.MainPage));
				});

			if (_form.SkipPartyLevelSelect)
			{
				fromMain.Completed = fromParty;
			}
			else
			{
				fromMain.Completed = dragForFindLevel;
				dragForFindLevel.Completed = findLevel;
				findLevel.Completed = clickLevel;
				clickLevel.Completed = fromParty;
			}

			fromParty.Completed = checkAwardPage;
			checkAwardPage.Completed = exitAwardPage;
			checkAwardPage.Failed = clickStart;
			exitAwardPage.Completed = clickStart;
			clickStart.Completed = checkPartyRestricted;
			checkPartyRestricted.Completed = exitPartyRestrictedPage;
			checkPartyRestricted.Failed = fromStarted;
			exitPartyRestrictedPage.Completed = exitPartyPage;
			fromStarted.Completed = fromResult;
			fromResult.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.CoworkPage] = fromMain;
			_repairMap[Globals.PartyPage] = fromParty;
			_repairMap[Globals.PartyRepeatAwardPage] = exitAwardPage;
			_repairMap[Globals.PartyRestrictedPage] = exitPartyRestrictedPage;
			_repairMap[Globals.PartyResultPage] = fromResult;
		}
	}
	
}