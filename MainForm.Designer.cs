using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenCvSharp;
using System.IO;

namespace SamplesCS
{
	partial class MainForm
	{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private int _partyClearedCnt = 0;
		public int PartyClearedCnt
		{
			get { return _partyClearedCnt; }
			set
			{
				_partyClearedCnt = value;
				PartyClearedCntLabel.Text = _partyClearedCnt.ToString();
			}
		}

		private int _raidClearedCnt = 0;
		public int RaidClearedCnt
		{
			get { return _raidClearedCnt; }
			set
			{
				_raidClearedCnt = value;
				RaidClearedCntLabel.Text = _raidClearedCnt.ToString();
			}
		}

		private int _traverseClearedCnt = 0;
		public int TraverseClearedCnt
		{
			get { return _traverseClearedCnt; }
			set
			{
				_traverseClearedCnt = value;
				TraverseClearedCntLabel.Text = _traverseClearedCnt.ToString();
			}
		}

		private int _questClearedCnt = 0;
		public int QuestClearedCnt
		{
			get { return _questClearedCnt; }
			set
			{
				_questClearedCnt = value;
				QuestClearedCntLabel.Text = _questClearedCnt.ToString();
			}
		}

		private int _mailCheckedCnt = 0;
		public int MailCheckedCnt
		{
			get { return _mailCheckedCnt; }
			set
			{
				_mailCheckedCnt = value;
				MailCheckCntLabel.Text = _mailCheckedCnt.ToString();
			}
		}

		private int _restartCompletedCnt = 0;
		public int RestartCompletedCnt
		{
			get { return _restartCompletedCnt; }
			set
			{
				_restartCompletedCnt = value;
				RestartCompletedCntLabel.Text = _restartCompletedCnt.ToString();
			}
		}

		private int _itemUpgradeCnt = 0;
		public int ItemUpgradeCnt
		{
			get { return _itemUpgradeCnt; }
			set
			{
				_itemUpgradeCnt = value;
				ItemUpgradeCntLabel.Text = _itemUpgradeCnt.ToString();
			}
		}

		private int _fishingCompletedCnt = 0;
		public int FishingCompletedCnt
		{
			get { return _fishingCompletedCnt; }
			set
			{
				_fishingCompletedCnt = value;
				FishingCompletedCntLabel.Text = _fishingCompletedCnt.ToString();
			}
		}

		private int _trialCompletedCnt = 0;
		public int TrialCompletedCnt
		{
			get { return _trialCompletedCnt; }
			set
			{
				_trialCompletedCnt = value;
				TrialCompletedLabel.Text = _trialCompletedCnt.ToString();
			}
		}

		private int _competeCompletedCnt = 0;
		public int CompeteCompletedCnt
		{
			get { return _competeCompletedCnt; }
			set
			{
				_competeCompletedCnt = value;
				CompeteCompletedLabel.Text = _competeCompletedCnt.ToString();
			}
		}


		private int _partyFailedCnt = 0;
		public int PartyFailedCnt
		{
			get { return _partyFailedCnt; }
			set
			{
				_partyFailedCnt = value;
				PartyFailedCntLabel.Text = _partyFailedCnt.ToString();
			}
		}

		private int _raidFailedCnt = 0;
		public int RaidFailedCnt
		{
			get { return _raidFailedCnt; }
			set
			{
				_raidFailedCnt = value;
				RaidFailedCntLabel.Text = _raidFailedCnt.ToString();
			}
		}

		private int _traverseFailedCnt = 0;
		public int TraverseFailedCnt
		{
			get { return _traverseFailedCnt; }
			set
			{
				_traverseFailedCnt = value;
				TraverseFailedCntLabel.Text = _traverseFailedCnt.ToString();
			}
		}

		private int _questFailedCnt = 0;
		public int QuestFailedCnt
		{
			get { return _questFailedCnt; }
			set
			{
				_questFailedCnt = value;
				QuestFailedCntLabel.Text = _questFailedCnt.ToString();
			}
		}

		private int _mailCheckFailedCnt = 0;
		public int MailCheckFailedCnt
		{
			get { return _mailCheckFailedCnt; }
			set
			{
				_mailCheckFailedCnt = value;
				MailCheckFailCntLabel.Text = _mailCheckFailedCnt.ToString();
			}
		}

		private int _restartFailedCnt = 0;
		public int RestartFailedCnt
		{
			get { return _restartFailedCnt; }
			set
			{
				_restartFailedCnt = value;
				RestartFailedCntLabel.Text = _restartFailedCnt.ToString();
			}
		}

		private int _itemUpgradeFailedCnt = 0;
		public int ItemUpgradeFailedCnt
		{
			get { return _itemUpgradeFailedCnt; }
			set
			{
				_itemUpgradeFailedCnt = value;
				ItemUpgradeFailCntLabel.Text = _itemUpgradeFailedCnt.ToString();
			}
		}

		private int _fishingFailedCnt = 0;
		public int FishingFailedCnt
		{
			get { return _fishingFailedCnt; }
			set
			{
				_fishingFailedCnt = value;
				FishingFailedCntLabel.Text = _fishingFailedCnt.ToString();
			}
		}

		private int _trialFailedCnt = 0;
		public int TrialFailedCnt
		{
			get { return _trialFailedCnt; }
			set
			{
				_trialFailedCnt = value;
				TrialFailedLabel.Text = _trialFailedCnt.ToString();
			}
		}

		private int _competeFailedCnt = 0;
		public int CompeteFailedCnt
		{
			get { return _competeFailedCnt; }
			set
			{
				_competeFailedCnt = value;
				CompeteFailedLabel.Text = _competeFailedCnt.ToString();
			}
		}

		private int _timeOutCnt = 0;
		public int TimeOutCnt
		{
			get { return _timeOutCnt; }
			set
			{
				_timeOutCnt = value;
				TimeOutCountLabel.Text = _timeOutCnt.ToString();
			}
		}

