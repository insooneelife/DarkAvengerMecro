using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using SampleBase;
using OpenCvSharp.XFeatures2D;

namespace SamplesCS.Samples
{
	public class ShiftMatchSample : ISample
	{
		public static ShiftMatchSample instance = new ShiftMatchSample();

		public const int FastMatchThreshold = 100;


		private Dictionary<string, FastCacheData> _cache = new Dictionary<string, FastCacheData>();

		public struct FastCacheData
		{
			public string keyName;
			public KeyPoint[] keypoints;
			public MatOfFloat descriptors;

			public FastCacheData(string keyName, KeyPoint[] keypoints, MatOfFloat descriptors)
			{
				this.keyName = keyName;
				this.keypoints = keypoints;
				this.descriptors = descriptors;
			}
		}

		public Dictionary<string, FastCacheData> Cache
		{
			get { return _cache; }
		}

		public void Run()
		{
			
		}

				
	}
}
