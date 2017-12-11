using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SamplesCS.Jobs
{
	public class JobClickMouse : Job
	{
		// milliseconds
		protected int _pressTime;
		protected DateTime _startTime;

		public JobClickMouse(MainForm form, int pressTime, bool noise = false)
			:
			base(JobTypes.ClickMouse, form)
		{
			int noiseTime = 0;
			if (noise)
			{
				noiseTime = form.Random.Next() % 50 - 25;
			}

			_pressTime = pressTime + noiseTime;
		}

		public override void Activate()
		{
			_status = State.Active;
			_startTime = DateTime.Now;
			
			Utils.PressMouse(_form.NoxWndHandle, Utils.MouseLeftDown);
		}

		public override State Process()
		{
			ActivateIfInActive();

			DateTime endTime = DateTime.Now;

			var elapsed = (endTime - _startTime).TotalMilliseconds;

			if (elapsed > _pressTime)
			{
				Utils.PressMouse(_form.NoxWndHandle, Utils.MouseLeftUp);
				return _status = State.Completed;
			}

			return _status = State.Active;
		}

		public override void Terminate() { }

	}
}