		private Dictionary<int, string> _partyLevelIdMap = new Dictionary<int, string>()
		{ 
			{ 1, Globals.PLevel1 }, { 2, Globals.PLevel2 }, { 3, Globals.PLevel3 }, 
			{ 4, Globals.PLevel4 }, { 5, Globals.PLevel5 }, { 6, Globals.PLevel6 }
		};

		private int _partyLevel = 6;
		public int PartyLevel
		{
			get { return _partyLevel; }
			set
			{
				if (value > 6)
				{
					_partyLevel = 6;
				}
				else if (value < 1)
				{
					_partyLevel = 1;
				}
				else
				{
					_partyLevel = value;
				}
			}
		}
		public string PartyLevelId
		{
			get { return _partyLevelIdMap[_partyLevel]; }
		}

		private Dictionary<int, string> _raidLevelIdMap = new Dictionary<int, string>()
		{
			{ 1, Globals.RLevel1 }, { 2, Globals.RLevel2 }, { 3, Globals.RLevel3 },
			{ 4, Globals.RLevel4 }, { 5, Globals.RLevel5 }
		};
		private int _raidLevel = 3;
		public int RaidLevel
		{
			get { return _raidLevel; }
			set
			{
				if (value > 5)
				{
					_raidLevel = 5;
				}
				else if (value < 1)
				{
					_raidLevel = 1;
				}
				else
				{
					_raidLevel = value;
				}
			}
		}
		public string RaidLevelId
		{
			get { return _raidLevelIdMap[_raidLevel]; }
		}

		private Dictionary<int, string> _traverseLevelIdMap = new Dictionary<int, string>()
		{
			{ 1, Globals.Stage1 }, { 2, Globals.Stage2 }, { 3, Globals.Stage3 },
			{ 4, Globals.Stage4 }, { 5, Globals.Stage5 },
			{ 6, Globals.Stage6 }, { 7, Globals.Stage7 }, { 8, Globals.Stage8 },
			{ 9, Globals.Stage9 }, { 10, Globals.Stage10 }
		};
		private int _traverseLevel = 5;
		public int TraverseLevel
		{
			get { return _traverseLevel; }
			set
			{
				if (value > 10)
				{
					_traverseLevel = 10;
				}
				else if (value < 1)
				{
					_traverseLevel = 1;
				}
				else
				{
					_traverseLevel = value;
				}
			}
		}
		public string TraverseLevelId
		{
			get { return _traverseLevelIdMap[_traverseLevel]; } 
		}


		private int[] _repairPeriodSet = new int[] 
			{
				10000, 25000, 50000, 100000
			};
		private int _repairPeriod = 25000;
		public int RepairPeriod
		{
			get { return _repairPeriod; }
			set
			{
				_repairPeriod = value;
			}
		}


		public bool RenderImageProcess
		{
			get 
			{
				var flag = RenderImageProcessCheckBox.Checked;
				return flag; 
			} 
		}

		public bool FishingAvailable
		{
			get
			{
				var flag = FishingCheckBox.Checked;
				return flag;
			}
		}

		public bool SkipPartyLevelSelect
		{
			get
			{
				var flag = SkipPartyCheckBox.Checked;
				return flag;
			}
		}

		public bool SkipRaidLevelSelect
		{
			get
			{
				var flag = SkipRaidCheckBox.Checked;
				return flag;
			}
		}

