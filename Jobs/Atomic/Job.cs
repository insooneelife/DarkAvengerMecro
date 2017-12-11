using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public abstract class Job
	{
		public enum JobTypes
		{
			// atomic
			MoveMouse, ClickMouse, WaitTime, CheckCurrentPage, CheckForAddJob, FindSubImage, FindOneOfSubImages, FindCircles,
			CheckPage, DragMouse, KillProcess, ExecuteProcess, CheckCondition,

			// composite
			Generator, MoveAndClickMouse, GoToMainPage, MovePage, TryFindSubImage, TryFindOneOfSubImages,

			// evaluate
			PlayParty, PlayRaid, PlayTraverse, ClearQuest, ExitArisAdvertise, Login, CheckMail, 
			RestartEmulator, UpgradeItem, TakeAbrasive, PlayGoldDungeon, PlayWeekDungeon,
			PlayTrial, Fishing, CompeteBattle
		}

		public enum State
		{
			Active, InActive, Completed, Failed
		}

		protected JobTypes _type;
		protected MainForm _form = null;
		protected State _status = State.InActive;
		private int _blackBoardAccessID = BlackBoard.CantAccess;

		public JobTypes Type
		{
			get { return _type; }
		}

		public State Status
		{
			get { return _status; }
		}

		public bool IsCompleted
		{
			get { return _status == State.Completed; }
		}

		public bool IsActive
		{
			get { return _status == State.Active; }
		}

		public bool IsInActive
		{
			get { return _status == State.InActive; }
		}

		public bool HasFailed
		{
			get { return _status == State.Failed; }
		}

		public int BlackBoardAccessID
		{
			get { return _blackBoardAccessID; }
		}

		public Job(JobTypes type, MainForm form, int blackBoardAccessID = BlackBoard.CantAccess)
		{
			_type = type;
			_form = form;
			_blackBoardAccessID = blackBoardAccessID;
		}

		public override string ToString()
		{
			return _type.ToString();
		}

		public abstract void Activate();

		public abstract State Process();

		public abstract void Terminate();

		public void ActivateIfInActive()
		{
			if (IsInActive)
			{
				Activate();
			}
		}

	}
}