using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobPlayGoldDungeon : JobSequence
	{
		public JobPlayGoldDungeon(MainForm form)
			:
			base(JobTypes.PlayGoldDungeon, form)
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
					AddSubJob(new JobMovePage(_form, Globals.TraversePage, Globals.GoldDungeon, Globals.GoldDungeonPage));
				});

				
			ActionSequenceTree fromGold =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromGold]");
					
					OpenCvSharp.Rect left;
					bool leftFlag =
						_form.AllUIRects.TryGetValue(Globals.GoldDragLeftPos, out left);

					OpenCvSharp.Rect right;
					bool rightFlag =
						_form.AllUIRects.TryGetValue(Globals.GoldDragRightPos, out right);

					if (leftFlag && rightFlag)
					{
						var start = Utils.RectToPos(right.X, right.Y, right.Width, right.Height);
						var end = Utils.RectToPos(left.X, left.Y, left.Width, left.Height);
						AddSubJob(new JobDrag(_form, start, end));
					}


					AddSubJob(new JobWaitTime(_form, 2000));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.GLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.GLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.GLevel5, true));
					AddSubJob(new JobWaitTime(_form, 750));

				});

			ActionSequenceTree checkGoldDungeonPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkGoldDungeonPage]");
					AddSubJob(new JobCheckPage(_form, Globals.GoldDungeonPage));
				});

			ActionSequenceTree exitGoldDungeonPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitGoldDungeonPage]");
					AddSubJob(new JobMovePage(_form, Globals.GoldDungeonPage, Globals.BackInGoldDungeon, Globals.MainPage));
				});

			ActionSequenceTree fromResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromResult]");
					AddSubJob(new JobMovePage(_form, Globals.GoldDungeonResultPage, Globals.ToMainFromGoldResult, Globals.MainPage));
				});
				

			fromMain.Completed = fromTraverse;
			fromTraverse.Completed = fromGold;
			fromGold.Completed = checkGoldDungeonPage;

			checkGoldDungeonPage.Completed = exitGoldDungeonPage;
			exitGoldDungeonPage.ActionOnCompleted = () => { _status = State.Failed; };

			checkGoldDungeonPage.Failed = fromResult;
			fromResult.ActionOnCompleted = () => { _status = State.Completed; };

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.TraversePage] = fromTraverse;
			_repairMap[Globals.GoldDungeonPage] = fromGold;
			_repairMap[Globals.GoldDungeonResultPage] = fromResult;
		}
	}
}
