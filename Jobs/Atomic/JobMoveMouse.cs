using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SamplesCS.Jobs
{
	public class JobMoveMouse : Job
	{
		protected Point _targetPos;

		public JobMoveMouse(MainForm owner, Point targetPos)
			:
			base(JobTypes.MoveMouse, owner)
		{
			_targetPos = targetPos;
		}

		public override void Activate()
		{
			_status = State.Active;
		}

		public override State Process()
		{
			ActivateIfInActive();

			//var box = _form.CaptureBox;
			//var pos = box.PointToScreen(new Point(_targetPos.X, _targetPos.Y));

			var rect = _form.GetCurrentWndRect();

			var pos = new Point(_targetPos.X + rect.X, _targetPos.Y + rect.Y);

			Utils.MoveMouse(pos.X, pos.Y);

			return _status = State.Completed;
		}

		public override void Terminate() { }

	}
}