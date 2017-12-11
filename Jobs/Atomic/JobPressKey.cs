using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobPressKeyboard : Job
	{
		protected string _text;

		public JobPressKeyboard(MainForm form, string text)
			:
			base(JobTypes.ClickMouse, form)
		{
			_text = text;
		}

		public override void Activate()
		{
			_status = State.Active;
			Utils.SendKeyboard(_text);
		}

		public override State Process()
		{
			ActivateIfInActive();
			return _status = State.Completed;
		}

		public override void Terminate() { }

	}
}
