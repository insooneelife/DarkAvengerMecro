using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SamplesCS.Jobs
{
	public class JobMoveAndClickMouse : JobComposite
	{
		public JobMoveAndClickMouse(MainForm owner, Point targetPos)
			:
			base(JobTypes.MoveAndClickMouse, owner)
		{
			AddSubJob(new JobMoveMouse(owner, targetPos));
			AddSubJob(new JobClickMouse(owner, 100, true));
		}

		public override void Activate()
		{
			_status = State.Active;
		}

		public override State Process()
		{
			ActivateIfInActive();
			
			return _status = ProcessSubJobs();
		}

		public override void Terminate() { }
	}
}