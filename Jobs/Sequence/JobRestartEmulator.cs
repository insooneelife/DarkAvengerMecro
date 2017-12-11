using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobRestartEmulator : JobSequence
	{
	
		private string _email = "insooneelife@nate.com";
		private string _password = "dlstns4877";

		public JobRestartEmulator(MainForm form)
			:
			base(JobTypes.RestartEmulator, form, form.RepairPeriod + 10000)
		{
			_startingPage = Globals.EveryPage;
			MakeActionSequenceTree();
		}

		protected override void OnJobCompleted()
		{
			_form.RestartCompletedCnt = _form.RestartCompletedCnt + 1;
		}

		protected override void OnJobFailed()
		{
			_form.RestartFailedCnt = _form.RestartFailedCnt + 1;
		}

		protected override void MakeActionSequenceTree()
		{
			ActionSequenceTree checkEmulatorPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[checkEmulatorPage]");
					AddSubJob(new JobCheckPage(_form, Globals.EmulatorPage));
				});

			ActionSequenceTree killProcess =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[killProcess]");

					AddSubJob(new JobKillProcess(_form, MainForm.VirtualMachineProcess));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobCheckCondition(
					_form,
					() => {
						MainForm.HookStates hookState;
						_form.HookProcessHandle(MainForm.VirtualMachineProcess, out hookState);
						return (hookState == MainForm.HookStates.NoProcess);
					}));
				});
				

			ActionSequenceTree restartEmulator =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[restartEmulator]");
					AddSubJob(new JobExecuteProcess(_form, MainForm.VirtualMachineProcess));
				});

				ActionSequenceTree fromEmulator =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromEmulator]");
					
					AddSubJob(new JobWaitTime(_form, 3000));

					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.EmulatorPage }));

					AddSubJob(new JobMovePage(_form, Globals.EmulatorPage, Globals.StartDarkAvenger3, Globals.StartGamePage));
					
				});
				
			// Login
			ActionSequenceTree fromLogin =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromLogin]");

					AddSubJob(new JobMovePage(_form, Globals.LoginPage, Globals.Facebook, Globals.FacebookPage));

					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.FacebookPage }));
					
				});

			/*ActionSequenceTree fromFacebookLogin =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromFacebookLogin]");

					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.EmailInFacebook, true));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobPressKeyboard(_form, _email));
					AddSubJob(new JobWaitTime(_form, 500));

					AddSubJob(JobGenerator.CreateJobMoveAndClickMouse(_form, Globals.PasswordInFacebook, true));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobPressKeyboard(_form, _password));
					AddSubJob(new JobWaitTime(_form, 500));

					AddSubJob(new JobMovePage(_form, Globals.FacebookPage, Globals.LoginInFacebook, Globals.FacebookConfirmPage, 2000, 2000));

					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.FacebookConfirmPage }));
				});*/


			ActionSequenceTree findLoginEmail =
			new ActionSequenceTree(() =>
			{
				Console.WriteLine("[findLoginEmail]");
				AddSubJob(new JobTryFindSubImage(_form, Globals.LoginPhoneImg));
			});

			ActionSequenceTree clickLoginEmail =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickLoginEmail]");
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);

					int addX = 0;
					int addY = 0;

					var pos = Utils.RectToPos(rect.X + addX, rect.Y + addY, rect.Width, rect.Height);

					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobPressKeyboard(_form, _email));
					AddSubJob(new JobWaitTime(_form, 500));
				});

			ActionSequenceTree findLoginPassword =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[findLoginPassword]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.LoginPasswordImg));
				});

			ActionSequenceTree clickLoginPassword =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickLoginEmail]");
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);

					int addX = 0;
					int addY = 0;

					var pos = Utils.RectToPos(rect.X + addX, rect.Y + addY, rect.Width, rect.Height);

					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 1000));

					AddSubJob(new JobPressKeyboard(_form, _password));
					AddSubJob(new JobWaitTime(_form, 500));
				});

			ActionSequenceTree findLoginButton =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[findLoginButton]");
					AddSubJob(new JobTryFindSubImage(_form, Globals.LoginButtonImg));
				});

			ActionSequenceTree clickLoginButton =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[clickLoginButton]");
					OpenCvSharp.Rect rect = (OpenCvSharp.Rect)BlackBoard.Instance.Read(BlackBoard.UIRectBlackBoardID, this);

					int addX = 0;
					int addY = 0;

					var pos = Utils.RectToPos(rect.X + addX, rect.Y + addY, rect.Width, rect.Height);

					AddSubJob(new JobWaitTime(_form, 500));
					AddSubJob(new JobMoveAndClickMouse(_form, pos));
					AddSubJob(new JobWaitTime(_form, 3000));
					
					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.FacebookConfirmPage }));
				});

			ActionSequenceTree fromFacebookConfirm =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromFacebookConfirm]");
					AddSubJob(new JobMovePage(_form, Globals.FacebookConfirmPage, Globals.YesInFacebook, Globals.StartGamePage));

					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.StartGamePage }));
				});


			// Login end





			ActionSequenceTree fromStartGame =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromStartGame]");
					AddSubJob(new JobMovePage(_form, Globals.StartGamePage, Globals.ClickOnStartGamePage, Globals.CharacterSelectPage));
				});

			ActionSequenceTree fromCharacterSelect =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromCharacterSelect]");
					AddSubJob(new JobMovePage(_form, Globals.CharacterSelectPage, Globals.StartGameInSelectPage, new string[] { Globals.NotifyPage, Globals.EventPage1, Globals.EventPage2, Globals.MainPage } ));
				});

			ActionSequenceTree closeEventPage1 =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[closeEventPage1]");
					AddSubJob(new JobMovePage(_form, Globals.EventPage1, Globals.OutFromEventPage1, new string[] { Globals.EventPage2, Globals.EventPage3, Globals.NotifyPage, Globals.MainPage }));
				});

			ActionSequenceTree closeEventPage2 =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[closeEventPage2]");
					AddSubJob(new JobMovePage(_form, Globals.EventPage2, Globals.OutFromEventPage2, new string[] { Globals.EventPage3, Globals.NotifyPage, Globals.MainPage }));
				});

			ActionSequenceTree closeEventPage3 =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[closeEventPage3]");
					AddSubJob(new JobMovePage(_form, Globals.EventPage3, Globals.OutFromEventPage3, new string[] { Globals.NotifyPage, Globals.MainPage }));
				});

			ActionSequenceTree closeNotifyPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[closeNotifyPage]");
					AddSubJob(new JobMovePage(_form, Globals.NotifyPage, Globals.CloseNotify, Globals.MainPage));
				});

			

			checkEmulatorPage.Completed = fromEmulator;
			checkEmulatorPage.Failed = killProcess;

			killProcess.Completed = restartEmulator;
			killProcess.Failed = killProcess;

			restartEmulator.Completed = fromEmulator;
			fromEmulator.Completed = fromStartGame;
			
			fromStartGame.Completed = fromCharacterSelect;
			fromCharacterSelect.Completed = closeEventPage1;
			closeEventPage1.Completed = closeEventPage2;
			closeEventPage2.Completed = closeEventPage3;
			closeEventPage3.Completed = closeNotifyPage;

			closeNotifyPage.ActionOnCompleted = () => { _returnState = State.Completed; };

			// will be executed only after repair
			fromLogin.Completed = findLoginEmail;
			findLoginEmail.Completed = clickLoginEmail;
			findLoginEmail.Failed = null;

			clickLoginEmail.Completed = findLoginPassword;
			findLoginPassword.Completed = clickLoginPassword;
			findLoginPassword.Failed = null;

			clickLoginPassword.Completed = findLoginButton;
			findLoginButton.Completed = clickLoginButton;
			findLoginButton.Failed = null;

			clickLoginButton.Completed = fromFacebookConfirm;
			fromFacebookConfirm.Completed = fromStartGame;

			_currentAction = checkEmulatorPage;

			// make repair map
			_repairMap[Globals.EmulatorPage] = fromEmulator;

			_repairMap[Globals.LoginPage] = fromLogin;
			_repairMap[Globals.FacebookPage] = findLoginEmail;
			_repairMap[Globals.FacebookConfirmPage] = fromFacebookConfirm;

			_repairMap[Globals.StartGamePage] = fromStartGame;
			_repairMap[Globals.CharacterSelectPage] = fromCharacterSelect;

			_repairMap[Globals.EventPage1] = closeEventPage1;
			_repairMap[Globals.EventPage2] = closeEventPage2;
			_repairMap[Globals.EventPage3] = closeEventPage3;

			_repairMap[Globals.NotifyPage] = closeNotifyPage;
		}

		
	}
}
