using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SamplesCS.Jobs
{
	public class JobDrag : Job
	{
		static public readonly Point DragRight = new Point(300, 0);
		static public readonly Point DragLeft = new Point(-300, 0);

		protected Point _fromPos;
		protected Point _toPos;

		protected float[] _val = new float[] 
			{
				0.05f, 0.1f, 0.15f, 0.2f, 0.25f, 
				0.3f, 0.35f, 0.4f, 0.45f, 0.5f, 
				0.55f, 0.6f, 0.65f, 0.7f, 0.75f,
				0.8f, 0.85f, 0.9f, 0.95f, 1.0f,
			};
		protected int _valIdx = 0;

		public JobDrag(
			MainForm form,
			Point fromPos,
			Point toPos)
			:
			base(JobTypes.DragMouse, form)
		{
			_fromPos = fromPos;
			_toPos = toPos;
		}

		public override void Activate()
		{
			_status = State.Active;

			var rect = _form.GetCurrentWndRect();

			Utils.MoveMouse(_fromPos.X + rect.X, _fromPos.Y + rect.Y);
			Utils.PressMouse(_form.NoxWndHandle, Utils.MouseLeftDown);
		}

		public override State Process()
		{
			ActivateIfInActive();

			DateTime endTime = DateTime.Now;
			float val = _val[_valIdx++];

			if (_valIdx == _val.Length)
			{
				Utils.PressMouse(_form.NoxWndHandle, Utils.MouseLeftUp);
				return _status = State.Completed;
			}

			var rect = _form.GetCurrentWndRect();
			
			var from = new Point(
							(int)((float)_fromPos.X * (1.0f - val)),
							(int)((float)_fromPos.Y * (1.0f - val)));

			var to = new Point(
							(int)((float)_toPos.X * (val)),
							(int)((float)_toPos.Y * (val)));

			Utils.MoveMouse(from.X + to.X + rect.X, from.Y + to.Y + rect.Y);
			//Utils.PressMouse(_form.NoxWndHandle, Utils.MouseLeftDown);

			return _status = State.Active;
		}

		public override void Terminate() { }

	}
}
