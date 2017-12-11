using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using OpenCvSharp;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;

namespace SamplesCS
{
	public static class Utils
	{
		public const UInt32 MouseLeftDown = 0x0002;
		public const UInt32 MouseLeftUp = 0x0004;

		public const UInt32 MouseRightDown = 0x0008;
		public const UInt32 MouseRightUp = 0x0010;

		public const UInt32 MouseMove = 0x0001;

		public static Random random = new Random();

		[DllImport("user32.dll")]
		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string strClassName, string strWindowName);

		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);
		
		
		public struct Rect
		{
			public int Left { get; set; }
			public int Top { get; set; }
			public int Right { get; set; }
			public int Bottom { get; set; }

			public int Width { get { return Right - Left; } }
			public int Height { get { return Bottom - Top; } }

			public int X { get { return Left; } }
			public int Y { get { return Top; } }
		}

		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		public static void MoveMouse(int x, int y)
		{
			System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
		}

		public static void PressMouse(IntPtr hWnd, UInt32 how)
		{
			SetForegroundWindow(hWnd);
			mouse_event(how, 0, 0, 0, 0);
		}

		public static void SendKeyboard(string text)
		{
			System.Windows.Forms.SendKeys.Send(text);
		}

		public static void Example()
		{
			System.Windows.Forms.Cursor c =
				new System.Windows.Forms.Cursor(System.Windows.Forms.Cursor.Current.Handle);

			System.Windows.Forms.Cursor.Position = new System.Drawing.Point(3200, 400);

			int moveY = -100;

			mouse_event(MouseLeftDown, 0, 0, 0, 0);//make left button down
			Thread.Sleep(1000);
			mouse_event(MouseMove, 0, (uint)moveY, 0, 0);
		}

		public static Process GetProcessByName(string name)
		{
			Process[] processes = Process.GetProcesses();
			
			foreach (var p in processes)
			{
				//Console.WriteLine(p.ProcessName);

				if (p.ProcessName == name)
				{
					//Console.WriteLine("["+p+"]");
					return p; 
				}
			}
			return null;
		}

		public static bool KillProcess(string name)
		{
			var process = GetProcessByName(name);
			if (process != null)
			{ 
				process.Kill();
				return true;
			}
			return false;
		}

		public static bool ExecuteProcess(string name)
		{
			const string NoxPath = "C:/Program Files (x86)/Nox/bin/Nox.exe";
			var process = Process.Start(NoxPath);

			return process != null;
		}

		public static void ProcessExample()
		{
			Process[] processes = Process.GetProcesses();

			foreach (var p in processes)
			{
				IntPtr ptr = p.MainWindowHandle;
				Utils.Rect rect = new Utils.Rect();
				Utils.GetWindowRect(ptr, ref rect);
				
				Console.WriteLine(
					p.ProcessName + " " +
					(int)rect.Left + " " + (int)rect.Right + " " + (int)rect.Top + " " + (int)rect.Bottom);
			}
		}

		static public string GetFileNameInFullEntry(string fileEntry, bool isDirectory = false)
		{
			string[] stringSeparators = new string[] { "/", ".", "\\" };
			string[] result;


			result = fileEntry.Split(stringSeparators, StringSplitOptions.None);

			if (isDirectory)
			{
				if (result.Length < 1)
					return "";

				return result[result.Length - 1];
			}
			else
			{
				if (result.Length < 2)
					return "";

				return result[result.Length - 2];
			}
		}

		static public void CaptureScreen(int screenX, int screenY, int width, int height, out Bitmap printscreen)
		{
			if (width <= 0 || height <= 0 || screenX < 0 || screenY < 0)
			{
				printscreen = null;
				return;
			}

			//Create the Bitmap
			printscreen = new Bitmap(
				width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			//Create the Graphic Variable with screen Dimensions
			Graphics graphics = Graphics.FromImage(printscreen as Image);

			//Copy Image from the screen
			graphics.CopyFromScreen(
				screenX, screenY, 0, 0, printscreen.Size, CopyPixelOperation.SourceCopy);
		}

		

		static public void RenderKeyPoints(string wndName, Mat mat, KeyPoint[] keypoints)
		{
			Mat view = mat.Clone();
			foreach (KeyPoint kp in keypoints)
			{
				view.Circle(kp.Pt, 3, Scalar.Red, -1, LineTypes.AntiAlias, 0);
			}
			Cv2.ImShow(wndName, view);
		}

		public static Bitmap MatToBitmap(Mat image)
		{
			return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
		}

		public static Mat BitmapToMat(Bitmap bitmap)
		{
			return OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
		}

		public static System.Drawing.Point RectToPos(int x, int y, int width, int height)
		{
			System.Drawing.Point pos = new System.Drawing.Point(x, y);

			pos.X += (int)((double)width * random.NextDouble());
			pos.Y += (int)((double)height * random.NextDouble());
			
			return pos;
		}


		public static Point2f[] GetPointsFromKeyPoints(KeyPoint[] keyPoints)
		{
			Point2f[] points = new Point2f[keyPoints.Length];

			for (int i = 0; i< points.Length; ++i)
			{
				points[i] = keyPoints[i].Pt;
			}

			return points;
		}

		public static bool CheckIsPNG(string file)
		{
			return file.Contains(".png") || file.Contains(".PNG");
		}

		public static bool CheckIsTXT(string file)
		{
			return file.Contains(".txt") || file.Contains(".TXT");
		}

	}
}
