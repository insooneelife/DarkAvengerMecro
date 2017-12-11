using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace SamplesCS.Jobs
{
	public class JobFindSubImage : Job
	{
		private Mat _subImage = null;
		private List<Rect> _imgRects = new List<Rect>();
		private Scalar _color = Scalar.Yellow;

		public JobFindSubImage(MainForm form, string subImage)
			:
			base(JobTypes.FindSubImage, form, BlackBoard.UIRectBlackBoardID)
		{
			_form.SubImages.TryGetValue(subImage, out _subImage);
		}

		public override void Activate()
		{
			_status = State.Active;
			
			if (_subImage != null)
			{
				if (_form.PrintscreenMat != null)
				{ 
					FindSubImageSample.Run(_form.PrintscreenMat, _subImage, _imgRects, _color);
				}

			}
		}

		public override State Process()
		{
			ActivateIfInActive();

			if (_imgRects.Count == 1)
			{
				Console.WriteLine("[JobFindSubImage] Found!!!!!!");
				// write on black board for other job to share this data
				BlackBoard.Instance.Write(BlackBoardAccessID, _imgRects[0], this);

				return _status = State.Completed;
			}
			else if (_imgRects.Count > 1)
			{
				BlackBoard.Instance.Write(BlackBoardAccessID, _imgRects[0], this);

				Console.WriteLine("[JobFindSubImage] Found!! but too many sub-images in main image");
				foreach (var r in _imgRects)
				{
					Console.WriteLine(r.X + " " + r.Y + " " + r.Width + " " + r.Height);
				}
				return _status = State.Completed;
			}
			else
			{
				Console.WriteLine("[JobFindSubImage] No Image!!!!!!");
				return _status = State.Failed;
			}
		}

		public override void Terminate() { }

	}
}
