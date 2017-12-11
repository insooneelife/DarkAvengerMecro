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
using System.IO;

namespace SamplesCS
{
	public partial class CaptureBox : Form
	{
		private MainForm _mainForm;

		public CaptureBox(MainForm mainForm)
		{
			_mainForm = mainForm;
			InitializeComponent();
			this.FormClosed += new FormClosedEventHandler(_mainForm.OnCaptureBoxClosed);
		}

		public PictureBox PictureBox
		{
			get { return pictureBox; } 
		}
		
		System.Drawing.Point _start;
		System.Drawing.Point _end;

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			_start = new System.Drawing.Point(e.X, e.Y);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			StreamWriter writetext = new StreamWriter("write.txt");
			_end = new System.Drawing.Point(e.X, e.Y);
			
			var rect = OpenCvSharp.Rect.FromLTRB(
				Math.Min(_start.X, _end.X),
				Math.Min(_start.Y, _end.Y),
				Math.Max(_start.X, _end.X),
				Math.Max(_start.Y, _end.Y));

			writetext.WriteLine(rect.X + " " + rect.Y + " " + rect.Width + " " + rect.Height);
			writetext.Close();
		}

		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (_mainForm != null)
			{
				_mainForm.OnKeyPress(sender, e);
			}
		}

		private void CaptureBox_Load(object sender, EventArgs e)
		{
			var r = _mainForm.GetCurrentWndRect();

			r.Top -= 33;
			r.Left -= 6;

			Location = new System.Drawing.Point(r.X, r.Y);
		}
	}
}