		public void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 'q')
			{
				Quit();
			}
			else if (e.KeyChar == 's')
			{
				Start();
			}
		}

		private void OnClickStart(object sender, EventArgs e)
		{
			Start();
		}

		private void OnClickStop(object sender, EventArgs e)
		{
			Quit();
		}

		private void OnClickCloseEmulator(object sender, EventArgs e)
		{
			Utils.KillProcess(MainForm.VirtualMachineProcess);
		}

		private void OnLoad(object sender, EventArgs e)
		{

			PartyComboBox.SelectedText = _partyLevel.ToString();
			foreach (var i in _partyLevelIdMap)
			{
				PartyComboBox.Items.Add(i.Key);
			}

			RaidComboBox.SelectedText = _raidLevel.ToString();
			foreach (var i in _raidLevelIdMap)
			{
				RaidComboBox.Items.Add(i.Key);
			}

			TraverseComboBox.SelectedText = _traverseLevel.ToString();
			foreach (var i in _traverseLevelIdMap)
			{
				TraverseComboBox.Items.Add(i.Key);
			}


			RepairComboBox.SelectedText = _repairPeriod.ToString();
			foreach (var i in _repairPeriodSet)
			{
				RepairComboBox.Items.Add(i);
			}
		}
		
		private void OnPartyComboBoxIndexChanged(object sender, EventArgs e)
		{
			PartyLevel = (int)PartyComboBox.SelectedItem;
		}

		private void OnRaidComboBoxIndexChanged(object sender, EventArgs e)
		{
			RaidLevel = (int)RaidComboBox.SelectedItem;
		}
		
		private void OnTraverseComboBoxIndexChanged(object sender, EventArgs e)
		{
			TraverseLevel = (int)TraverseComboBox.SelectedItem;
		}

		private void OnRepairComboBoxIndexChanged(object sender, EventArgs e)
		{
			RepairPeriod = (int)RepairComboBox.SelectedItem;
		}

		private void OnCheckChanged(object sender, EventArgs e)
		{
			if (CaptureBoxCheckBox.Checked)
			{
				CreateCaptureBox();
			}
			else
			{
				KillCaptureBox();
			}
		}

		public void OnCaptureBoxClosed(object sender, FormClosedEventArgs e)
		{
			CaptureBoxCheckBox.Checked = false;
		}

		public void SetPartyEvaluateText(string value)
		{
			PartyEvaluateLabel.Text = value;
		}
		
		public void SetRaidEvaluateText(string value)
		{
			RaidEvaluateLabel.Text = value;
		}
		public void SetTraverseEvaluateText(string value)
		{
			TraverseEvaluateLabel.Text = value;
		}
		public void SetRestartEvaluateText(string value)
		{
			RestartEvaluateLabel.Text = value;
		}
		public void SetQuestEvaluateText(string value)
		{
			QuestEvaluateLabel.Text = value;
		}
		public void SetMailEvaluateText(string value)
		{
			MailEvaluateLabel.Text = value;
		}

		public void SetItemUpgradeEvaluateText(string value)
		{
			ItemUpgradeEvaluateLabel.Text = value;
		}

		public void SetFishingEvaluateText(string value)
		{
			FishingEvaluateLabel.Text = value;
		}

		public void SetTrialEvaluateText(string value)
		{
			TrialEvaluateLabel.Text = value;
		}

		public void SetCompeteEvaluateText(string value)
		{
			CompeteEvaluateLabel.Text = value;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StartBtn = new System.Windows.Forms.Button();
			this.Party = new System.Windows.Forms.Label();
			this.PartyClearedCntLabel = new System.Windows.Forms.Label();
			this.RaidClearedCntLabel = new System.Windows.Forms.Label();
			this.Raid = new System.Windows.Forms.Label();
			this.TraverseClearedCntLabel = new System.Windows.Forms.Label();
			this.Traverse = new System.Windows.Forms.Label();
			this.StopBtn = new System.Windows.Forms.Button();
			this.CaptureBoxCheckBox = new System.Windows.Forms.CheckBox();
			this.RenderImageProcessCheckBox = new System.Windows.Forms.CheckBox();
			this.PartyComboBox = new System.Windows.Forms.ComboBox();
			this.RaidComboBox = new System.Windows.Forms.ComboBox();
			this.TraverseComboBox = new System.Windows.Forms.ComboBox();
			this.TryCountLabel = new System.Windows.Forms.Label();
			this.LevelLabel = new System.Windows.Forms.Label();
			this.QuestClearedCntLabel = new System.Windows.Forms.Label();
			this.Quest = new System.Windows.Forms.Label();
			this.QuestFailedCntLabel = new System.Windows.Forms.Label();
			this.TraverseFailedCntLabel = new System.Windows.Forms.Label();
			this.RaidFailedCntLabel = new System.Windows.Forms.Label();
			this.PartyFailedCntLabel = new System.Windows.Forms.Label();
			this.RepairComboBox = new System.Windows.Forms.ComboBox();
			this.RepairLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SkipPartyCheckBox = new System.Windows.Forms.CheckBox();
			this.SkipRaidCheckBox = new System.Windows.Forms.CheckBox();
			this.Mail = new System.Windows.Forms.Label();
			this.MailCheckFailCntLabel = new System.Windows.Forms.Label();
			this.MailCheckCntLabel = new System.Windows.Forms.Label();
			this.RestartFailedCntLabel = new System.Windows.Forms.Label();
			this.RestartCompletedCntLabel = new System.Windows.Forms.Label();
			this.Restart = new System.Windows.Forms.Label();
			this.TimeOutCountLabel = new System.Windows.Forms.Label();
			this.TimeOutCount = new System.Windows.Forms.Label();
			this.RestartEvaluateLabel = new System.Windows.Forms.Label();
			this.MailEvaluateLabel = new System.Windows.Forms.Label();
			this.QuestEvaluateLabel = new System.Windows.Forms.Label();
			this.TraverseEvaluateLabel = new System.Windows.Forms.Label();
			this.RaidEvaluateLabel = new System.Windows.Forms.Label();
			this.PartyEvaluateLabel = new System.Windows.Forms.Label();
			this.ItemUpgradeEvaluateLabel = new System.Windows.Forms.Label();
			this.ItemUpgradeCntLabel = new System.Windows.Forms.Label();
			this.Item = new System.Windows.Forms.Label();
			this.ItemUpgradeFailCntLabel = new System.Windows.Forms.Label();
			this.CloseEmulator = new System.Windows.Forms.Button();
			this.FishingFailedCntLabel = new System.Windows.Forms.Label();
			this.FishingEvaluateLabel = new System.Windows.Forms.Label();
			this.FishingCompletedCntLabel = new System.Windows.Forms.Label();
			this.Fishing = new System.Windows.Forms.Label();
			this.TrialFailedLabel = new System.Windows.Forms.Label();
			this.TrialEvaluateLabel = new System.Windows.Forms.Label();
			this.TrialCompletedLabel = new System.Windows.Forms.Label();
			this.Trial = new System.Windows.Forms.Label();
			this.FishingCheckBox = new System.Windows.Forms.CheckBox();
			this.CompeteFailedLabel = new System.Windows.Forms.Label();
			this.CompeteEvaluateLabel = new System.Windows.Forms.Label();
			this.CompeteCompletedLabel = new System.Windows.Forms.Label();
			this.Compete = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// StartBtn
			// 
			this.StartBtn.Location = new System.Drawing.Point(20, 23);
			this.StartBtn.Name = "StartBtn";
			this.StartBtn.Size = new System.Drawing.Size(110, 31);
			this.StartBtn.TabIndex = 0;
			this.StartBtn.Text = "Start";
			this.StartBtn.UseVisualStyleBackColor = true;
			this.StartBtn.Click += new System.EventHandler(this.OnClickStart);
			// 
			// Party
			// 
			this.Party.AutoSize = true;
			this.Party.Location = new System.Drawing.Point(188, 37);
			this.Party.Name = "Party";
			this.Party.Size = new System.Drawing.Size(34, 12);
			this.Party.TabIndex = 1;
			this.Party.Text = "Party";
			// 
			// PartyClearedCntLabel
			// 
			this.PartyClearedCntLabel.AutoSize = true;
			this.PartyClearedCntLabel.Location = new System.Drawing.Point(237, 37);
			this.PartyClearedCntLabel.Name = "PartyClearedCntLabel";
			this.PartyClearedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.PartyClearedCntLabel.TabIndex = 2;
			this.PartyClearedCntLabel.Text = "0";
			// 
			// RaidClearedCntLabel
			// 
			this.RaidClearedCntLabel.AutoSize = true;
			this.RaidClearedCntLabel.Location = new System.Drawing.Point(237, 64);
			this.RaidClearedCntLabel.Name = "RaidClearedCntLabel";
			this.RaidClearedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.RaidClearedCntLabel.TabIndex = 4;
			this.RaidClearedCntLabel.Text = "0";
			// 
			// Raid
			// 
			this.Raid.AutoSize = true;
			this.Raid.Location = new System.Drawing.Point(192, 64);
			this.Raid.Name = "Raid";
			this.Raid.Size = new System.Drawing.Size(30, 12);
			this.Raid.TabIndex = 3;
			this.Raid.Text = "Raid";
			// 
			// TraverseClearedCntLabel
			// 
			this.TraverseClearedCntLabel.AutoSize = true;
			this.TraverseClearedCntLabel.Location = new System.Drawing.Point(237, 91);
			this.TraverseClearedCntLabel.Name = "TraverseClearedCntLabel";
			this.TraverseClearedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.TraverseClearedCntLabel.TabIndex = 6;
			this.TraverseClearedCntLabel.Text = "0";
			// 
			// Traverse
			// 
			this.Traverse.AutoSize = true;
			this.Traverse.Location = new System.Drawing.Point(167, 91);
			this.Traverse.Name = "Traverse";
			this.Traverse.Size = new System.Drawing.Size(55, 12);
			this.Traverse.TabIndex = 5;
			this.Traverse.Text = "Traverse";
			// 
			// StopBtn
			// 
			this.StopBtn.Location = new System.Drawing.Point(20, 73);
			this.StopBtn.Name = "StopBtn";
			this.StopBtn.Size = new System.Drawing.Size(110, 30);
			this.StopBtn.TabIndex = 7;
			this.StopBtn.Text = "Stop";
			this.StopBtn.UseVisualStyleBackColor = true;
			this.StopBtn.Click += new System.EventHandler(this.OnClickStop);
			// 
			// CaptureBoxCheckBox
			// 
			this.CaptureBoxCheckBox.AutoSize = true;
			this.CaptureBoxCheckBox.Location = new System.Drawing.Point(20, 178);
			this.CaptureBoxCheckBox.Name = "CaptureBoxCheckBox";
			this.CaptureBoxCheckBox.Size = new System.Drawing.Size(94, 16);
			this.CaptureBoxCheckBox.TabIndex = 11;
			this.CaptureBoxCheckBox.Text = "Capture Box";
			this.CaptureBoxCheckBox.UseVisualStyleBackColor = true;
			this.CaptureBoxCheckBox.CheckedChanged += new System.EventHandler(this.OnCheckChanged);
			// 
			// RenderImageProcessCheckBox
			// 
			this.RenderImageProcessCheckBox.AutoSize = true;
			this.RenderImageProcessCheckBox.Location = new System.Drawing.Point(20, 209);
			this.RenderImageProcessCheckBox.Name = "RenderImageProcessCheckBox";
			this.RenderImageProcessCheckBox.Size = new System.Drawing.Size(154, 16);
			this.RenderImageProcessCheckBox.TabIndex = 12;
			this.RenderImageProcessCheckBox.Text = "Render Image Process";
			this.RenderImageProcessCheckBox.UseVisualStyleBackColor = true;
			// 
			// PartyComboBox
			// 
			this.PartyComboBox.FormattingEnabled = true;
			this.PartyComboBox.ImeMode = System.Windows.Forms.ImeMode.On;
			this.PartyComboBox.Location = new System.Drawing.Point(406, 34);
			this.PartyComboBox.Name = "PartyComboBox";
			this.PartyComboBox.Size = new System.Drawing.Size(82, 20);
			this.PartyComboBox.TabIndex = 13;
			this.PartyComboBox.SelectedIndexChanged += new System.EventHandler(this.OnPartyComboBoxIndexChanged);
			// 
			// RaidComboBox
			// 
			this.RaidComboBox.FormattingEnabled = true;
			this.RaidComboBox.ImeMode = System.Windows.Forms.ImeMode.On;
			this.RaidComboBox.Location = new System.Drawing.Point(406, 60);
			this.RaidComboBox.Name = "RaidComboBox";
			this.RaidComboBox.Size = new System.Drawing.Size(82, 20);
			this.RaidComboBox.TabIndex = 14;
			this.RaidComboBox.SelectedIndexChanged += new System.EventHandler(this.OnRaidComboBoxIndexChanged);
			// 
			// TraverseComboBox
			// 
			this.TraverseComboBox.FormattingEnabled = true;
			this.TraverseComboBox.ImeMode = System.Windows.Forms.ImeMode.On;
			this.TraverseComboBox.Location = new System.Drawing.Point(406, 86);
			this.TraverseComboBox.Name = "TraverseComboBox";
			this.TraverseComboBox.Size = new System.Drawing.Size(82, 20);
			this.TraverseComboBox.TabIndex = 15;
			this.TraverseComboBox.SelectedIndexChanged += new System.EventHandler(this.OnTraverseComboBoxIndexChanged);
			// 
			// TryCountLabel
			// 
			this.TryCountLabel.AutoSize = true;
			this.TryCountLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.TryCountLabel.Location = new System.Drawing.Point(167, 14);
			this.TryCountLabel.Name = "TryCountLabel";
			this.TryCountLabel.Size = new System.Drawing.Size(205, 12);
			this.TryCountLabel.TabIndex = 16;
			this.TryCountLabel.Text = "Completed / Failed / Evaluate";
			// 
			// LevelLabel
			// 
			this.LevelLabel.AutoSize = true;
			this.LevelLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.LevelLabel.Location = new System.Drawing.Point(448, 14);
			this.LevelLabel.Name = "LevelLabel";
			this.LevelLabel.Size = new System.Drawing.Size(40, 12);
			this.LevelLabel.TabIndex = 17;
			this.LevelLabel.Text = "Level";
			// 
			// QuestClearedCntLabel
			// 
			this.QuestClearedCntLabel.AutoSize = true;
			this.QuestClearedCntLabel.Location = new System.Drawing.Point(237, 118);
			this.QuestClearedCntLabel.Name = "QuestClearedCntLabel";
			this.QuestClearedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.QuestClearedCntLabel.TabIndex = 19;
			this.QuestClearedCntLabel.Text = "0";
			// 
			// Quest
			// 
			this.Quest.AutoSize = true;
			this.Quest.Location = new System.Drawing.Point(184, 118);
			this.Quest.Name = "Quest";
			this.Quest.Size = new System.Drawing.Size(38, 12);
			this.Quest.TabIndex = 18;
			this.Quest.Text = "Quest";
			// 
			// QuestFailedCntLabel
			// 
			this.QuestFailedCntLabel.AutoSize = true;
			this.QuestFailedCntLabel.Location = new System.Drawing.Point(263, 118);
			this.QuestFailedCntLabel.Name = "QuestFailedCntLabel";
			this.QuestFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.QuestFailedCntLabel.TabIndex = 23;
			this.QuestFailedCntLabel.Text = "0";
			// 
			// TraverseFailedCntLabel
			// 
			this.TraverseFailedCntLabel.AutoSize = true;
			this.TraverseFailedCntLabel.Location = new System.Drawing.Point(263, 91);
			this.TraverseFailedCntLabel.Name = "TraverseFailedCntLabel";
			this.TraverseFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.TraverseFailedCntLabel.TabIndex = 22;
			this.TraverseFailedCntLabel.Text = "0";
			// 
			// RaidFailedCntLabel
			// 
			this.RaidFailedCntLabel.AutoSize = true;
			this.RaidFailedCntLabel.Location = new System.Drawing.Point(263, 64);
			this.RaidFailedCntLabel.Name = "RaidFailedCntLabel";
			this.RaidFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.RaidFailedCntLabel.TabIndex = 21;
			this.RaidFailedCntLabel.Text = "0";
			// 
			// PartyFailedCntLabel
			// 
			this.PartyFailedCntLabel.AutoSize = true;
			this.PartyFailedCntLabel.Location = new System.Drawing.Point(263, 37);
			this.PartyFailedCntLabel.Name = "PartyFailedCntLabel";
			this.PartyFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.PartyFailedCntLabel.TabIndex = 20;
			this.PartyFailedCntLabel.Text = "0";
			// 
			// RepairComboBox
			// 
			this.RepairComboBox.FormattingEnabled = true;
			this.RepairComboBox.ImeMode = System.Windows.Forms.ImeMode.On;
			this.RepairComboBox.Location = new System.Drawing.Point(406, 202);
			this.RepairComboBox.Name = "RepairComboBox";
			this.RepairComboBox.Size = new System.Drawing.Size(82, 20);
			this.RepairComboBox.TabIndex = 24;
			this.RepairComboBox.SelectedIndexChanged += new System.EventHandler(this.OnRepairComboBoxIndexChanged);
			// 
			// RepairLabel
			// 
			this.RepairLabel.AutoSize = true;
			this.RepairLabel.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.RepairLabel.Location = new System.Drawing.Point(441, 178);
			this.RepairLabel.Name = "RepairLabel";
			this.RepairLabel.Size = new System.Drawing.Size(47, 12);
			this.RepairLabel.TabIndex = 25;
			this.RepairLabel.Text = "Repair";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(516, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 12);
			this.label1.TabIndex = 27;
			this.label1.Text = "Skip";
			// 
			// SkipPartyCheckBox
			// 
			this.SkipPartyCheckBox.AutoSize = true;
			this.SkipPartyCheckBox.Checked = true;
			this.SkipPartyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SkipPartyCheckBox.Location = new System.Drawing.Point(518, 35);
			this.SkipPartyCheckBox.Name = "SkipPartyCheckBox";
			this.SkipPartyCheckBox.Size = new System.Drawing.Size(15, 14);
			this.SkipPartyCheckBox.TabIndex = 28;
			this.SkipPartyCheckBox.UseVisualStyleBackColor = true;
			// 
			// SkipRaidCheckBox
			// 
			this.SkipRaidCheckBox.AutoSize = true;
			this.SkipRaidCheckBox.Checked = true;
			this.SkipRaidCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.SkipRaidCheckBox.Location = new System.Drawing.Point(518, 62);
			this.SkipRaidCheckBox.Name = "SkipRaidCheckBox";
			this.SkipRaidCheckBox.Size = new System.Drawing.Size(15, 14);
			this.SkipRaidCheckBox.TabIndex = 29;
			this.SkipRaidCheckBox.UseVisualStyleBackColor = true;
			// 
			// Mail
			// 
			this.Mail.AutoSize = true;
			this.Mail.Location = new System.Drawing.Point(193, 145);
			this.Mail.Name = "Mail";
			this.Mail.Size = new System.Drawing.Size(29, 12);
			this.Mail.TabIndex = 30;
			this.Mail.Text = "Mail";
			// 
			// MailCheckFailCntLabel
			// 
			this.MailCheckFailCntLabel.AutoSize = true;
			this.MailCheckFailCntLabel.Location = new System.Drawing.Point(263, 145);
			this.MailCheckFailCntLabel.Name = "MailCheckFailCntLabel";
			this.MailCheckFailCntLabel.Size = new System.Drawing.Size(11, 12);
			this.MailCheckFailCntLabel.TabIndex = 32;
			this.MailCheckFailCntLabel.Text = "0";
			// 
			// MailCheckCntLabel
			// 
			this.MailCheckCntLabel.AutoSize = true;
			this.MailCheckCntLabel.Location = new System.Drawing.Point(237, 145);
			this.MailCheckCntLabel.Name = "MailCheckCntLabel";
			this.MailCheckCntLabel.Size = new System.Drawing.Size(11, 12);
			this.MailCheckCntLabel.TabIndex = 31;
			this.MailCheckCntLabel.Text = "0";
			// 
			// RestartFailedCntLabel
			// 
			this.RestartFailedCntLabel.AutoSize = true;
			this.RestartFailedCntLabel.Location = new System.Drawing.Point(263, 172);
			this.RestartFailedCntLabel.Name = "RestartFailedCntLabel";
			this.RestartFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.RestartFailedCntLabel.TabIndex = 35;
			this.RestartFailedCntLabel.Text = "0";
			// 
			// RestartCompletedCntLabel
			// 
			this.RestartCompletedCntLabel.AutoSize = true;
			this.RestartCompletedCntLabel.Location = new System.Drawing.Point(237, 172);
			this.RestartCompletedCntLabel.Name = "RestartCompletedCntLabel";
			this.RestartCompletedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.RestartCompletedCntLabel.TabIndex = 34;
			this.RestartCompletedCntLabel.Text = "0";
			// 
			// Restart
			// 
			this.Restart.AutoSize = true;
			this.Restart.Location = new System.Drawing.Point(178, 172);
			this.Restart.Name = "Restart";
			this.Restart.Size = new System.Drawing.Size(44, 12);
			this.Restart.TabIndex = 33;
			this.Restart.Text = "Restart";
			// 
			// TimeOutCountLabel
			// 
			this.TimeOutCountLabel.AutoSize = true;
			this.TimeOutCountLabel.Location = new System.Drawing.Point(119, 275);
			this.TimeOutCountLabel.Name = "TimeOutCountLabel";
			this.TimeOutCountLabel.Size = new System.Drawing.Size(11, 12);
			this.TimeOutCountLabel.TabIndex = 37;
			this.TimeOutCountLabel.Text = "0";
			// 
			// TimeOutCount
			// 
			this.TimeOutCount.AutoSize = true;
			this.TimeOutCount.Location = new System.Drawing.Point(18, 275);
			this.TimeOutCount.Name = "TimeOutCount";
			this.TimeOutCount.Size = new System.Drawing.Size(86, 12);
			this.TimeOutCount.TabIndex = 36;
			this.TimeOutCount.Text = "TimeOutCount";
			// 
			// RestartEvaluateLabel
			// 
			this.RestartEvaluateLabel.AutoSize = true;
			this.RestartEvaluateLabel.Location = new System.Drawing.Point(288, 172);
			this.RestartEvaluateLabel.Name = "RestartEvaluateLabel";
			this.RestartEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.RestartEvaluateLabel.TabIndex = 43;
			this.RestartEvaluateLabel.Text = "0";
			// 
			// MailEvaluateLabel
			// 
			this.MailEvaluateLabel.AutoSize = true;
			this.MailEvaluateLabel.Location = new System.Drawing.Point(288, 145);
			this.MailEvaluateLabel.Name = "MailEvaluateLabel";
			this.MailEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.MailEvaluateLabel.TabIndex = 42;
			this.MailEvaluateLabel.Text = "0";
			// 
			// QuestEvaluateLabel
			// 
			this.QuestEvaluateLabel.AutoSize = true;
			this.QuestEvaluateLabel.Location = new System.Drawing.Point(288, 118);
			this.QuestEvaluateLabel.Name = "QuestEvaluateLabel";
			this.QuestEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.QuestEvaluateLabel.TabIndex = 41;
			this.QuestEvaluateLabel.Text = "0";
			// 
			// TraverseEvaluateLabel
			// 
			this.TraverseEvaluateLabel.AutoSize = true;
			this.TraverseEvaluateLabel.Location = new System.Drawing.Point(288, 91);
			this.TraverseEvaluateLabel.Name = "TraverseEvaluateLabel";
			this.TraverseEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.TraverseEvaluateLabel.TabIndex = 40;
			this.TraverseEvaluateLabel.Text = "0";
			// 
			// RaidEvaluateLabel
			// 
			this.RaidEvaluateLabel.AutoSize = true;
			this.RaidEvaluateLabel.Location = new System.Drawing.Point(288, 64);
			this.RaidEvaluateLabel.Name = "RaidEvaluateLabel";
			this.RaidEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.RaidEvaluateLabel.TabIndex = 39;
			this.RaidEvaluateLabel.Text = "0";
			// 
			// PartyEvaluateLabel
			// 
			this.PartyEvaluateLabel.AutoSize = true;
			this.PartyEvaluateLabel.Location = new System.Drawing.Point(288, 37);
			this.PartyEvaluateLabel.Name = "PartyEvaluateLabel";
			this.PartyEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.PartyEvaluateLabel.TabIndex = 38;
			this.PartyEvaluateLabel.Text = "0";
			// 
			// ItemUpgradeEvaluateLabel
			// 
			this.ItemUpgradeEvaluateLabel.AutoSize = true;
			this.ItemUpgradeEvaluateLabel.Location = new System.Drawing.Point(288, 199);
			this.ItemUpgradeEvaluateLabel.Name = "ItemUpgradeEvaluateLabel";
			this.ItemUpgradeEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.ItemUpgradeEvaluateLabel.TabIndex = 47;
			this.ItemUpgradeEvaluateLabel.Text = "0";
			// 
			// ItemUpgradeCntLabel
			// 
			this.ItemUpgradeCntLabel.AutoSize = true;
			this.ItemUpgradeCntLabel.Location = new System.Drawing.Point(237, 199);
			this.ItemUpgradeCntLabel.Name = "ItemUpgradeCntLabel";
			this.ItemUpgradeCntLabel.Size = new System.Drawing.Size(11, 12);
			this.ItemUpgradeCntLabel.TabIndex = 45;
			this.ItemUpgradeCntLabel.Text = "0";
			// 
			// Item
			// 
			this.Item.AutoSize = true;
			this.Item.Location = new System.Drawing.Point(193, 199);
			this.Item.Name = "Item";
			this.Item.Size = new System.Drawing.Size(29, 12);
			this.Item.TabIndex = 44;
			this.Item.Text = "Item";
			// 
			// ItemUpgradeFailCntLabel
			// 
			this.ItemUpgradeFailCntLabel.AutoSize = true;
			this.ItemUpgradeFailCntLabel.Location = new System.Drawing.Point(263, 199);
			this.ItemUpgradeFailCntLabel.Name = "ItemUpgradeFailCntLabel";
			this.ItemUpgradeFailCntLabel.Size = new System.Drawing.Size(11, 12);
			this.ItemUpgradeFailCntLabel.TabIndex = 48;
			this.ItemUpgradeFailCntLabel.Text = "0";
			// 
			// CloseEmulator
			// 
			this.CloseEmulator.Location = new System.Drawing.Point(20, 123);
			this.CloseEmulator.Name = "CloseEmulator";
			this.CloseEmulator.Size = new System.Drawing.Size(110, 34);
			this.CloseEmulator.TabIndex = 49;
			this.CloseEmulator.Text = "CloseEmulator";
			this.CloseEmulator.UseVisualStyleBackColor = true;
			this.CloseEmulator.Click += new System.EventHandler(this.OnClickCloseEmulator);
			// 
			// FishingFailedCntLabel
			// 
			this.FishingFailedCntLabel.AutoSize = true;
			this.FishingFailedCntLabel.Location = new System.Drawing.Point(263, 226);
			this.FishingFailedCntLabel.Name = "FishingFailedCntLabel";
			this.FishingFailedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.FishingFailedCntLabel.TabIndex = 53;
			this.FishingFailedCntLabel.Text = "0";
			// 
			// FishingEvaluateLabel
			// 
			this.FishingEvaluateLabel.AutoSize = true;
			this.FishingEvaluateLabel.Location = new System.Drawing.Point(288, 226);
			this.FishingEvaluateLabel.Name = "FishingEvaluateLabel";
			this.FishingEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.FishingEvaluateLabel.TabIndex = 52;
			this.FishingEvaluateLabel.Text = "0";
			// 
			// FishingCompletedCntLabel
			// 
			this.FishingCompletedCntLabel.AutoSize = true;
			this.FishingCompletedCntLabel.Location = new System.Drawing.Point(237, 226);
			this.FishingCompletedCntLabel.Name = "FishingCompletedCntLabel";
			this.FishingCompletedCntLabel.Size = new System.Drawing.Size(11, 12);
			this.FishingCompletedCntLabel.TabIndex = 51;
			this.FishingCompletedCntLabel.Text = "0";
			// 
			// Fishing
			// 
			this.Fishing.AutoSize = true;
			this.Fishing.Location = new System.Drawing.Point(178, 226);
			this.Fishing.Name = "Fishing";
			this.Fishing.Size = new System.Drawing.Size(46, 12);
			this.Fishing.TabIndex = 50;
			this.Fishing.Text = "Fishing";
			// 
			// TrialFailedLabel
			// 
			this.TrialFailedLabel.AutoSize = true;
			this.TrialFailedLabel.Location = new System.Drawing.Point(263, 253);
			this.TrialFailedLabel.Name = "TrialFailedLabel";
			this.TrialFailedLabel.Size = new System.Drawing.Size(11, 12);
			this.TrialFailedLabel.TabIndex = 57;
			this.TrialFailedLabel.Text = "0";
			// 
			// TrialEvaluateLabel
			// 
			this.TrialEvaluateLabel.AutoSize = true;
			this.TrialEvaluateLabel.Location = new System.Drawing.Point(288, 253);
			this.TrialEvaluateLabel.Name = "TrialEvaluateLabel";
			this.TrialEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.TrialEvaluateLabel.TabIndex = 56;
			this.TrialEvaluateLabel.Text = "0";
			// 
			// TrialCompletedLabel
			// 
			this.TrialCompletedLabel.AutoSize = true;
			this.TrialCompletedLabel.Location = new System.Drawing.Point(237, 253);
			this.TrialCompletedLabel.Name = "TrialCompletedLabel";
			this.TrialCompletedLabel.Size = new System.Drawing.Size(11, 12);
			this.TrialCompletedLabel.TabIndex = 55;
			this.TrialCompletedLabel.Text = "0";
			// 
			// Trial
			// 
			this.Trial.AutoSize = true;
			this.Trial.Location = new System.Drawing.Point(192, 253);
			this.Trial.Name = "Trial";
			this.Trial.Size = new System.Drawing.Size(30, 12);
			this.Trial.TabIndex = 54;
			this.Trial.Text = "Trial";
			// 
			// FishingCheckBox
			// 
			this.FishingCheckBox.AutoSize = true;
			this.FishingCheckBox.Location = new System.Drawing.Point(20, 240);
			this.FishingCheckBox.Name = "FishingCheckBox";
			this.FishingCheckBox.Size = new System.Drawing.Size(65, 16);
			this.FishingCheckBox.TabIndex = 58;
			this.FishingCheckBox.Text = "Fishing";
			this.FishingCheckBox.UseVisualStyleBackColor = true;
			// 
			// CompeteFailedLabel
			// 
			this.CompeteFailedLabel.AutoSize = true;
			this.CompeteFailedLabel.Location = new System.Drawing.Point(263, 280);
			this.CompeteFailedLabel.Name = "CompeteFailedLabel";
			this.CompeteFailedLabel.Size = new System.Drawing.Size(11, 12);
			this.CompeteFailedLabel.TabIndex = 62;
			this.CompeteFailedLabel.Text = "0";
			// 
			// CompeteEvaluateLabel
			// 
			this.CompeteEvaluateLabel.AutoSize = true;
			this.CompeteEvaluateLabel.Location = new System.Drawing.Point(288, 280);
			this.CompeteEvaluateLabel.Name = "CompeteEvaluateLabel";
			this.CompeteEvaluateLabel.Size = new System.Drawing.Size(11, 12);
			this.CompeteEvaluateLabel.TabIndex = 61;
			this.CompeteEvaluateLabel.Text = "0";
			// 
			// CompeteCompletedLabel
			// 
			this.CompeteCompletedLabel.AutoSize = true;
			this.CompeteCompletedLabel.Location = new System.Drawing.Point(237, 280);
			this.CompeteCompletedLabel.Name = "CompeteCompletedLabel";
			this.CompeteCompletedLabel.Size = new System.Drawing.Size(11, 12);
			this.CompeteCompletedLabel.TabIndex = 60;
			this.CompeteCompletedLabel.Text = "0";
			// 
			// Compete
			// 
			this.Compete.AutoSize = true;
			this.Compete.Location = new System.Drawing.Point(166, 280);
			this.Compete.Name = "Compete";
			this.Compete.Size = new System.Drawing.Size(56, 12);
			this.Compete.TabIndex = 59;
			this.Compete.Text = "Compete";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 332);
			this.Controls.Add(this.CompeteFailedLabel);
			this.Controls.Add(this.CompeteEvaluateLabel);
			this.Controls.Add(this.CompeteCompletedLabel);
			this.Controls.Add(this.Compete);
			this.Controls.Add(this.FishingCheckBox);
			this.Controls.Add(this.TrialFailedLabel);
			this.Controls.Add(this.TrialEvaluateLabel);
			this.Controls.Add(this.TrialCompletedLabel);
			this.Controls.Add(this.Trial);
			this.Controls.Add(this.FishingFailedCntLabel);
			this.Controls.Add(this.FishingEvaluateLabel);
			this.Controls.Add(this.FishingCompletedCntLabel);
			this.Controls.Add(this.Fishing);
			this.Controls.Add(this.CloseEmulator);
			this.Controls.Add(this.ItemUpgradeFailCntLabel);
			this.Controls.Add(this.ItemUpgradeEvaluateLabel);
			this.Controls.Add(this.ItemUpgradeCntLabel);
			this.Controls.Add(this.Item);
			this.Controls.Add(this.RestartEvaluateLabel);
			this.Controls.Add(this.MailEvaluateLabel);
			this.Controls.Add(this.QuestEvaluateLabel);
			this.Controls.Add(this.TraverseEvaluateLabel);
			this.Controls.Add(this.RaidEvaluateLabel);
			this.Controls.Add(this.PartyEvaluateLabel);
			this.Controls.Add(this.TimeOutCountLabel);
			this.Controls.Add(this.TimeOutCount);
			this.Controls.Add(this.RestartFailedCntLabel);
			this.Controls.Add(this.RestartCompletedCntLabel);
			this.Controls.Add(this.Restart);
			this.Controls.Add(this.MailCheckFailCntLabel);
			this.Controls.Add(this.MailCheckCntLabel);
			this.Controls.Add(this.Mail);
			this.Controls.Add(this.SkipRaidCheckBox);
			this.Controls.Add(this.SkipPartyCheckBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.RepairLabel);
			this.Controls.Add(this.RepairComboBox);
			this.Controls.Add(this.QuestFailedCntLabel);
			this.Controls.Add(this.TraverseFailedCntLabel);
			this.Controls.Add(this.RaidFailedCntLabel);
			this.Controls.Add(this.PartyFailedCntLabel);
			this.Controls.Add(this.QuestClearedCntLabel);
			this.Controls.Add(this.Quest);
			this.Controls.Add(this.LevelLabel);
			this.Controls.Add(this.TryCountLabel);
			this.Controls.Add(this.TraverseComboBox);
			this.Controls.Add(this.RaidComboBox);
			this.Controls.Add(this.PartyComboBox);
			this.Controls.Add(this.RenderImageProcessCheckBox);
			this.Controls.Add(this.CaptureBoxCheckBox);
			this.Controls.Add(this.StopBtn);
			this.Controls.Add(this.TraverseClearedCntLabel);
			this.Controls.Add(this.Traverse);
			this.Controls.Add(this.RaidClearedCntLabel);
			this.Controls.Add(this.Raid);
			this.Controls.Add(this.PartyClearedCntLabel);
			this.Controls.Add(this.Party);
			this.Controls.Add(this.StartBtn);
			this.ImeMode = System.Windows.Forms.ImeMode.On;
			this.Name = "MainForm";
			this.Text = "MainForm2";
			this.Load += new System.EventHandler(this.OnLoad);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button StartBtn;
		private System.Windows.Forms.Label Party;
		private System.Windows.Forms.Label PartyClearedCntLabel;
		private System.Windows.Forms.Label RaidClearedCntLabel;
		private System.Windows.Forms.Label Raid;
		private System.Windows.Forms.Label TraverseClearedCntLabel;
		private System.Windows.Forms.Label Traverse;
		private System.Windows.Forms.Button StopBtn;
		private System.Windows.Forms.CheckBox CaptureBoxCheckBox;
		private CheckBox RenderImageProcessCheckBox;
		private ComboBox PartyComboBox;
		private ComboBox RaidComboBox;
		private ComboBox TraverseComboBox;
		private Label TryCountLabel;
		private Label LevelLabel;
		private Label QuestClearedCntLabel;
		private Label Quest;
		private Label QuestFailedCntLabel;
		private Label TraverseFailedCntLabel;
		private Label RaidFailedCntLabel;
		private Label PartyFailedCntLabel;
		private ComboBox RepairComboBox;
		private Label RepairLabel;
		private Label label1;
		private CheckBox SkipPartyCheckBox;
		private CheckBox SkipRaidCheckBox;
		private Label Mail;
		private Label MailCheckFailCntLabel;
		private Label MailCheckCntLabel;
		private Label RestartFailedCntLabel;
		private Label RestartCompletedCntLabel;
		private Label Restart;
		private Label TimeOutCountLabel;
		private Label TimeOutCount;
		private Label RestartEvaluateLabel;
		private Label MailEvaluateLabel;
		private Label QuestEvaluateLabel;
		private Label TraverseEvaluateLabel;
		private Label RaidEvaluateLabel;
		private Label PartyEvaluateLabel;
		private Label ItemUpgradeEvaluateLabel;
		private Label ItemUpgradeCntLabel;
		private Label Item;
		private Label ItemUpgradeFailCntLabel;
		private Button CloseEmulator;
		private Label FishingFailedCntLabel;
		private Label FishingEvaluateLabel;
		private Label FishingCompletedCntLabel;
		private Label Fishing;
		private Label TrialFailedLabel;
		private Label TrialEvaluateLabel;
		private Label TrialCompletedLabel;
		private Label Trial;
		private CheckBox FishingCheckBox;
		private Label CompeteFailedLabel;
		private Label CompeteEvaluateLabel;
		private Label CompeteCompletedLabel;
		private Label Compete;
	}
}