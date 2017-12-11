using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;
using SampleBase;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using SamplesCS.Jobs;

namespace SamplesCS
{
	public partial class MainForm : Form
	{
		#region Variables
		public static readonly string MacroDirectory = "Data/DarkAvengerImage/";
		public static readonly string MatchPagesDirectory = "Data/DarkAvengerImage/MatchPages/";
		public static readonly string SubImagesDirectory = "Data/DarkAvengerImage/SubImages/";
		public static readonly string SlicedPagesDirectory = "Data/DarkAvengerImage/SlicedPages/";
		public static readonly string VirtualMachineProcess = "Nox";
		public static readonly string DebugTextFile = "DebugText.txt";

		//public static readonly string VirtualMachineProcess = "wmplayer";

		private Jobs.JobGenerator _jobGenerator = null;
		private Dictionary<string, MatchPage> _matchPages = new Dictionary<string, MatchPage>();
		private Dictionary<string, Rect> _allUIRects = new Dictionary<string, Rect>();
		private Dictionary<string, Mat> _subImages = new Dictionary<string, Mat>();

		private Dictionary<string, SlicedPage> _slicedPages = new Dictionary<string, SlicedPage>();

		private string _currentPageName;
		private string _prevPageName;

		private KeyPoint[] _keypoints = null;

		private CaptureBox _captureBox;
		private Bitmap _printscreen;
		private IntPtr _noxWndHandle;
		private System.Windows.Forms.Timer _checkPageTimer;
		private System.Windows.Forms.Timer _updateTimer;

		private Random _random = new Random();		
		private bool _log = true;
		#endregion Variables


		#region Properties
		public string CurrentPageName
		{
			get { return _currentPageName; } 
		}

		public Dictionary<string, Rect> AllUIRects
		{
			get { return _allUIRects; }
		}

		public Random Random
		{
			get { return _random; } 
		}

		public Dictionary<string, Mat> SubImages
		{
			get { return _subImages; } 
		}

		public Bitmap Printscreen
		{
			get { return _printscreen; }
		}

		public Mat PrintscreenMat
		{
			get 
			{
				if (_printscreen == null)
				{
					return null;
				}
				return Utils.BitmapToMat(_printscreen); 
			}
		}
		
		public IntPtr NoxWndHandle
		{
			get { return _noxWndHandle; }
		}

		public KeyPoint[] KeyPoints
		{
			get { return _keypoints; }
		}

		#endregion Properties

		
		public MainForm()
		{
			InitializeComponent();
			LoadMatchPages();
			LoadSubImages();
			InitTimers();

			HookStates hookState;
			HookProcessHandle(VirtualMachineProcess, out hookState);

			_currentPageName = Globals.UnknownPage;
			_prevPageName = Globals.UnknownPage;
			_jobGenerator = new Jobs.JobGenerator(this);
		}

		private void InitTimers()
		{
			_checkPageTimer = new System.Windows.Forms.Timer();
			_checkPageTimer.Tick += new EventHandler(OnCheckPage);

			// in miliseconds
			_checkPageTimer.Interval = 500; 
			_checkPageTimer.Stop();
			
			_updateTimer = new System.Windows.Forms.Timer();
			_updateTimer.Tick += new EventHandler(OnUpdate);
			_updateTimer.Interval = 20; 
			_updateTimer.Stop();
		}
		
		private void LoadMatchPages()
		{
			string[] directoryEntries = Directory.GetDirectories(MatchPagesDirectory);

			foreach (var f in directoryEntries)
			{
				string directoryName = Utils.GetFileNameInFullEntry(f, true);
				Console.WriteLine(directoryName + " " + f);

				string pageName = directoryName;

				var matchPage = new MatchPage(f, pageName);
				_matchPages.Add(pageName, matchPage);

				
				foreach (var p in matchPage.UIRects)
				{
					_allUIRects[p.Key] = p.Value;
				}
			}


			directoryEntries = Directory.GetDirectories(SlicedPagesDirectory);

			foreach (var f in directoryEntries)
			{
				string directoryName = Utils.GetFileNameInFullEntry(f, true);
				Console.WriteLine(directoryName + " " + f);

				string pageName = directoryName;

				var matchPage = new SlicedPage(f, pageName);
				_slicedPages.Add(pageName, matchPage);
			}
		}

