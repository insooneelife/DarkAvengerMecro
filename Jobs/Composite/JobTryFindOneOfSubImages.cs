using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class JobTryFindOneOfSubImages : JobComposite
	{
		private string[] _subImages;
		private int _tryCnt = 3;
		private int _waitTerm = 300;

		public JobTryFindOneOfSubImages(
			MainForm form, string[] subImages, int tryCnt = 3, int waitTerm = 300)
			:
			base(JobTypes.TryFindOneOfSubImages, form)
		{
			_subImages = subImages;
			_tryCnt = tryCnt;
			_waitTerm = waitTerm;
		}

		public override void Activate()
		{
			_status = State.Active;
			AddSubJob(new JobFindOneOfSubImages(_form, _subImages));
		}

		public override State Process()
		{
			ActivateIfInActive();

			Console.WriteLine("------ Try Find One of Sub Image -----");

			var status = ProcessSubJobs();

			if (status == State.Completed)
			{
				return _status = status;
			}
			else if (status == State.Failed)
			{
				AddSubJob(new JobWaitTime(_form, _waitTerm));
				_tryCnt--;

				if (_tryCnt == 0)
				{
					return _status = State.Failed;
				}
				else
				{
					return _status = State.InActive;
				}
			}

			return _status = State.Active;
		}

		public override void Terminate() { }
	}
}
