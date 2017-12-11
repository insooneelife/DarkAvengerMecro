using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using System.IO;

namespace SamplesCS.Jobs
{
	public class JobGenerator : JobComposite
	{		
		public static JobMoveAndClickMouse CreateJobMoveAndClickMouse(
			MainForm form, string uiName, bool noise = false)
		{
			OpenCvSharp.Rect rect;
			if (form.AllUIRects.TryGetValue(uiName, out rect))
			{
				var pos = Utils.RectToPos(rect.X, rect.Y, rect.Width, rect.Height);

				return new JobMoveAndClickMouse(form, pos);
			}
			return null;
		}
		
		private List<JobEvaluator> _jobEvaluators = new List<JobEvaluator>();
		private Dictionary<string, JobEvaluator> _pageMapForGenJob = new Dictionary<string, JobEvaluator>();
		private DateTime _startTime;

		public MainForm Form
		{
			get { return _form; } 
		}	
		
		public List<Job> SubJobs
		{
			get { return _subJobs; }
		}

		public JobGenerator(MainForm form)
			:
			base(JobTypes.Generator, form)
		{
		

			var traverse = new PlayTraverseEvaluator(this);
			traverse.SetOnEvaluate(form.SetTraverseEvaluateText);

			var party = new PlayPartyEvaluator(this);
			party.SetOnEvaluate(form.SetPartyEvaluateText);

			var raid = new PlayRaidEvaluator(this);
			raid.SetOnEvaluate(form.SetRaidEvaluateText);

			var quest = new ClearQuestEvaluator(this);
			quest.SetOnEvaluate(form.SetQuestEvaluateText);

			var arisAdvertise = new ExitArisAdvertiseEvaluator(this);
			//arisAdvertise.SetOnEvaluate(form.SetPartyEvaluateText);

			var oneFlusAdvertise = new ExitOneFlusAdvertiseEvaluator(this);

			var outFromTraverseDead = new OutFromTraverseDeadEvaluator(this);

			var mail = new CheckMailEvaluator(this);
			mail.SetOnEvaluate(form.SetMailEvaluateText);

			var restart = new RestartEmulatorEvaluator(this);
			restart.SetOnEvaluate(form.SetRestartEvaluateText);

			var serverConnectFail = new CloseServerConnectFailEvaluator(this);
			//serverConnectFail.SetOnEvaluate(form.SetPartyEvaluateText);

			var loginFail = new CloseLoginFailEvaluator(this);

			var closeOption = new CloseOptionEvaluator(this);

			var inGameStop = new StartInGameStopEvaluator(this);

			var itemUpgrade = new UpgradeItemEvaluator(this);
			itemUpgrade.SetOnEvaluate(form.SetItemUpgradeEvaluateText);

			var abrasive = new TakeAbrasiveEvaluator(this);

			var gold = new PlayGoldEvaluator(this);

			var playWeek = new PlayWeekEvaluator(this);

			var playTrial = new PlayTrialEvaluator(this);
			playTrial.SetOnEvaluate(form.SetTrialEvaluateText);

			var fishing = new FishingEvaluator(this);
			fishing.SetOnEvaluate(form.SetFishingEvaluateText);

			var competeBattle = new CompeteBattleEvaluator(this);
			competeBattle.SetOnEvaluate(form.SetCompeteEvaluateText);

			_jobEvaluators.Add(traverse);
			_jobEvaluators.Add(party);
			_jobEvaluators.Add(raid);
			_jobEvaluators.Add(restart);
			_jobEvaluators.Add(quest);
			_jobEvaluators.Add(arisAdvertise);
			_jobEvaluators.Add(oneFlusAdvertise);
			_jobEvaluators.Add(outFromTraverseDead);
			_jobEvaluators.Add(mail);
			_jobEvaluators.Add(serverConnectFail);
			_jobEvaluators.Add(loginFail);
			_jobEvaluators.Add(closeOption);
			_jobEvaluators.Add(inGameStop);
			_jobEvaluators.Add(itemUpgrade);
			_jobEvaluators.Add(abrasive);
			_jobEvaluators.Add(gold);
			_jobEvaluators.Add(playWeek);
			_jobEvaluators.Add(playTrial);
			_jobEvaluators.Add(fishing);
			_jobEvaluators.Add(competeBattle);

			// others
			_pageMapForGenJob[Globals.MenuPage] = quest;
			_pageMapForGenJob[Globals.WeekQuestPage] = quest;
			_pageMapForGenJob[Globals.MailPage] = mail;
			_pageMapForGenJob[Globals.ArisAdvertisePage] = arisAdvertise;
			_pageMapForGenJob[Globals.ServerConnectFailPage] = serverConnectFail;
			_pageMapForGenJob[Globals.LoginFailPage] = loginFail;
			_pageMapForGenJob[Globals.OneFlusAdvertisePage] = oneFlusAdvertise;
			_pageMapForGenJob[Globals.OptionPage] = closeOption;
			_pageMapForGenJob[Globals.InGameStopPage] = inGameStop;
			_pageMapForGenJob[Globals.TraverseDeadPage] = outFromTraverseDead;
			
			// party
			_pageMapForGenJob[Globals.CoworkPage] = party;
			_pageMapForGenJob[Globals.PartyPage] = party;
			_pageMapForGenJob[Globals.PartyResultPage] = party;
			_pageMapForGenJob[Globals.PartyRestrictedPage] = party;
			_pageMapForGenJob[Globals.PartyRepeatAwardPage] = party;

			// raid
			_pageMapForGenJob[Globals.RaidPage] = raid;
			_pageMapForGenJob[Globals.RaidResultPage] = raid;
			_pageMapForGenJob[Globals.RaidRestrictedPage] = raid;
			_pageMapForGenJob[Globals.RaidRepeatAwardPage] = raid;
			
			// traverse
			_pageMapForGenJob[Globals.TraversePage] = traverse;
			_pageMapForGenJob[Globals.TraverseDungeonPage] = traverse;
			_pageMapForGenJob[Globals.TraverseDungeonBlankPage] = traverse;
			_pageMapForGenJob[Globals.TraverseStagePage] = traverse;
			_pageMapForGenJob[Globals.TraverseResultPage] = traverse;
			_pageMapForGenJob[Globals.TraverseRestrictedPage] = traverse;
			
			// restart
			_pageMapForGenJob[Globals.EmulatorPage] = restart;
			_pageMapForGenJob[Globals.StartGamePage] = restart;
			_pageMapForGenJob[Globals.CharacterSelectPage] = restart;
			_pageMapForGenJob[Globals.EventPage1] = restart;
			_pageMapForGenJob[Globals.EventPage2] = restart;
			_pageMapForGenJob[Globals.NotifyPage] = restart;
			_pageMapForGenJob[Globals.LoginPage] = restart;
			_pageMapForGenJob[Globals.FacebookPage] = restart;
			_pageMapForGenJob[Globals.FacebookConfirmPage] = restart;
			
			// item
			_pageMapForGenJob[Globals.ItemPage] = itemUpgrade;
			_pageMapForGenJob[Globals.ItemDetailPage] = itemUpgrade;
			_pageMapForGenJob[Globals.ItemUpgradePage] = itemUpgrade;
			_pageMapForGenJob[Globals.ItemLockPage] = itemUpgrade;
			_pageMapForGenJob[Globals.ItemUpgradeResultPage] = itemUpgrade;
			_pageMapForGenJob[Globals.ItemGetVisualPage] = itemUpgrade;

			// forge
			_pageMapForGenJob[Globals.ForgePage] = abrasive;
			_pageMapForGenJob[Globals.ForgeAbrasivePage] = abrasive;
			_pageMapForGenJob[Globals.ForgeAbrasiveGemPage] = abrasive;
			_pageMapForGenJob[Globals.ForgeAbrasiveRestrictedPage] = abrasive;

			// gold
			_pageMapForGenJob[Globals.GoldDungeonPage] = gold;
			_pageMapForGenJob[Globals.GoldDungeonResultPage] = gold;
			
			// week
			_pageMapForGenJob[Globals.WeekDungeonPage] = playWeek;
			_pageMapForGenJob[Globals.WeekDungeonResultPage] = playWeek;

			// trial
			_pageMapForGenJob[Globals.TrialPage] = playTrial;
			_pageMapForGenJob[Globals.TrialSweepAwayPage] = playTrial;
			_pageMapForGenJob[Globals.TrialMansFightPage] = playTrial;
			_pageMapForGenJob[Globals.TrialWomansFightPage] = playTrial;
			_pageMapForGenJob[Globals.TrialTwoDevilPage] = playTrial;
			_pageMapForGenJob[Globals.TrialStormEyePage] = playTrial;
			_pageMapForGenJob[Globals.TrialBloodyPage] = playTrial;
			_pageMapForGenJob[Globals.TrialResultPage] = playTrial;

			// fishing
			_pageMapForGenJob[Globals.GuildPage] = fishing;
			_pageMapForGenJob[Globals.FishingPage] = fishing;
			_pageMapForGenJob[Globals.FishingInPage] = fishing;
			_pageMapForGenJob[Globals.FishingFinishPage] = fishing;

			// compete battle
			_pageMapForGenJob[Globals.CompeteBattlePage] = competeBattle;
			_pageMapForGenJob[Globals.CompeteBattleRewardPage] = competeBattle;
			_pageMapForGenJob[Globals.CompeteBattleResultPage] = competeBattle;
			_pageMapForGenJob[Globals.CompeteBattleRestrictedPage] = competeBattle;
		}

		