		private void LoadSubImages()
		{
			string [] fileEntries = Directory.GetFiles(SubImagesDirectory);

			Console.WriteLine("Load SubImages..");
			foreach (var entry in fileEntries)
			{
				var fileName = Utils.GetFileNameInFullEntry(entry);
				var mat = new Mat(entry);
				_subImages[fileName] = mat;

				Console.WriteLine(fileName + " " + mat); 
			}
		}

		

		public enum HookStates
		{
			WindowMinimized, Successed, Failed, NoProcess
		}

		

		public void HookProcessHandle(string processName, out HookStates hookState)
		{
			var p = Utils.GetProcessByName(processName);
			if (p != null)
			{
				_noxWndHandle = p.MainWindowHandle;

				if (_noxWndHandle != null)
				{
					Utils.Rect rect = GetCurrentWndRect();

					if (rect.X < 0 || rect.Y < 0 || rect.Width <= 0 || rect.Height <= 0)
					{
						Console.WriteLine("Process hook is succcessful but window is minimized!!");
						hookState = HookStates.WindowMinimized;
					}
					else
					{
						Console.WriteLine("Hook process window successed!!! " + p.ProcessName + "  " + rect.Left + " " + rect.Right + " " + rect.Top + " " + rect.Bottom + " !!!!!!");
						hookState = HookStates.Successed;
					}
				}
				else
				{
					Console.WriteLine("Hook process failed!!!");
					hookState = HookStates.Failed;
				}

			}
			else
			{
				Console.WriteLine("There is no process!!! " + processName);
				hookState = HookStates.NoProcess;
			}
		}

		public Utils.Rect GetCurrentWndRect()
		{
			Utils.Rect rect = new Utils.Rect();

			Utils.GetWindowRect(_noxWndHandle, ref rect);
			rect.Top += 33;
			return rect;
		}

		private void ChangePage(string newPageName)
		{
			_prevPageName = _currentPageName;
			_currentPageName = newPageName;
		}
		

		private void FindAllSubImages(Mat mat)
		{
			foreach (var d in _matchPages)
			{
				d.Value.FindSubImage(mat);
			}
		}


		private void ScoreAllMatchPages(Mat mat)
		{
			string mainLog = "--------------------------ScoreAllMatchPages-----------------------------\n";
			string maxPageName = Globals.UnknownPage;
			double maxSimilarity = 0.0f;

			MatchPage maxPage = null;
			DateTime start = DateTime.Now;

			MatOfFloat descriptors = null;
			_keypoints = null;
			Mat gray = null;

			FastMatchSample.FastDescriptor(mat, out descriptors, out _keypoints, out gray);
			//FastMatchSample.SiftDescriptor(mat, out descriptors, out keypoints, out gray);

			double dissimilarity;
			double similarity;
			string log;

			if (_currentPageName == Globals.UnknownPage)
			{
				foreach (var p in _matchPages)
				{
					var page = p.Value;

					int score = page.PageScore(descriptors, out dissimilarity, out similarity, out log);

					if (_log)
					{
						mainLog += log;
					}

					similarity = page.AdditionalCalcSimilarity(mat, similarity);

					if (similarity > maxSimilarity)
					{
						maxSimilarity = similarity;
						maxPageName = p.Value.PageName;
						maxPage = page;
					}
				}
			}
			else
			{
				var rPages = _matchPages[_currentPageName].RelatedPages;

				foreach (var p in rPages)
				{
					if (!_matchPages.ContainsKey(p))
					{
						Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!! no Page!!!!!!!!!!!!!!!!!!!!!! [" + p + "]");
						continue;
					}

					var page = _matchPages[p];

					int score = page.PageScore(
						descriptors, out dissimilarity, out similarity, out log);

					if (_log)
					{
						mainLog += log;
					}
					

					similarity = page.AdditionalCalcSimilarity(mat, similarity);

					if (similarity > maxSimilarity)
					{
						maxSimilarity = similarity;
						maxPageName = page.PageName;
						maxPage = page;
					}
				}
			}


			if (RenderImageProcess)
			{
				if (maxPage != null)
				{
					maxPage.RenderKeyPoints();
				}

				Utils.RenderKeyPoints(Globals.ImShowCurrentPageName, mat, _keypoints);
			}
			else
			{
				Cv2.DestroyWindow(Globals.ImShowMaxMatchPageName);
				Cv2.DestroyWindow(Globals.ImShowCurrentPageName);
			}


			DateTime end = DateTime.Now;
			var time = (end - start).TotalMilliseconds;

			if (maxSimilarity > 250.0f)
			{
				ChangePage(maxPageName);
			}
			else
			{
				ChangePage(Globals.UnknownPage);
			}
			

			if (_log /*&& _prevPageName != _currentPageName*/)
			{
				mainLog += "\n[" + _currentPageName + "]  maxSimilarity : " + maxSimilarity + "  total time : " + time + "\n";
				mainLog += "-------------------------------------------------------------------------\n\n";
				Console.WriteLine(mainLog);
			}
		}


