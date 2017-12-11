using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{

	public class JobLogin : JobSequence
	{
		// facebook, google ..
		private string _signInWith;
		private string _email;
		private string _password;

		public JobLogin(MainForm form, string signInWith, string email, string password)
			:
			base(JobTypes.Login, form)
		{
			_signInWith = signInWith;
			_email = email;
			_password = password;
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
			ActionSequenceTree fromLogin =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromLogin]");
					// #fix - to page
					AddSubJob(new JobMovePage(_form, Globals.LoginPage, _signInWith, Globals.FacebookPage));

					if (_signInWith == Globals.Facebook)
					{ 
						AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.FacebookPage }));
					}
					else if (_signInWith == Globals.Google)
					{
						//AddSubJob(new JobCheckCurrentPage(_form, new string[] { Globals.GooglePage }));
					}
					else if (_signInWith == Globals.Guest)
					{
						//AddSubJob(new JobCheckCurrentPage(_form, new string[] { Globals.FacebookPage }));
					}
				});

			ActionSequenceTree fromFacebookLogin =
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
				});

			ActionSequenceTree fromFacebookConfirm =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[fromFacebookConfirm]");
					AddSubJob(new JobMovePage(_form, Globals.FacebookConfirmPage, Globals.YesInFacebook, Globals.StartGamePage));

					AddSubJob(new JobCheckUntilMeetPage(_form, new string[] { Globals.StartGamePage }));
				});


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
					AddSubJob(new JobMovePage(_form, Globals.CharacterSelectPage, Globals.StartGameInSelectPage, Globals.NotifyPage));
				});

			ActionSequenceTree closeNotifyPage =
				new ActionSequenceTree(() =>
				{
					Console.WriteLine("[closeNotifyPage]");
					AddSubJob(new JobMovePage(_form, Globals.NotifyPage, Globals.CloseNotify, Globals.MainPage));
				});

			fromLogin.Completed = fromFacebookLogin;
			fromFacebookLogin.Completed = fromFacebookConfirm;
			fromFacebookConfirm.Completed = fromStartGame;
			fromStartGame.Completed = fromCharacterSelect;
			fromCharacterSelect.Completed = closeNotifyPage;

			_currentAction = fromLogin;

			// make repair map
			_repairMap[Globals.LoginPage] = fromLogin;
			_repairMap[Globals.FacebookPage] = fromFacebookLogin;
			_repairMap[Globals.FacebookConfirmPage] = fromFacebookConfirm;
			_repairMap[Globals.StartGamePage] = fromStartGame;
			_repairMap[Globals.CharacterSelectPage] = fromCharacterSelect;
		}
	}
}
