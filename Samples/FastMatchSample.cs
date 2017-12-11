using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using SampleBase;
using OpenCvSharp.XFeatures2D;

namespace SamplesCS
{
	public class FastMatchSample
	{
		public const int FastMatchThreshold = 100;
		public static FastMatchSample instance = new FastMatchSample();

		SIFT _sift = SIFT.Create(200);
		public SIFT Sift
		{
			get { return _sift; } 
		}

		

		static private void Fast(
			Mat img, 
			int threshold,
			out Mat imgGray, 
			out KeyPoint[] keypoints)
		{
			imgGray = new Mat();
			Cv2.CvtColor(img, imgGray, ColorConversionCodes.BGR2GRAY, 0);

			keypoints = Cv2.FAST(imgGray, threshold, true);
		}

		static private void CreateORB(
			Mat imgGray,
			KeyPoint[] keypoints,
			out MatOfFloat descriptors)
		{
			descriptors = new MatOfFloat();

			ORB orb1 = ORB.Create();
			orb1.Compute(imgGray, ref keypoints, descriptors);
		}

		static public void FastDescriptor(
			Mat img, 
			out MatOfFloat descriptors, 
			out KeyPoint[] keypoints, 
			out Mat imgGray)
		{
			FastMatchSample.Fast(img, FastMatchThreshold, out imgGray, out keypoints);
			FastMatchSample.CreateORB(imgGray, keypoints, out descriptors);
		}

		static public void SiftDescriptor(
			Mat img, 
			out MatOfFloat descriptors,
			out KeyPoint[] keypoints, 
			out Mat imgGray)
		{
			imgGray = new Mat();
			Cv2.CvtColor(img, imgGray, ColorConversionCodes.BGR2GRAY);
			
			descriptors = new MatOfFloat();
			instance.Sift.DetectAndCompute(imgGray, null, out keypoints, descriptors);
		}

		static public void Match(
			MatOfFloat descriptors1,
			MatOfFloat descriptors2,
			int knnLevel,
			out DMatch[][] matches)
		{
			var bfMatcher = new BFMatcher(NormTypes.L2SQR, false);
			matches = bfMatcher.KnnMatch(descriptors1, descriptors2, knnLevel);
		}

		static public int Scoring(
			DMatch[][] matches,
			out double dissimilarity,
			out double similarity)
		{
			DateTime start = DateTime.Now;

			double maxDist = 0;
			double minDist = 1000.0f;
			int cnt = 0;

			foreach (DMatch[] subArray in matches)
			{
				foreach (DMatch i in subArray)
				{
					maxDist = Math.Max(maxDist, i.Distance);
					minDist = Math.Min(minDist, i.Distance);
					if (i.Distance < 0.02f)
					{
						cnt++;
					}
				}
			}

			List<DMatch> goodMatches = new List<DMatch>();
			double goodMatchesSum = 0.0;

			foreach (DMatch[] subArray in matches)
			{
				foreach (DMatch i in subArray)
				{
					if (i.Distance < Math.Max(2 * minDist, 0.02f))
					{
						goodMatches.Add(i);
						goodMatchesSum = i.Distance;
					}
				}
			}

			DateTime end = DateTime.Now;

			dissimilarity = goodMatchesSum / (double)goodMatches.Count;

			if (dissimilarity < 0.01f)
			{
				dissimilarity = 0.01f;
			}

			similarity = (goodMatches.Count) / dissimilarity;

			//Console.WriteLine("scoring time : " + (end - start).TotalMilliseconds + "  dissimilarity : " + dissimilarity + "  good matches : " + goodMatches.Count);

			return goodMatches.Count;
		}

	}
}

