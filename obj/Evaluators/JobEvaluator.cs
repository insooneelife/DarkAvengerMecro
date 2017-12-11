using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public abstract class JobEvaluator
	{
		protected JobGenerator _jobGenerator = null;

		protected bool _always = false;
		protected float _evaluateValue = 0.0f;

		protected Action<string> _onEvaluate = null;

		public void SetOnEvaluate(Action<string> onEvaluate)
		{
			_onEvaluate = onEvaluate;
		}
	
		public abstract Job.JobTypes JobType { get; }

		public bool Always
		{
			get { return _always; }
			set { _always = value; }
		}

		public JobEvaluator(JobGenerator generator)
		{
			_jobGenerator = generator;
		}

		public abstract float Evaluate();
		public abstract Job CreateJob();

		public virtual void Reset() { _always = false; }

		protected void OnEvaluate()
		{
			if (_onEvaluate != null)
			{
				string value = "";
				if (_evaluateValue == float.MaxValue)
				{
					value = "Infinite";
				}
				else
				{
					value = _evaluateValue.ToString("0.00");
				}

				_onEvaluate(value);
			}
		}
	}


	public class PlayPartyEvaluator : JobEvaluator
	{
		private float _partyClearWeight = 20.0f;
		private float _partyFailWeight = 160.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.PlayParty; }
		}

		public PlayPartyEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.PartyClearedCnt * _partyClearWeight
						+ _jobGenerator.Form.PartyFailedCnt * _partyFailWeight);
			}

			OnEvaluate();

			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobPlayParty(_jobGenerator.Form, _jobGenerator.Form.PartyLevelId);
		}
	}

	public class PlayRaidEvaluator : JobEvaluator
	{
		private float _raidClearWeight = 40.0f;
		private float _raidFailWeight = 320.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.PlayRaid; }
		}


		public PlayRaidEvaluator(JobGenerator generator)
			: base(generator)
		{}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.RaidClearedCnt * _raidClearWeight
					+ _jobGenerator.Form.RaidFailedCnt * _raidFailWeight);
			}
			
			OnEvaluate();

			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobPlayRaid(_jobGenerator.Form, _jobGenerator.Form.RaidLevelId);
		}
	}

	public class PlayTraverseEvaluator : JobEvaluator
	{
		private float _traverseClearWeight = 20f;
		private float _traverseFailWeight = 100f;

		public override Job.JobTypes JobType 
		{
			get { return Job.JobTypes.PlayTraverse; }
		}
		

		public PlayTraverseEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate() 
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.TraverseClearedCnt * _traverseClearWeight
					  + _jobGenerator.Form.TraverseFailedCnt * _traverseFailWeight);
					  
			}
			
			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob() 
		{
			return new JobPlayTraverse(_jobGenerator.Form, _jobGenerator.Form.TraverseLevelId); 
		}
	}

	public class FishingEvaluator : JobEvaluator
	{
		private float _fishingWeight = 750f;
		private float _fishingFailWeight = 100f;
		private float _startDelayWeight = 1000f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.Fishing; }
		}


		public FishingEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (!_jobGenerator.Form.FishingAvailable)
			{
				return -float.MaxValue;
			}

			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.FishingCompletedCnt * _fishingWeight
					  + _jobGenerator.Form.FishingFailedCnt * _fishingFailWeight)
					  - _startDelayWeight;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobFishing(_jobGenerator.Form);
		}
	}

	public class ClearQuestEvaluator : JobEvaluator
	{
		private float _questClearWeight = 100.0f;
		private float _questFailWeight = 250.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.ClearQuest; }
		}


		public ClearQuestEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				return _evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.QuestClearedCnt * _questClearWeight
					  + _jobGenerator.Form.QuestFailedCnt * _questFailWeight);
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobClearQuest(_jobGenerator.Form);
		}
	}

	public class ExitArisAdvertiseEvaluator : JobEvaluator
	{

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public ExitArisAdvertiseEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{ 
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.DontSee, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}


	public class ExitOneFlusAdvertiseEvaluator : JobEvaluator
	{

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public ExitOneFlusAdvertiseEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job1 = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.DontSeeInOneFlus, true);
			job1.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));

			var job2 = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.ExitOneFlusAdvertise, true);
			job2.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));

			job1.AddSubJob(job2);

			return job1;			
		}
	}

	public class CloseServerConnectFailEvaluator : JobEvaluator
	{

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public CloseServerConnectFailEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{ 
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.CloseServerConnectFail, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}

	public class CloseLoginFailEvaluator : JobEvaluator
	{

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public CloseLoginFailEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.OkInLoginFailPage, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}

	public class CloseOptionEvaluator : JobEvaluator
	{

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public CloseOptionEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.OutFromOption, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}

	public class StartInGameStopEvaluator : JobEvaluator
	{
		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public StartInGameStopEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.KeepGoingInStopPage, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}

	public class OutFromTraverseDeadEvaluator : JobEvaluator
	{
		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.MoveAndClickMouse; }
		}


		public OutFromTraverseDeadEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = float.MinValue;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			var job = JobGenerator.CreateJobMoveAndClickMouse(_jobGenerator.Form, Globals.OutFromTraverseDeadPage, true);
			job.AddSubJob(new JobWaitTime(_jobGenerator.Form, 750));
			return job;
		}
	}

	public class CheckMailEvaluator : JobEvaluator
	{
		private float _checkMailWeight = 300.0f;
		private float _checkMailFailWeight = 501.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.CheckMail; }
		}


		public CheckMailEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.MailCheckedCnt * _checkMailWeight
						+ _jobGenerator.Form.MailCheckFailedCnt * _checkMailFailWeight);
			}
			
			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobCheckMail(_jobGenerator.Form);
		}
	}

	public class RestartEmulatorEvaluator : JobEvaluator
	{
		const float StartWeight = -93 * 1000 * 60;
		private DateTime _startTime = DateTime.Now;
		

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.RestartEmulator; }
		}


		public RestartEmulatorEvaluator(JobGenerator generator)
			: base(generator)
		{
			Reset();
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				var endTime = DateTime.Now;
				var elapsed = (endTime - _startTime).TotalMilliseconds;
				//var second = elapsed / 1000;

				_evaluateValue = (float)(StartWeight + elapsed);
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobRestartEmulator(_jobGenerator.Form);
		}
		
		public override void Reset() 
		{
			base.Reset();
			_startTime = DateTime.Now;
		}
	}


	public class UpgradeItemEvaluator : JobEvaluator
	{
		private float _upgradeItemWeight = 30.0f;
		private float _upgradeItemFailedWeight = 400.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.UpgradeItem; }
		}


		public UpgradeItemEvaluator(JobGenerator generator)
			: base(generator)
		{
			Reset();
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.ItemUpgradeCnt * _upgradeItemWeight +
				_jobGenerator.Form.ItemUpgradeFailedCnt * _upgradeItemFailedWeight) + 1.0f;
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobUpgradeItem(
				_jobGenerator.Form,
				new string[] { Globals.ItemBasicImg, Globals.ItemHighImg, Globals.ItemRareImg });
		}
	}


	public class TakeAbrasiveEvaluator : JobEvaluator
	{
		const float StartWeight = -30000 * 60;
		private DateTime _startTime = DateTime.Now;
		

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.TakeAbrasive; }
		}


		public TakeAbrasiveEvaluator(JobGenerator generator)
			: base(generator)
		{
			Reset();
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				var endTime = DateTime.Now;
				var elapsed = (endTime - _startTime).TotalMilliseconds;
				_evaluateValue = (float)(StartWeight + elapsed);
			}

			OnEvaluate();
			return _evaluateValue;
		}
		

		public override Job CreateJob()
		{
			return new JobTakeAbrasive(_jobGenerator.Form);
		}

		public override void Reset()
		{
			base.Reset();
			_startTime = DateTime.Now;
		}
	}


	public class PlayGoldEvaluator : JobEvaluator
	{
		const float StartWeight = -30000 * 60;
		private DateTime _startTime = DateTime.Now;


		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.PlayGoldDungeon; }
		}


		public PlayGoldEvaluator(JobGenerator generator)
			: base(generator)
		{
			Reset();
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				var endTime = DateTime.Now;
				var elapsed = (endTime - _startTime).TotalMilliseconds;
				_evaluateValue = (float)(StartWeight + elapsed);
			}

			OnEvaluate();
			return _evaluateValue;
		}


		public override Job CreateJob()
		{
			return new JobPlayGoldDungeon(_jobGenerator.Form);
		}

		public override void Reset()
		{
			base.Reset();
			_startTime = DateTime.Now;
		}
	}

	public class PlayWeekEvaluator : JobEvaluator
	{
		const float StartWeight = -60000 * 60;
		private DateTime _startTime = DateTime.Now;


		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.PlayWeekDungeon; }
		}


		public PlayWeekEvaluator(JobGenerator generator)
			: base(generator)
		{
			Reset();
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				var endTime = DateTime.Now;
				var elapsed = (endTime - _startTime).TotalMilliseconds;
				_evaluateValue = (float)(StartWeight + elapsed);
			}

			OnEvaluate();
			return _evaluateValue;
		}


		public override Job CreateJob()
		{
			return new JobPlayWeekDungeon(_jobGenerator.Form);
		}

		public override void Reset()
		{
			base.Reset();
			_startTime = DateTime.Now;
		}
	}



	public class PlayTrialEvaluator : JobEvaluator
	{
		private float _trialWeight = 1000f;
		private float _trialFailWeight = 80f;
		private float _startDelayWeight = 500f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.PlayTrial; }
		}


		public PlayTrialEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				if (_jobGenerator.Form.TrialFailedCnt > 10)
					return -float.MaxValue;

				_evaluateValue = -(_jobGenerator.Form.TrialCompletedCnt * _trialWeight
					  + _jobGenerator.Form.TrialFailedCnt * _trialFailWeight)
					  - _startDelayWeight + (float)Utils.random.NextDouble();
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobPlayTrial(_jobGenerator.Form);
		}
	}


	public class CompeteBattleEvaluator : JobEvaluator
	{
		private float _competeBattleWeight = 501.0f;
		private float _competeBattleFailWeight = 1001.0f;

		public override Job.JobTypes JobType
		{
			get { return Job.JobTypes.CompeteBattle; }
		}


		public CompeteBattleEvaluator(JobGenerator generator)
			: base(generator)
		{
		}

		public override float Evaluate()
		{
			if (_always)
			{
				_evaluateValue = float.MaxValue;
			}
			else
			{
				_evaluateValue = -(_jobGenerator.Form.CompeteCompletedCnt * _competeBattleWeight
						+ _jobGenerator.Form.CompeteFailedCnt * _competeBattleFailWeight);
			}

			OnEvaluate();
			return _evaluateValue;
		}

		public override Job CreateJob()
		{
			return new JobCompeteBattle(_jobGenerator.Form);
		}
	}
}
