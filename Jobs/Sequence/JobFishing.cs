using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	

	public class JobFishing : JobSequence
	{
		private static void DragJoystick(MainForm form, JobFishing job, System.Drawing.Point add)
		{
			OpenCvSharp.Rect area;
			bool leftFlag =
				form.AllUIRects.TryGetValue(Globals.Joystick, out area);

			// drag from right pos -> left pos
			if (leftFlag )
			{
				var center = Utils.RectToPos(area.X, area.Y, area.Width, area.Height);
				var target = new System.Drawing.Point(center.X + add.X, center.Y + add.Y);

				job.AddSubJob(new JobDrag(form, center, target));
			}
		}

		private int _drag1Cnt = 0;
		private int _drag2Cnt = 0;
		private string _bait = Globals.DefaultBait;
		protected DateTime _fishingStartTime;
		protected double _fishingElapsed = 0;

		const double _fishingTime = 25 * 60 * 1000;

		public JobFishing(MainForm form)
			:
			base(JobTypes.Fishing, form, int.MaxValue)
		{
			MakeActionSequenceTree();
		}

		public override State Process()
		{
			var endTime = DateTime.Now;
			_fishingElapsed = (endTime - _fishingStartTime).TotalMilliseconds;

			return base.Process();
		}

		protected override void OnJobCompleted()
		{
			_form.FishingCompletedCnt = _form.FishingCompletedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.FishingFailedCnt = _form.FishingFailedCnt + 1;
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
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.MenuToGuild, Globals.GuildPage));
				});


			ActionSequenceTree fromGuild =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromGuild]");
				});

			ActionSequenceTree checkFishingPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkFishingPage]");
					AddSubJob(new JobCheckPage(_form, Globals.FishingPage, 750));
				});

			ActionSequenceTree checkBreakAway =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkBreakAway]");
					AddSubJob(new JobCheckCondition(_form, () => { return _drag2Cnt > 25; }));
				});

			ActionSequenceTree exitFishingByBreakAway =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitFishingByBreakAway]");
					AddSubJob(new JobMovePage(_form, _form.CurrentPageName, Globals.ToMainPageFromFishing, Globals.MainPage));
				});

			ActionSequenceTree checkCondition =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkCondition]");
					AddSubJob(new JobCheckCondition(_form, () => { return _drag1Cnt > 10; }));
				});


			ActionSequenceTree drag1 =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[drag1]");

					if (_form.CurrentPageName != Globals.FishingPage)
					{
						DragJoystick(_form, this, new System.Drawing.Point(-50, 40));
						_drag1Cnt++;
					}
				});

			ActionSequenceTree drag2 =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[drag2]");
					if (_form.CurrentPageName != Globals.FishingPage)
					{
						if (_drag2Cnt == 0)
						{
							DragJoystick(_form, this, new System.Drawing.Point(15, -60));
						}
						else
						{
							DragJoystick(_form, this, new System.Drawing.Point(-50, -60));
						}
						_drag2Cnt++;

					}
				});

			ActionSequenceTree goToFishing =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[goToFishing]");

					AddSubJob(new JobMovePage(_form, Globals.FishingPage, Globals.GoToFishing, Globals.FishingInPage));
				});

			ActionSequenceTree readyToFish =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[readyToFish]");

					_fishingStartTime = DateTime.Now;

					AddSubJob(new JobWaitTime(_form, 1000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, _bait, true));

					AddSubJob(new JobWaitTime(_form, 1000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.GetList, true));
				
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree findForCheckNotFishing =
				new ActionSequenceTree(
				() =>
				{
					Console.WriteLine("[findForCheckNotFishing]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.StartFishingImg));
				});

			ActionSequenceTree clickToStartFishing =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickToStartFishing]");

					AddSubJob(new JobWaitTime(_form, 1000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.StartFishing, true));
					AddSubJob(new JobWaitTime(_form, 1000));
				});

			ActionSequenceTree checkFinish =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkFinish] fishingElapsed : " + _fishingElapsed + "  drag2Cnt : " + _drag2Cnt);
					AddSubJob(new JobCheckCondition(_form, () => { return _fishingElapsed > _fishingTime; }));
				});

			ActionSequenceTree exitFishing =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitFishing]");
					AddSubJob(new JobMovePage(_form, Globals.FishingInPage, Globals.StopFishing, Globals.FishingFinishPage));
					AddSubJob(new JobMovePage(_form, Globals.FishingFinishPage, Globals.YesInFishingFinishPage, Globals.FishingPage));
					AddSubJob(new JobMovePage(_form, Globals.FishingPage, Globals.ToMainPageFromFishing, Globals.MainPage));
				});


			fromMain.Completed = fromMenu;
			fromMenu.Completed = fromGuild;
			fromGuild.Completed = checkBreakAway;

			checkBreakAway.Completed = exitFishingByBreakAway;
			exitFishingByBreakAway.Completed = null;

			checkBreakAway.Failed = checkFishingPage;

			

			checkFishingPage.Completed = goToFishing;
			checkFishingPage.Failed = checkCondition;
			checkCondition.Completed = drag2;
			checkCondition.Failed = drag1;
			drag1.Completed = checkBreakAway;
			drag2.Completed = checkBreakAway;

			goToFishing.Completed = readyToFish;
			readyToFish.Completed = findForCheckNotFishing;

			findForCheckNotFishing.Completed = clickToStartFishing;
			clickToStartFishing.Completed = findForCheckNotFishing;
			findForCheckNotFishing.Failed = checkFinish;
			
			checkFinish.Failed = checkFinish;
			checkFinish.Completed = exitFishing;
			exitFishing.Completed = null;
			exitFishing.ActionOnCompleted = () => { _returnState = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			//_repairMap[Globals.TraversePage] = fromTraverse;
			//_repairMap[Globals.WeekDungeonPage] = fromWeek;
			//_repairMap[Globals.WeekDungeonResultPage] = fromResult;
		}
	}
}