		public override void Activate()
		{
			_status = State.Active;
			_startTime = DateTime.Now;

			var pageName = _form.CurrentPageName;

			// check current page, and generate suitable one for current page
			JobEvaluator jobEvaluator = null;
			if (_pageMapForGenJob.TryGetValue(pageName, out jobEvaluator))
			{
				jobEvaluator.Always = true;
			}
			

			JobEvaluator bestJobEval = null;
			float bestWeight = float.MinValue;
			
			foreach (var e in _jobEvaluators)
			{
				float weight = e.Evaluate();
				if (weight > bestWeight)
				{
					bestJobEval = e;
					bestWeight = weight;
				}
				Console.WriteLine(e.JobType + " weight : " + weight);
			}
			
			if (bestJobEval != null)
			{
				Console.WriteLine("[JobGenerator] created new job : [" + bestJobEval.JobType + "]");
				Console.WriteLine();

				//AddSubJob(new JobClearQuest(_form));

				//AddSubJob(new JobFindSubImage(_form, Globals.ItemHigh));

				//AddSubJob(new JobPressKeyboard(_form, "insooneelife@nate.com"));

				/*AddSubJob(
					new JobDrag(
						_form, 
						new System.Drawing.Point(2400, 400), 
						new System.Drawing.Point(2700, 400)));

				AddSubJob(new JobWaitTime(_form, 10000));*/

				//AddSubJob(new JobCheckMail(_form));

				//AddSubJob(new JobRestartEmulator(_form));

				//AddSubJob(new JobUpgradeItem(_form, new string[] { Globals.ItemBasicImg, Globals.ItemHighImg, Globals.ItemRareImg }));

				//AddSubJob(new JobTakeAbrasive(_form));

				//AddSubJob(new JobPlayGoldDungeon(_form));

				//AddSubJob(new JobPlayWeekDungeon(_form));

				//AddSubJob(new JobPlayTrial(_form));

				//AddSubJob(new JobFishing(_form));

				//AddSubJob(new JobCompeteBattle(_form));

				//AddSubJob(new JobPlayRaid(_form, Globals.RLevel2));

				AddSubJob(bestJobEval.CreateJob());

				bestJobEval.Reset();
			}
		}

		public override State Process()
		{
			ActivateIfInActive();
			var subJobStatus = ProcessSubJobs();

			var endTime = DateTime.Now;
			double elapsed = (endTime - _startTime).TotalMilliseconds;

			
			if (elapsed > 600000 && _subJobs.Count > 0 && 
				_subJobs[0].Type != JobTypes.Fishing && 
				_subJobs[0].Type != JobTypes.PlayTrial)
			{
				string term = "[JobGenerator] have no action for too long time!!!! remove job : [" +
							_subJobs[0].Type + "]  time : " + endTime + "\n";
				
				Console.WriteLine(term);
				File.AppendAllText(MainForm.DebugTextFile, term);
				
				RemoveAllSubJobs();
				_form.TimeOutCnt = _form.TimeOutCnt + 1;
				_status = State.InActive;
			}
			else if (subJobStatus == State.Completed)
			{
				_status = State.InActive;
			}
			else if (subJobStatus == State.Failed)
			{
				_status = State.InActive;
			}

			return _status;
		}

		public override void Terminate() { }
	}
}