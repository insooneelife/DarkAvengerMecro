using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobPlayWeekDungeon : JobSequence
	{
		public JobPlayWeekDungeon(MainForm form)
			:
			base(JobTypes.PlayWeekDungeon, form)
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
					AddSubJob(new JobMovePage(_form, Globals.MainPage, Globals.Traverse, Globals.TraversePage));
				});

			ActionSequenceTree fromTraverse =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromTraverse]");
					AddSubJob(new JobMovePage(_form, Globals.TraversePage, Globals.WeekDungeon, Globals.WeekDungeonPage));
				});


			ActionSequenceTree fromWeek =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromWeek]");
					OpenCvSharp.Rect left;
					bool leftFlag =
						_form.AllUIRects.TryGetValue(Globals.WeekDragLeftPos, out left);

					OpenCvSharp.Rect right;
					bool rightFlag =
						_form.AllUIRects.TryGetValue(Globals.WeekDragRightPos, out right);

					if (leftFlag && rightFlag)
					{
						var start = Utils.RectToPos(right.X, right.Y, right.Width, right.Height);
						var end = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
						AddSubJob(new JobDrag(_form, start, end));
					}

					AddSubJob(new JobWaitTime(_form, 2000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.WLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.WLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.WLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));

				});

			ActionSequenceTree checkWeekDungeonPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkWeekDungeonPage]");
					AddSubJob(new JobCheckPage(_form, Globals.WeekDungeonPage));
				});

			ActionSequenceTree exitWeekDungeonPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitWeekDungeonPage]");
					AddSubJob(new JobMovePage(_form, Globals.WeekDungeonPage, Globals.BackInWeekDungeon, Globals.MainPage));
				});

			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");
					AddSubJob(new JobMovePage(_form, Globals.WeekDungeonResultPage, Globals.ToMainFromWeekResult, Globals.MainPage));
				});


			fromMain.Completed = fromTraverse;
			fromTraverse.Completed = fromWeek;
			fromWeek.Completed = checkWeekDungeonPage;

			checkWeekDungeonPage.Completed = exitWeekDungeonPage;
			exitWeekDungeonPage.ActionOnCompleted = () => { _status = State.Failed; };

			checkWeekDungeonPage.Failed = fromResult;
			fromResult.ActionOnCompleted = () => { _status = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.TraversePage] = fromTraverse;
			_repairMap[Globals.WeekDungeonPage] = fromWeek;
			_repairMap[Globals.WeekDungeonResultPage] = fromResult;
		}
	}
}
