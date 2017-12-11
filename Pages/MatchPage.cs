using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.IO;
using System.Windows.Forms;

// data
// checkHard, skipPageScore

namespace SamplesCS
{
	public class MatchPage
	{
		#region Variables

		private string _pageName;
		private Scalar _color = Scalar.White;
		private Dictionary<string, Rect> _uiRects = new Dictionary<string, Rect>();
		private List<string> _relatedPages = new List<string>();
		
		private Mat _imageMat = null;
		private MatOfFloat _pageDescriptors = null;
		private KeyPoint[] _keypoints = null;

		private Dictionary<string, Mat> _additionalImgMats = new Dictionary<string, Mat>();
		private bool _checkHard = true;
		private bool _skipPageScore = false;

		private Dictionary<string, Action> _specialCmdByTxtFileNameMap = new Dictionary<string, Action>();

		#endregion Variables

		#region Properties

		public string PageName
		{
			get { return _pageName; }
		}

		public Scalar Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public Dictionary<string, Rect> UIRects
		{
			get { return _uiRects; }
		}

		public List<string> RelatedPages
		{
			get { return _relatedPages; }
		}
		

		#endregion Properties

		// entry => ../MatchPages
		public MatchPage(string entry, string pageName)
		{
			_pageName = pageName;

			Console.WriteLine("------------------------------- " + _pageName + " -------------------------------");
			LoadSpecialCommands(entry);
			LoadMainImages(entry);
			LoadAdditionalImages(entry);
			LoadData(entry);

			string temp = new string('-', _pageName.Length);


			Console.WriteLine("--------------------------------" + temp + "--------------------------------\n");

			if (_additionalImgMats.Count > 0 && _imageMat == null)
			{
				_skipPageScore = true;
			}
		}

		#region Public Functions

		public void RenderKeyPoints()
		{
			if (_imageMat != null)
			{ 
				Utils.RenderKeyPoints(Globals.ImShowMaxMatchPageName, _imageMat, _keypoints);
			}
		}

		public int PageScore(
			MatOfFloat currentDescriptors,
			out double dissimilarity,
			out double similarity,
			out string log,
			bool logging = true)
		{
			log = "";

			if (_skipPageScore)
			{
				similarity = 10000.0f;
				dissimilarity = 0.0f;
				return 50000;
			}

			if (_pageDescriptors == null)
			{
				dissimilarity = 2000000000.0f;
				similarity = 0.0f;
				return 0;
			}
			
			

			DMatch[][] matches;
			FastMatchSample.Match(currentDescriptors, _pageDescriptors, 3, out matches);
				
			int score = FastMatchSample.Scoring(matches, out dissimilarity, out similarity);

			if (logging)
			{ 
				log += PageName + " score : " + score + "  dissimilarity : " + dissimilarity + "\n";
			}

			return score;
		}

		public void FindSubImage(Mat curPageMat)
		{
			foreach (var m in _additionalImgMats)
			{
				List<Rect> outRects = new List<Rect>();
				FindSubImageSample.Run(curPageMat, m.Value, outRects, _color);

				foreach (var r in outRects)
				{
					Console.WriteLine(m.Key + "  " + r);
				}
			}
		}

		

		public bool AdditionalCheck(Mat curPageMat)
		{
			if (_checkHard)
			{
				return CheckPageHard(curPageMat);
			}
			else
			{
				return CheckPageWeak(curPageMat);
			}
		}

		public double AdditionalCalcSimilarity(Mat curPageMat, double similarity)
		{
			if (_checkHard)
			{
				if (CheckPageHard(curPageMat))
				{
					return similarity;
				}
				else return -500.0f;
			}
			else
			{
				if (CheckPageWeak(curPageMat))
				{
					return similarity;
				}
				else return -500.0f;
			}
		}

		#endregion Public Functions


		#region Private Functions

