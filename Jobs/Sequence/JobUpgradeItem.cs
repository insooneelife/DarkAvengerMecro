using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobUpgradeItem : JobSequence
	{
		enum UpgradeState
		{
			Weapon, Armor, Accessory, Finished
		}
		private UpgradeState _upgradeState = UpgradeState.Weapon;
		private string[] _itemRanks = null;

		private Dictionary<UpgradeState, string> _upgradeItemOnState = new Dictionary<UpgradeState, string>();

		private bool _nothingToUpgrade = true;

		public JobUpgradeItem(MainForm form, string[] itemRanks)
			:
			base(JobTypes.UpgradeItem, form)
		{
			_itemRanks = itemRanks;  

			_upgradeItemOnState[UpgradeState.Weapon] = Globals.Weapon;
			_upgradeItemOnState[UpgradeState.Armor] = Globals.Decoration;
			_upgradeItemOnState[UpgradeState.Accessory] = Globals.Ring;
			
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{	}

		protected override void OnJobFailed()
		{
			_form.ItemUpgradeFailedCnt = _form.ItemUpgradeFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
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
					AddSubJob(new JobMovePage(_form, Globals.MenuPage, Globals.MenuToItem, Globals.ItemPage));
				});

			ActionSequenceTree fromItem =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromItem]");

					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot2, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot2, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot2, true));
					AddSubJob(new JobWaitTime(_form, 750));
				});

			ActionSequenceTree selectItem =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[selectItem]");
					if (_upgradeState != UpgradeState.Finished)
					{
						var item = _upgradeItemOnState[_upgradeState];
						AddSubJob(new JobMovePage(_form, Globals.ItemPage, item, Globals.ItemDetailPage));
					}
				});


			ActionSequenceTree fromItemDetail =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromItemDetail]");
					AddSubJob(new JobMovePage(_form, Globals.ItemDetailPage, Globals.ToUpgrade, Globals.ItemUpgradePage));
					AddSubJob(new JobWaitTime(_form, 750));
				});


			// upgrade
			ActionSequenceTree findItem =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[findItem]");
					AddSubJob(new JobTryFindOneOfSubImages(_form, _itemRanks));
				});

			ActionSequenceTree clickItem =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickItem]");
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);

					int addX = 10;
					int addY = 20;

					var pos = Utils.RectToPos(rect.X + addX, rect.Y + addY, rect.Width, rect.Height);
					
					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 500));

				});

			ActionSequenceTree upgrade =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[upgrade]");
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.StartUpgrade, true));
					AddSubJob(new JobWaitTime(_form, 500));
					AddSubJob(new JobCheckPage(_form, Globals.ItemUpgradeResultPage, 5000));
					
				});

			ActionSequenceTree checkLockPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkLockPage]");
					AddSubJob(new JobWaitTime(_form, 500));
					AddSubJob(new JobCheckPage(_form, Globals.ItemLockPage, 5000));
				});


			ActionSequenceTree exitResult =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitResult]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.ItemUpgradeResultPage,
							Globals.OutFromUpgradeResult, Globals.ItemUpgradePage));
				});


			// exit
			ActionSequenceTree exitItemUpgrade =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitItemUpgrade]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.ItemUpgradePage,
							Globals.BackInUpgradePage, Globals.ItemPage));
				});

			ActionSequenceTree checkUpgradeState =
				new ActionSequenceTree(() =>
				{
					AddSubJob(new JobCheckCondition(_form, () => { return _upgradeState == UpgradeState.Finished; }));
				});

			ActionSequenceTree exitItemPage =
				new ActionSequenceTree(() =>
				{
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot1, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot1, true));
					AddSubJob(new JobWaitTime(_form, 750));
					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.ItemSlot1, true));
					AddSubJob(new JobWaitTime(_form, 750));

					Console.WriteLine("[exitItemPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.ItemPage,
							Globals.BackInItemPage, Globals.MenuPage));
				});

			ActionSequenceTree exitMenu =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitMenu]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.MenuPage,
							Globals.BackInMenu, Globals.MainPage));
				});

			// if repair
			ActionSequenceTree exitLockPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitLockPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.ItemLockPage,
							Globals.NoInItemLockPage, Globals.ItemUpgradePage));
				});

			ActionSequenceTree exitGetVisualPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[exitGetVisualPage]");
					AddSubJob(
						new JobMovePage(
							_form,
							Globals.ItemGetVisualPage,
							Globals.ExitItemGetVisual, Globals.ItemDetailPage));
				});


			fromMain.Completed = fromMenu;
			fromMenu.Completed = fromItem;
			fromItem.Completed = selectItem;
			selectItem.Completed = fromItemDetail;
			fromItemDetail.Completed = findItem;


			// upgrade
			findItem.Completed = clickItem;
			findItem.ActionOnCompleted = () => 
			{
				_form.ItemUpgradeCnt = _form.ItemUpgradeCnt + 1; 
				_nothingToUpgrade = false; 
			};

			clickItem.Completed = upgrade;
			upgrade.Completed = exitResult;
			upgrade.Failed = checkLockPage;
			checkLockPage.Completed = exitLockPage;
			exitLockPage.Completed = exitItemUpgrade;
			checkLockPage.Failed = findItem;
			exitResult.Completed = findItem;

			findItem.Failed = exitItemUpgrade;

			exitItemUpgrade.Completed = checkUpgradeState;
			exitItemUpgrade.ActionOnCompleted = () => 
			{
				UpdateUpgradeState();
			};

			checkUpgradeState.Completed = exitItemPage;
			checkUpgradeState.Failed = selectItem;

			exitItemPage.Completed = exitMenu;
			exitMenu.ActionOnCompleted = () => 
			{
				if (_nothingToUpgrade)
				{
					_returnState = State.Failed;
				}
				else
				{ 
					_returnState = State.Completed;
				}
			};

			_currentAction = fromMain;

			// make repair map
			_repairMap[Globals.MenuPage] = fromMenu;
			_repairMap[Globals.ItemPage] = fromItem;
			_repairMap[Globals.ItemDetailPage] = fromItemDetail;
			_repairMap[Globals.ItemUpgradePage] = findItem;
			_repairMap[Globals.ItemLockPage] = exitLockPage;
			_repairMap[Globals.ItemUpgradeResultPage] = exitResult;
			_repairMap[Globals.ItemGetVisualPage] = exitGetVisualPage;
		}

		void UpdateUpgradeState()
		{
			if (_upgradeState == UpgradeState.Weapon)
			{
				_upgradeState = UpgradeState.Armor;
			}
			else if(_upgradeState == UpgradeState.Armor)
			{
				_upgradeState = UpgradeState.Accessory;
			}
			else if (_upgradeState == UpgradeState.Accessory)
			{
				_upgradeState = UpgradeState.Finished;
			}
		}
	}
}