		private void OnCheckPage(object sender, EventArgs e)
		{
			DateTime startTime = DateTime.Now;

			Utils.Rect rect = GetCurrentWndRect();

			int x = rect.Left;
			int y = rect.Top;
			int width = rect.Right - x;
			int height = rect.Bottom - y;

			Utils.CaptureScreen(x, y, width, height, out _printscreen);

			if (_printscreen == null)
			{
				Console.WriteLine("No window to print screen!");

				HookStates hookState;
				HookProcessHandle(VirtualMachineProcess, out hookState);

				if (hookState == HookStates.NoProcess)
				{
					RestartEmulator();
				}

				return;
			}

			Mat mat = Utils.BitmapToMat(_printscreen);
			
			ScoreAllMatchPages(mat);

			//ScoreAllSlicedPages(mat, x, y, width, height);

			DateTime endTime = DateTime.Now;
		}

		private void ScoreAllSlicedPages(Mat mat, int wndX, int wndY, int wndWidth, int wndHeight)
		{
			DateTime start = DateTime.Now;
			string mainLog = "--------------------------ScoreAllSlicedPages-----------------------------\n";
			string maxPageName = Globals.UnknownPage;
			double maxSimilarity = 0.0f;

			double dissimilarity;
			double similarity;
			string log;

			SlicedPage maxPage = null;

			foreach (var p in _slicedPages)
			{
				p.Value.PageScore(wndX, wndY, out dissimilarity, out similarity, out log, true);

				if (_log)
				{
					mainLog += log;
				}

				if (similarity > maxSimilarity)
				{
					maxSimilarity = similarity;
					maxPageName = p.Value.PageName;
				}

				p.Value.RenderKeyPoints();
			}
			
			/*if (RenderImageProcess)
			{
				if (maxPage != null)
				{
					maxPage.RenderKeyPoints();
				}
			}
			else
			{
				Cv2.DestroyWindow(Globals.ImShowMaxSlicedPageName);
				Cv2.DestroyWindow(Globals.ImShowCurrentSlicedPageName);
			}*/

			DateTime end = DateTime.Now;
			var time = (end - start).TotalMilliseconds;

			/*
			if (maxSimilarity > 250.0f)
			{
				ChangePage(maxPageName);
			}
			else
			{
				ChangePage(Globals.UnknownPage);
			}
			*/

			if (_log /*&& _prevPageName != _currentPageName*/)
			{
				mainLog += "\n[" + maxPageName + "]  maxSimilarity : " + maxSimilarity + "  total time : " + time + "\n";
				mainLog += "-------------------------------------------------------------------------\n\n";
				Console.WriteLine(mainLog);
			}
		}

		private void OnUpdate(object sender, EventArgs e)
		{
			_jobGenerator.Process();
		}


		private void RestartEmulator()
		{
			
			if (_jobGenerator.SubJobs.Count > 0 && 
				 _jobGenerator.SubJobs[0].Type == Jobs.Job.JobTypes.RestartEmulator)
			{
				return;
			}

		
			_jobGenerator.RemoveAllSubJobs();
			_jobGenerator.AddSubJob(new Jobs.JobRestartEmulator(this));
		}

		private void Start()
		{
			_checkPageTimer.Start();
			_updateTimer.Start();
		}

		private void Quit()
		{
			_jobGenerator.RemoveAllSubJobs();
			_checkPageTimer.Stop();
			_updateTimer.Stop();
		}

		private void CreateCaptureBox()
		{
			_captureBox = new CaptureBox(this);
			_captureBox.MdiParent = this.ParentForm;
			_captureBox.Show();
		}

		private void KillCaptureBox()
		{
			_captureBox.Close();
			_captureBox = null;
		}

		
	}
}