		// load image for fast algorithm
		private void LoadMainImages(string entry)
		{
			var pageImageEntries = Directory.GetFiles(entry);


			Console.WriteLine("(Main Image)");
			foreach (var e in pageImageEntries)
			{
				string fileName = Utils.GetFileNameInFullEntry(e);

				if (Utils.CheckIsPNG(e))
				{
					_imageMat = new Mat(e);

					Console.WriteLine("Load file : " + e);
					Console.WriteLine("Matrix : " + _imageMat);
				}
			}

			if (_imageMat != null)
			{
				Mat imageGray = null;
				FastMatchSample.FastDescriptor(_imageMat, out _pageDescriptors, out _keypoints, out imageGray);
			}
			Console.WriteLine();

		}

		// load images for template match algorithm
		private void LoadAdditionalImages(string entry)
		{
			var pageAdditionalImageEntries = Directory.GetFiles(entry + "/Additional");

			Console.WriteLine("(Additional Images)");
			foreach (var e in pageAdditionalImageEntries)
			{
				string fileName = Utils.GetFileNameInFullEntry(e);
				var mat = new Mat(e);

				if (!_additionalImgMats.ContainsKey(fileName))
				{
					_additionalImgMats.Add(fileName, mat);
				}

				Console.WriteLine("Load file : " + e);
				Console.WriteLine("Matrix : " + mat);
			}
			Console.WriteLine();
		}

		// load ui data and related page data
		private void LoadData(string entry)
		{
			Console.WriteLine("(Load data)");
			string UITextDataEntry = entry + "/Data/UI.txt";
			string RelatedPagesTextDataEntry = entry + "/Data/RelatedPages.txt";

			// read ui rectangles
			System.IO.StreamReader uiRectFile = new System.IO.StreamReader(UITextDataEntry);
			string line;

			while ((line = uiRectFile.ReadLine()) != null)
			{
				string[] stringSeparators = new string[] { " " };
				string[] result;
				result = line.Split(stringSeparators, StringSplitOptions.None);

				string uiName = result[0];
				int x = Int32.Parse(result[1]);
				int y = Int32.Parse(result[2]);
				int width = Int32.Parse(result[3]);
				int height = Int32.Parse(result[4]);

				Rect rect = new Rect(x, y, width, height);
				if (!_uiRects.ContainsKey(uiName))
				{
					Console.WriteLine(uiName +"  "+ rect);
					_uiRects.Add(uiName, rect);
				}
			}

			uiRectFile.Close();


			// read related pages
			System.IO.StreamReader relatedFile = new System.IO.StreamReader(RelatedPagesTextDataEntry);

			_relatedPages.Add(_pageName);
			while ((line = relatedFile.ReadLine()) != null)
			{
				if (!_relatedPages.Exists(x => x == line))
				{
					_relatedPages.Add(line);
				}
			}

			Console.WriteLine();

			relatedFile.Close();
		}

		public void LoadSpecialCommands(string entry)
		{
			_specialCmdByTxtFileNameMap["CheckHard"] = () => { _checkHard = false; Console.WriteLine("CheckHard  " + _checkHard); };


			var files = Directory.GetFiles(entry);

			Console.WriteLine("(Special Commands)");

			foreach (var e in files)
			{
				string fileName = Utils.GetFileNameInFullEntry(e);

				if (Utils.CheckIsTXT(e))
				{
					//Console.WriteLine(fileName);

					Action action;
					if (_specialCmdByTxtFileNameMap.TryGetValue(fileName, out action))
					{
						if (action != null)
						{
							action();
						}
					}
				}
			}
			Console.WriteLine();
		}

		private bool CheckPageWeak(Mat curPageMat)
		{
			if (_additionalImgMats.Count == 0)
				return true;

			List<Rect> outRects = new List<Rect>();
			foreach (var m in _additionalImgMats)
			{
				if (FindSubImageSample.Run(curPageMat, m.Value, outRects))
				{
					return true;
				}
			}

			return false;
		}

		private bool CheckPageHard(Mat curPageMat)
		{
			if (_additionalImgMats.Count == 0)
				return true;

			List<Rect> outRects = new List<Rect>();
			foreach (var m in _additionalImgMats)
			{
				if (!FindSubImageSample.Run(curPageMat, m.Value, outRects, _color))
				{
					return false;
				}
			}
			
			return true;
		}
		#endregion Private Functions
	}
}

