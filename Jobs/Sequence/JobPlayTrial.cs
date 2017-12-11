using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobPlayTrial : JobSequence
	{
		private string _currentPage;

		public enum TrialState
		{
			SweepAway, MansFight, WomansFight, TwoDevil, 
			StormEye, Bloody, Finished
		}
		private TrialState _currentState = TrialState.SweepAway;

		private Dictionary<string, string> _clickUI = new Dictionary<string, string>();

		private Dictionary<TrialState, string> _todo = new Dictionary<TrialState, string>();

		public JobPlayTrial(MainForm form)
			:
			base(JobTypes.PlayTrial, form)
		{
			MakeActionSequenceTree();

			_clickUI[Globals.TrialSweepAwayPage] = Globals.TrialStartLevelFirstFromRight;
			_clickUI[Globals.TrialMansFightPage] = Globals.TrialStartLevelSecondFromRight;
			_clickUI[Globals.TrialWomansFightPage] = Globals.TrialStartLevelSecondFromRight;
			_clickUI[Globals.TrialTwoDevilPage] = Globals.TrialStartLevel2;
			//_clickUI[Globals.TrialBattleGodPage] = Globals.TrialStartLevel3;
			_clickUI[Globals.TrialStormEyePage] = Globals.TrialStartLevel2;
			_clickUI[Globals.TrialBloodyPage] = Globals.TrialStartLevel2;
			//_clickUI[Globals.TrialEventPage] = Globals.TrialStartLevel2;

			_todo[TrialState.SweepAway] = Globals.TrialSweepAwayImg;
			_todo[TrialState.MansFight] = Globals.TrialMansFightImg;
			_todo[TrialState.WomansFight] = Globals.TrialWomansFightImg;
			_todo[TrialState.TwoDevil] = Globals.TrialTwoDevilImg;
			//_todo[TrialState.BattleGod] = Globals.TrialBattleGodImg;
			_todo[TrialState.StormEye] = Globals.TrialStormEyeImg;
			_todo[TrialState.Bloody] = Globals.TrialBloodyImg;			
		}

		protected override void OnJobCompleted()
		{
			_form.TrialCompletedCnt = _form.TrialCompletedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.TrialFailedCnt = _form.TrialFailedCnt + 1;
		}

		private static void Drag(MainForm form, JobPlayTrial job, System.Drawing.Point addStartPos)
		{
			OpenCvSharp.Rect left;
			bool leftFlag =
				form.AllUIRects.TryGetValue(Globals.TrialDragLeft, out left);

			OpenCvSharp.Rect right;
			bool rightFlag =
				form.AllUIRects.TryGetValue(Globals.TrialDragRight, out right);

			// drag from right pos -> left pos
			if (leftFlag && rightFlag)
			{
				var start = Utils.RectToPos(right.X + addStartPos.X, right.Y + addStartPos.Y, right.Width, right.Height);
				var end = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
				job.AddSubJob(new JobDrag(form, start, end));
			}
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree fromTrial =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromTrial]");
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Trial, Globals.TrialPage));
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.TrialPage }));
				});

			ActionSequenceTree findBeforeDrag =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[findBeforeDrag]");

					string what = "";
					if (_todo.TryGetValue(_currentState, out what))
					{
						AddSubJob(new JobTryFindSubImage(_form, what));
					}
				});

			ActionSequenceTree click =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[click]");
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);
					var pos = Utils.RectToPos(rect.X + 50, rect.Y + 100, rect.Width, rect.Height);
					AddSubJob(new JobMoveAndClickMouse(_form, pos));
				});


			ActionSequenceTree drag =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[drag]");
					AddSubJob(new JobWaitTime(_form, 1500));
					Drag(_form, this, new System.Drawing.Point(0, 0));
					AddSubJob(new JobWaitTime(_form, 1000));
					Drag(_form, this, new System.Drawing.Point(-100, 0));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree findAfterDrag =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[findAfterDrag]");

					string what = "";
					if (_todo.TryGetValue(_currentState, out what))
					{ 
						AddSubJob(new JobTryFindSubImage(_form, what));
					}
				});


			ActionSequenceTree fromTrialStage =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[fromTrialStage]");
					AddSubJob(new JobWaitTime(_form, 1500));
					Drag(_form, this, new System.Drawing.Point(0, 0));
					AddSubJob(new JobWaitTime(_form, 1000));
					Drag(_form, this, new System.Drawing.Point(-100, 0));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree getCurrentPage =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[getCurrentPage]");
					AddSubJob(new JobWaitTime(_form, 3000));
					_currentPage = _form.CurrentPageName;
				});


			ActionSequenceTree start =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[start]  current page : " + _currentPage);

					string ui;
					if (_clickUI.TryGetValue(_currentPage, out ui))
					{
						AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, ui, true));
						AddSubJob(new JobWaitTime(_form, 1000));
						AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, ui, true));
						AddSubJob(new JobWaitTime(_form, 1000));
					}
				});

			ActionSequenceTree checkNoPageMove =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkCurrentPage]");
					AddSubJob(new JobCheckPage(_form, _currentPage, 5000));
				});


			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");

					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ToTrialFromTrialResult, true));
					AddSubJob(new JobWaitTime(_form, 1000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ToTrialFromTrialResult, true));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree checkCurrentPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkCurrentPage]");
					AddSubJob(new JobCheckPage(_form, Globals.TrialPage, 8000));
				});

			ActionSequenceTree backToTrial =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[backToTrial]");

					AddSubJob(new JobMovePage(_form, _currentPage, Globals.BackInTrialLevelPages, Globals.TrialPage));
				});

			ActionSequenceTree checkCurrentState =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkCurrentState]");
					AddSubJob(new JobCheckCondition(_form, () => { return _currentState == TrialState.Finished; }));
				});

			ActionSequenceTree exitTrial =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitTrial]");
					AddSubJob(new JobMovePage(_form, Globals.TrialPage, Globals.BackInTrial, Globals.MainPage));
				});
				
			fromTrial.Completed = checkCurrentState;

			checkCurrentState.Completed = exitTrial;
			checkCurrentState.Failed = findBeforeDrag;

			findBeforeDrag.Completed = click;
			findBeforeDrag.Failed = drag;
			drag.Completed = findAfterDrag;
			findAfterDrag.Completed = click;
			findAfterDrag.Failed = fromTrial;
			findAfterDrag.ActionOnFailed = () => { UpdateTrialState(); };

			click.Completed = fromTrialStage;
			fromTrialStage.Completed = getCurrentPage;
			getCurrentPage.Completed = start;
			start.Completed = checkNoPageMove;

			// not started
			checkNoPageMove.Completed = backToTrial;
			checkNoPageMove.Failed = fromResult;
			
			fromResult.Completed = checkCurrentPage;

			checkCurrentPage.Completed = fromTrial;
			checkCurrentPage.Failed = backToTrial;
			backToTrial.Completed = fromTrial;
			backToTrial.ActionOnCompleted = () => { UpdateTrialState(); };

			

			exitTrial.ActionOnCompleted = () =>
			{
				if (_currentState == TrialState.Finished)
				{
					Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
					Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
					Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
					_returnState = State.Completed;
				}
				else
				{
					Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
					Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
					Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
					_returnState = State.Failed;
				}
			};

			_currentAction = fromTrial;

			// make repair map
			_repairMap[Globals.TrialPage] = fromTrial;
			_repairMap[Globals.TrialSweepAwayPage] = fromTrialStage;
			_repairMap[Globals.TrialMansFightPage] = fromTrialStage;
			_repairMap[Globals.TrialWomansFightPage] = fromTrialStage;
			_repairMap[Globals.TrialTwoDevilPage] = fromTrialStage;
			//_repairMap[Globals.TrialBattleGodPage] = fromTrialStage;
			_repairMap[Globals.TrialStormEyePage] = fromTrialStage;
			_repairMap[Globals.TrialBloodyPage] = fromTrialStage;
			_repairMap[Globals.TrialResultPage] = fromResult;
		}

		void UpdateTrialState()
		{
			if (_currentState == TrialState.SweepAway)
			{
				_currentState = TrialState.MansFight;
			}
			else if (_currentState == TrialState.MansFight)
			{
				_currentState = TrialState.WomansFight;
			}
			else if (_currentState == TrialState.WomansFight)
			{
				_currentState = TrialState.TwoDevil;
			}
			else if (_currentState == TrialState.TwoDevil)
			{
				_currentState = TrialState.StormEye;
			}
			else if (_currentState == TrialState.StormEye)
			{
				_currentState = TrialState.Bloody;
			}
			else if (_currentState == TrialState.Bloody)
			{
				_currentState = TrialState.Finished;
			}

			Console.WriteLine("Current state : [" + _currentState + "] ##########################################");
		}
	}
}
