using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public abstract class JobSequence : JobComposite
	{
		protected ActionSequenceTree _currentAction = null;
		protected DateTime _changeStartTime;
		protected Dictionary<string, ActionSequenceTree> _repairMap = 
			new Dictionary<string, ActionSequenceTree>();

		protected List<Func<bool>> _failConditions = new List<Func<bool>>();

		protected State _returnState = State.Failed;
		protected double _elapsed = 0;
		protected int _repairPeriod = 0;

		protected string _startingPage = Globals.MainPage;
		protected bool _repairByForce = false;

		public JobSequence(JobTypes type, MainForm form, int repairPeriod = 0)
			:
			base(type, form)
		{
			if (repairPeriod <= 0)
			{
				_repairPeriod = _form.RepairPeriod;
			}
			else
			{ 
				_repairPeriod = repairPeriod;
			}

			MakeActionSequenceTree();
			MakeFailConditions();
		}

		public override void Activate()
		{
			_status = State.Active;
			_changeStartTime = DateTime.Now;
			_currentAction.Action();

			var page = _form.CurrentPageName;

			if (page != Globals.UnknownPage && page != _startingPage && _startingPage != Globals.EveryPage)
			{
				_repairByForce = true;
			}
		}


		public override State Process()
		{
			ActivateIfInActive();

			var endTime = DateTime.Now;
			_elapsed = (endTime - _changeStartTime).TotalMilliseconds;

			if (CheckFail())
			{
				return _status = State.Failed;
			}


			Console.WriteLine(_elapsed);
			if (_elapsed > _repairPeriod || _repairByForce)
			{
				return _status = Repair();
			}

			var status = ProcessSubJobs();

			if (status == State.Completed || status == State.Failed)
			{
				if (_currentAction != null)
				{
					Action action = null;
					ActionSequenceTree newActionTree = null;

					if (status == State.Completed)
					{
						action = _currentAction.ActionOnCompleted;
						newActionTree = _currentAction.Completed;
					}
					else
					{
						action = _currentAction.ActionOnFailed;
						newActionTree = _currentAction.Failed;
					}

					if (action != null)
					{
						action();
					}

					SetCurrentAction(newActionTree);

					if (_currentAction != null)
					{
						_currentAction.Action();
					}
				}
				else
				{
					_status = _returnState;

					if (_returnState == State.Completed)
					{
						OnJobCompleted();
					}

					else if (_returnState == State.Failed)
					{
						OnJobFailed();
					}
				}
			}

			return _status;
		}

		public override void Terminate()
		{ }

		protected abstract void OnJobCompleted();

		protected abstract void OnJobFailed();

		protected abstract void MakeActionSequenceTree();

		protected virtual void MakeFailConditions()
		{
		}

		private void SetCurrentAction(ActionSequenceTree actionTree)
		{
			_currentAction = actionTree;
			_changeStartTime = DateTime.Now;
		}
		private State Repair()
		{
			Console.WriteLine("Repair " + _type + "..");
			RemoveAllSubJobs();

			var pageName = _form.CurrentPageName;
			ActionSequenceTree actionTree = null;

			if (pageName == Globals.UnknownPage)
			{
			}
			else if (_repairMap.TryGetValue(pageName, out actionTree))
			{
				_repairByForce = false;
				SetCurrentAction(actionTree);
				actionTree.Action();
			}
			else
			{
				OnJobFailed();
				return State.Failed;
			}

			return _status = State.Active;

		}

		private bool CheckFail()
		{
			foreach (var f in _failConditions)
			{
				if (f())
				{
					Console.WriteLine("Failed!!!!!!!!!!");
					return true;
				}
			}
			return false;
		}
	}
}
