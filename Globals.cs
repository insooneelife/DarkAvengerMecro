using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS
{
	public static class Globals
	{
		public static readonly string ImShowMaxMatchPageName = "Max Match Page";
		public static readonly string ImShowCurrentPageName = "Current Page";

		public static readonly string ImShowMaxSlicedPageName = "Max Sliced Page";
		public static readonly string ImShowCurrentSlicedPageName = "Current Sliced Page";

		#region Pages
		public static readonly string EveryPage = "EveryPage";
		public static readonly string UnknownPage = "UnknownPage";

		public static readonly string MainPage = "MainPage";
		public static readonly string MenuPage = "MenuPage";
		public static readonly string CharacterPage = "CharacterPage";
		public static readonly string FriendPage = "FriendPage";
		public static readonly string QuestPage = "QuestPage";
		public static readonly string BonusPage = "BonusPage";
		public static readonly string ShopPage = "ShopPage";
		public static readonly string ArisPage = "ArisPage";

		public static readonly string FightPage = "FightPage";
		public static readonly string MailPage = "MailPage";
		public static readonly string OptionPage = "OptionPage";

		public static readonly string ArisAdvertisePage = "ArisAdvertisePage";
		public static readonly string OneFlusAdvertisePage = "OneFlusAdvertisePage";
		


		// cowork
		public static readonly string CoworkPage = "CoworkPage";
		public static readonly string PartyPage = "PartyPage";
		public static readonly string RaidPage = "RaidPage";

		public static readonly string PartyResultPage = "PartyResultPage";
		public static readonly string PartyRestrictedPage = "PartyRestrictedPage";
		public static readonly string PartyRepeatAwardPage = "PartyRepeatAwardPage";

		public static readonly string RaidResultPage = "RaidResultPage";
		public static readonly string RaidRestrictedPage = "RaidRestrictedPage";
		public static readonly string RaidRepeatAwardPage = "RaidRepeatAwardPage";
		
		

		// traverse
		public static readonly string TraversePage = "TraversePage";
		public static readonly string TraverseDungeonPage = "TraverseDungeonPage";
		public static readonly string TraverseDungeonBlankPage = "TraverseDungeonBlankPage";
		public static readonly string TraverseStagePage = "TraverseStagePage";
		public static readonly string TraverseResultPage = "TraverseResultPage";
		public static readonly string TraverseRestrictedPage = "TraverseRestrictedPage";
		public static readonly string TraverseDeadPage = "TraverseDeadPage";
		
		public static readonly string InfiniteDungeonPage = "InfiniteDungeonPage";
		
		// quest
		public static readonly string WeekQuestPage = "WeekQuestPage";

		// login
		public static readonly string LoginPage = "LoginPage";
		public static readonly string FacebookPage = "FacebookPage";
		public static readonly string FacebookConfirmPage = "FacebookConfirmPage";
		public static readonly string StartGamePage = "StartGamePage";
		public static readonly string CharacterSelectPage = "CharacterSelectPage";

		// in game
		public static readonly string InGameStopPage = "InGameStopPage";

		// out game 
		public static readonly string EmulatorPage = "EmulatorPage";
		public static readonly string NotifyPage = "NotifyPage";

		// network
		public static readonly string ServerConnectFailPage = "ServerConnectFailPage";

		// login fail
		public static readonly string LoginFailPage = "LoginFailPage";
		
		// event
		public static readonly string EventPage1 = "EventPage1";
		public static readonly string EventPage2 = "EventPage2";
		public static readonly string EventPage3 = "EventPage3";

		// item
		public static readonly string ItemPage = "ItemPage";
		public static readonly string ItemDetailPage = "ItemDetailPage";
		public static readonly string ItemUpgradePage = "ItemUpgradePage";
		public static readonly string ItemUpgradeResultPage = "ItemUpgradeResultPage";
		public static readonly string ItemLockPage = "ItemLockPage";
		public static readonly string ItemGetVisualPage = "ItemGetVisualPage";

		// forge
		public static readonly string ForgePage = "ForgePage";
		public static readonly string ForgeAbrasivePage = "ForgeAbrasivePage";
		public static readonly string ForgeAbrasiveGemPage = "ForgeAbrasiveGemPage";
		public static readonly string ForgeAbrasiveRestrictedPage = "ForgeAbrasiveRestrictedPage";

		// gold
		public static readonly string GoldDungeonPage = "GoldDungeonPage";
		public static readonly string GoldDungeonResultPage = "GoldDungeonResultPage";

		// week
		public static readonly string WeekDungeonPage = "WeekDungeonPage";
		public static readonly string WeekDungeonResultPage = "WeekDungeonResultPage";

		// trial
		public static readonly string TrialPage = "TrialPage";
		public static readonly string TrialResultPage = "TrialResultPage";
		public static readonly string TrialSweepAwayPage = "TrialSweepAwayPage";
		public static readonly string TrialMansFightPage = "TrialMansFightPage";
		public static readonly string TrialWomansFightPage = "TrialWomansFightPage";
		public static readonly string TrialTwoDevilPage = "TrialTwoDevilPage";
		//public static readonly string TrialBattleGodPage = "TrialBattleGodPage";
		public static readonly string TrialStormEyePage = "TrialStormEyePage";
		public static readonly string TrialBloodyPage = "TrialBloodyPage";
		public static readonly string TrialEventPage = "TrialEventPage";

		// guild
		public static readonly string GuildPage = "GuildPage";
		public static readonly string FishingPage = "FishingPage";
		public static readonly string FishingInPage = "FishingInPage";
		public static readonly string FishingFinishPage = "FishingFinishPage";

		// compete battle
		public static readonly string CompeteBattlePage = "CompeteBattlePage";
		public static readonly string CompeteBattleRewardPage = "CompeteBattleRewardPage";
		public static readonly string CompeteBattleRewardRankingPage = "CompeteBattleRewardRankingPage";
		public static readonly string CompeteBattleResultPage = "CompeteBattleResultPage";
		public static readonly string CompeteBattleRestrictedPage = "CompeteBattleRestrictedPage";

		// personal battle
		public static readonly string PersonalBattlePage = "PersonalBattlePage";

		#endregion Pages


		#region Click UI
		// main
		public static readonly string Menu = "Menu";
		public static readonly string Trial = "Trial";
		public static readonly string Cowork = "Cowork";
		public static readonly string Fight = "Fight";
		public static readonly string Traverse = "Traverse";
		public static readonly string Notify = "Notify";
		public static readonly string Mail = "Mail";

		public static readonly string DontSee = "DontSee";
		public static readonly string DontSeeInOneFlus = "DontSeeInOneFlus";
		public static readonly string ExitOneFlusAdvertise = "ExitOneFlusAdvertise";
		

		// cowork
		public static readonly string OutFromRepeatAward = "OutFromRepeatAward";
		
		// party
		public static readonly string Party = "Party";
		public static readonly string PartyAward = "PartyAward";
		public static readonly string StartParty = "StartParty";
		public static readonly string ToMainFromPartyResult = "ToMainFromPartyResult";
		public static readonly string ToPartyFromPartyResult = "ToPartyFromPartyResult";
		public static readonly string RetryParty = "RetryParty";
		public static readonly string PLevel1 = "PLevel1";
		public static readonly string PLevel2 = "PLevel2";
		public static readonly string PLevel3 = "PLevel3";
		public static readonly string PLevel4 = "PLevel4";
		public static readonly string PLevel5 = "PLevel5";
		public static readonly string PLevel6 = "PLevel6";
		public static readonly string YesInPartyRestrictedPage = "YesInPartyRestrictedPage";
		public static readonly string NoInPartyRestrictedPage = "NoInPartyRestrictedPage";
		public static readonly string BackInParty = "BackInParty";
		
		// raid
		public static readonly string Raid = "Raid";
		public static readonly string RaidAward = "RaidAward";
		public static readonly string StartRaid = "StartRaid";
		public static readonly string ToMainFromRaidResult = "ToMainFromRaidResult";
		public static readonly string ToRaidFromRaidResult = "ToRaidFromRaidResult";
		public static readonly string RetryRaid = "RetryRaid";

		public static readonly string RLevel1 = "RLevel1";
		public static readonly string RLevel2 = "RLevel2";
		public static readonly string RLevel3 = "RLevel3";
		public static readonly string RLevel4 = "RLevel4";
		public static readonly string RLevel5 = "RLevel5";
		public static readonly string YesInRaidRestrictedPage = "YesInRaidRestrictedPage";
		public static readonly string NoInRaidRestrictedPage = "NoInRaidRestrictedPage";
		public static readonly string BackInRaid = "BackInRaid";
		public static readonly string MoveBoss = "MoveBoss";
		public static readonly string Grovise = "Grovise";
		public static readonly string Caligo = "Caligo";
		public static readonly string Demian = "Demian";
		public static readonly string Legina = "Legina";
		

		public static readonly string RaidDragLeftPos = "RaidDragLeftPos";
		public static readonly string RaidDragRightPos = "RaidDragRightPos";

		// traverse
		public static readonly string TraverseDungeon = "TraverseDungeon";
		
		
		public static readonly string InfiniteDungeon = "InfiniteDungeon";

		public static readonly string LeftInTraverse = "LeftInTraverse";
		public static readonly string RightInTraverse = "RightInTraverse";
		public static readonly string BackInTraverseDungeon = "BackInTraverseDungeon";
		public static readonly string Stage1 = "Stage1";
		public static readonly string Stage2 = "Stage2";
		public static readonly string Stage3 = "Stage3";
		public static readonly string Stage4 = "Stage4";
		public static readonly string Stage5 = "Stage5";
		public static readonly string Stage6 = "Stage6";
		public static readonly string Stage7 = "Stage7";
		public static readonly string Stage8 = "Stage8";
		public static readonly string Stage9 = "Stage9";
		public static readonly string Stage10 = "Stage10";

		public static string YesInTraverseRestrictedPage = "YesInTraverseRestrictedPage";
		public static string NoInTraverseRestrictedPage = "NoInTraverseRestrictedPage";

		public static readonly string RepeatTraverse = "RepeatTraverse";
		public static readonly string SequenceTraverse = "SequenceTraverse";
		public static readonly string StartTraverse = "StartTraverse";
		public static readonly string BackInTraverseStage = "BackInTraverseStage";

		public static readonly string ToMainFromTraverseResultPage = "ToMainFromTraverseResultPage";
		public static readonly string RetryInTraverseResultPage = "RetryInTraverseResultPage";
		public static readonly string NextInTraverseResultPage = "NextInTraverseResultPage";
		
		public static readonly string NextStage = "NextStage";
		public static readonly string OutFromTraverseDeadPage = "OutFromTraverseDeadPage";

		// menu
		public static readonly string MenuToCharacter = "MenuToCharacter";
		public static readonly string MenuToItem = "MenuToItem";
		public static readonly string MenuToForge = "MenuToForge";
		public static readonly string MenuToQuest = "MenuToQuest";
		public static readonly string MenuToBonus = "MenuToBonus";
		public static readonly string MenuToAris = "MenuToAris";
		public static readonly string MenuToWeekQuest = "MenuToWeekQuest";
		public static readonly string MenuToDayQuest = "MenuToDayQuest";
		public static readonly string MenuToGuild = "MenuToGuild";
		public static readonly string BackInMenu = "BackInMenu";

		// quest
		public static readonly string MissionCompleted = "MissionCompleted";
		public static readonly string GetMission = "GetMission";
		public static readonly string BackInWeekQuest = "BackInWeekQuest";

		// login
		public static readonly string Google = "Google";
		public static readonly string Facebook = "Facebook";
		public static readonly string Guest = "Guest";

		public static readonly string EmailInFacebook = "EmailInFacebook";
		public static readonly string PasswordInFacebook = "PasswordInFacebook";
		public static readonly string LoginInFacebook = "LoginInFacebook";

		public static readonly string ClickOnStartGamePage = "ClickOnStartGamePage";

		public static readonly string SelectKenneth = "SelectKenneth";
		public static readonly string SelectBella = "SelectBella";
		public static readonly string SelectHector = "SelectHector";
		public static readonly string StartGameInSelectPage = "StartGameInSelectPage";

		public static readonly string YesInFacebook = "YesInFacebook";
		public static readonly string NoInFacebook = "NoInFacebook";

		// mail
		public static readonly string OutFromMailPage = "OutFromMailPage";
		public static readonly string RecvAllInMailPage = "RecvAllInMailPage";

		// in game
		public static readonly string KeepGoingInStopPage = "KeepGoingInStopPage";
		public static readonly string OutFromInGamePage = "OutFromInGamePage";

		// out game
		public static readonly string CloseNotify = "CloseNotify";
		public static readonly string StartDarkAvenger3 = "StartDarkAvenger3";

		// network
		public static readonly string CloseServerConnectFail = "CloseServerConnectFail";

		// login fail
		public static readonly string OkInLoginFailPage = "OkInLoginFailPage";

		// event
		public static readonly string OutFromEventPage1 = "OutFromEventPage1";
		public static readonly string OutFromEventPage2 = "OutFromEventPage2";
		public static readonly string OutFromEventPage3 = "OutFromEventPage3";

		// item
		public static readonly string Weapon = "Weapon";
		public static readonly string Armor = "Armor";
		public static readonly string Necklase = "Necklase";
		public static readonly string Earring = "Earring";
		public static readonly string Decoration = "Decoration";
		public static readonly string Helmet = "Helmet";
		public static readonly string Cape = "Cape";
		public static readonly string Ring = "Ring";
		public static readonly string Charm = "Charm";
		public static readonly string Belt = "Belt";
		public static readonly string WeaponTab = "WeaponTab";
		public static readonly string ArmorTab = "ArmorTab";
		public static readonly string AccessoryTab = "AccessoryTab";
		public static readonly string BackInItemPage = "BackInItemPage";
		public static readonly string ItemSlot1 = "ItemSlot1";
		public static readonly string ItemSlot2 = "ItemSlot2";
		public static readonly string ItemSlot3 = "ItemSlot3";
		public static readonly string ToUpgrade = "ToUpgrade";
		public static readonly string OutFromItemDetailPage = "OutFromItemDetailPage";
		public static readonly string StartUpgrade = "StartUpgrade";
		public static readonly string BackInUpgradePage = "BackInUpgradePage";
		public static readonly string OutFromUpgradeResult = "OutFromUpgradeResult";
		public static readonly string NoInItemLockPage = "NoInItemLockPage";
		public static readonly string ExitItemGetVisual = "ExitItemGetVisual";
		

		// forge
		public static readonly string ToAbrasive = "ToAbrasive";
		public static readonly string BackInForge = "BackInForge";
		public static readonly string TakeAbrasive = "TakeAbrasive";
		public static readonly string BackInForgeAbrasive = "BackInForgeAbrasive";
		public static readonly string NoInForgeAbrasiveGemPage = "NoInForgeAbrasiveGemPage";
		public static readonly string NoInForgeAbrasiveRestrictedPage = "NoInForgeAbrasiveRestrictedPage";

		// gold
		public static readonly string GoldDungeon = "GoldDungeon";
		public static readonly string GLevel5 = "GLevel5";
		public static readonly string ToMainFromGoldResult = "ToMainFromGoldResult";
		public static readonly string ToGoldFromGoldResult = "ToGoldFromGoldResult";
		public static readonly string BackInGoldDungeon = "BackInGoldDungeon";
		public static readonly string GoldDragLeftPos = "GoldDragLeftPos";
		public static readonly string GoldDragRightPos = "GoldDragRightPos";

		// week
		public static readonly string WeekDungeon = "WeekDungeon";
		public static readonly string WLevel5 = "WLevel5";
		public static readonly string ToMainFromWeekResult = "ToMainFromWeekResult";
		public static readonly string ToWeekFromWeekResult = "ToWeekFromWeekResult";
		public static readonly string BackInWeekDungeon = "BackInWeekDungeon";
		public static readonly string WeekDragLeftPos = "WeekDragLeftPos";
		public static readonly string WeekDragRightPos = "WeekDragRightPos";
		

		// option
		public static readonly string OutFromOption = "OutFromOption";

		// trial
		public static readonly string BackInTrial = "BackInTrial";
		public static readonly string TrialDragLeft = "TrialDragLeft";
		public static readonly string TrialDragRight = "TrialDragRight";

		public static readonly string BackInTrialLevelPages = "BackInTrialLevelPages";
		public static readonly string TrialStartLevelFirstFromRight = "TrialStartLevelFirstFromRight";
		public static readonly string TrialStartLevelSecondFromRight = "TrialStartLevelSecondFromRight";
		public static readonly string TrialStartLevelThirdFromRight = "TrialStartLevelThirdFromRight";
		public static readonly string TrialStartLevelFourthFromRight = "TrialStartLevelFourthFromRight";
		public static readonly string TrialStartLevel1 = "TrialStartLevel1";
		public static readonly string TrialStartLevel2 = "TrialStartLevel2";
		public static readonly string TrialStartLevel3 = "TrialStartLevel3";

		public static readonly string ToMainFromTrialResult = "ToMainFromTrialResult";
		public static readonly string ToTrialFromTrialResult = "ToTrialFromTrialResult";

		// guild
		public static readonly string ToMainPageFromGuild = "ToMainPageFromGuild";
		public static readonly string Joystick = "Joystick";
		public static readonly string ToMainPageFromFishing = "ToMainPageFromFishing";
		public static readonly string GoToFishing = "GoToFishing";
		public static readonly string StartFishing = "StartFishing";
		public static readonly string StopFishing = "StopFishing";
		public static readonly string GetList = "GetList";
		public static readonly string DefaultBait = "DefaultBait";
		public static readonly string NormalBait = "NormalBait";
		public static readonly string HighBait = "HighBait";

		public static readonly string YesInFishingFinishPage = "YesInFishingFinishPage";
		public static readonly string NoInFishingFinishPage = "NoInFishingFinishPage";

		// compete battle
		public static readonly string ToCompeteBattle = "ToCompeteBattle";
		public static readonly string ToPersonalBattle = "ToPersonalBattle";

		public static readonly string ToMainFromCompeteBattle = "ToMainFromCompeteBattle";
		public static readonly string StartCompeteBattle1 = "StartCompeteBattle1";
		public static readonly string StartCompeteBattle2 = "StartCompeteBattle2";
		public static readonly string StartCompeteBattle3 = "StartCompeteBattle3";
		public static readonly string RewardCompeteBattle = "RewardCompeteBattle";
		public static readonly string ToCompeteBattleFromReward = "ToCompeteBattleFromReward";

		public static readonly string ToMainFromCompeteBattleResult = "ToMainFromCompeteBattleResult";
		public static readonly string ToCompeteBattleFromResult = "ToCompeteBattleFromResult";
		public static readonly string NoInCompeteBattleRestrictedPage = "NoInCompeteBattleRestrictedPage";
		public static readonly string RewardRankingCompeteBattle = "RewardRankingCompeteBattle";

		public static readonly string ToCompeteBattleFromRewardRanking = "ToCompeteBattleFromRewardRanking";

		#endregion Click UI


		#region SubImages
		public static readonly string MoveImmediatelyImg = "MoveImmediatelyImg";
		public static readonly string MissionCompletedImg = "MissionCompletedImg";
		public static readonly string GetMissionImg = "GetMissionImg";
		public static readonly string RaidChangedImageImg = "RaidChangedImageImg";
		public static readonly string QuestUISetImg = "QuestUISetImg";
		public static readonly string DarkAvengerIconImg = "DarkAvengerIconImg";
		public static readonly string ItemHighImg = "ItemHighImg";
		public static readonly string ItemRareImg = "ItemRareImg";
		public static readonly string ItemBasicImg = "ItemBasicImg";

		public static readonly string AbrasiveHighImg = "AbrasiveHighImg";
		public static readonly string AbrasiveRareImg = "AbrasiveRareImg";
		public static readonly string AbrasiveHeroImg = "AbrasiveHeroImg";

		public static readonly string MakeAbrasiveImg = "MakeAbrasiveImg";
		public static readonly string AbrasiveImmediateMakeImg = "AbrasiveImmediateMakeImg";
		public static readonly string TakeAbrasiveImg = "TakeAbrasiveImg";

		public static readonly string TrialBattleGodImg = "TrialBattleGodImg";
		public static readonly string TrialBloodyImg = "TrialBloodyImg";
		public static readonly string TrialMansFightImg = "TrialMansFightImg";
		public static readonly string TrialStormEyeImg = "TrialStormEyeImg";
		public static readonly string TrialSweepAwayImg = "TrialSweepAwayImg";
		public static readonly string TrialTwoDevilImg = "TrialTwoDevilImg";
		public static readonly string TrialWomansFightImg = "TrialWomansFightImg";
		public static readonly string TrialEventImg = "TrialEventImg";
		public static readonly string StartFishingImg = "StartFishingImg";
		public static readonly string LoginPhoneImg = "LoginPhoneImg";
		public static readonly string LoginPasswordImg = "LoginPasswordImg";
		public static readonly string LoginButtonImg = "LoginButtonImg";
		

		#endregion SubImages
	}
}
