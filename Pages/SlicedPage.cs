using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SamplesCS
{
	public class SlicedPage
	{
		private string _pageName;
		private Scalar _color = Scalar.White;
		
		private int _fromX = 852;
		private int _fromY = 572;
		private int _width = 88;
		private int _height = 84;

		private Mat _currentMat = null;
		private Mat _currentGray = null;
		private KeyPoint[] _currentKeypoints = null;

		private Mat _imageMat = null;
		private Mat _imageGray = null;
		private MatOfFloat _pageDescriptors = null;
		private KeyPoint[] _keypoints = null;


		public string PageName
		{
			get { return _pageName; }
		}

		public Scalar Color
		{
			get { return _color; }
			set { _color = value; }
		}


		// entry => ../SlicedPages
		public SlicedPage(string entry, string pageName)
		{
			_pageName = pageName;
			
			LoadMainImages(entry);
			LoadData(entry);
		}

		#region Public Functions

		public void RenderKeyPoints()
		{
			Utils.RenderKeyPoints(Globals.ImShowMaxSlicedPageName, _imageMat, _keypoints);

			Utils.RenderKeyPoints(Globals.ImShowCurrentSlicedPageName, _currentMat, _currentKeypoints);
		}

		public int PageScore(
			int wndStartX,
			int wndStartY,
			out double dissimilarity,
			out double similarity,
			out string log,
			bool logging = true)
		{
			log = "";
			if (_pageDescriptors == null)
			{
				dissimilarity = 2000000000.0f;
				similarity = 0.0f;
				return 0;
			}

			Bitmap printscreen;
			Utils.CaptureScreen(wndStartX + _fromX, wndStartY + _fromY, _width, _height, out printscreen);

			_currentMat = Utils.BitmapToMat(printscreen);
			MatOfFloat discriptors = null;
			
			FastMatchSample.FastDescriptor(
				_currentMat, out discriptors, out _currentKeypoints, out _currentGray);

			DMatch[][] matches;
			FastMatchSample.Match(discriptors, _pageDescriptors, 3, out matches);

			int score = FastMatchSample.Scoring(matches, out dissimilarity, out similarity);

			if (logging)
			{
				log += PageName + " score : " + score + "  dissimilarity : " + dissimilarity + " keypoints : " + _currentKeypoints.Length + "\n";
			}

			return score;
		}
		
		

		#endregion Public Functions


		#region Private Functions

		// load image for fast algorithm
		private void LoadMainImages(string entry)
		{
			var pageImageEntries = Directory.GetFiles(entry);

			Console.WriteLine("-------------------------- " + _pageName + " -----------------------------");

			foreach (var e in pageImageEntries)
			{
				string fileName = Utils.GetFileNameInFullEntry(e);
				_imageMat = new Mat(e);

				Console.WriteLine("Load file : " + e);
				Console.WriteLine("Matrix : " + _imageMat);
			}

			if (_imageMat != null)
			{
				FastMatchSample.FastDescriptor(_imageMat, out _pageDescriptors, out _keypoints, out _imageGray);
				//FastMatchSample.SiftDescriptor(_imageMat, out _pageDescriptors, out _keypoints, out _imageGray);
			}

			Console.WriteLine("-------------------------------------------------------------------------");
		}


		// load ui data and related page data
		private void LoadData(string entry)
		{
			string UITextDataEntry = entry + "/Data/Rect.txt";

			// read ui rectangles
			System.IO.StreamReader dataFile = new System.IO.StreamReader(UITextDataEntry);
			string line;

			if ((line = dataFile.ReadLine()) != null)
			{
				string[] stringSeparators = new string[] { " " };
				string[] result;
				result = line.Split(stringSeparators, StringSplitOptions.None);

				_fromX = Int32.Parse(result[0]);
				_fromY = Int32.Parse(result[1]);
				_width = Int32.Parse(result[2]);
				_height = Int32.Parse(result[3]);
			}

			dataFile.Close();
			
		}
		
		#endregion Private Functions
	}
}
