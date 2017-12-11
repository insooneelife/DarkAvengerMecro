using OpenCvSharp;
using SampleBase;
using System;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// find subimage from big image
/// </summary>
class FindSubImageSample
{
	const float TemplateMatchThreshold = 0.75f;	

	static public bool Run(Mat refMat, Mat tplMat, List<Rect> outRects, bool draw = false)
	{
		return Run(refMat, tplMat, outRects, Scalar.White, draw);
	}

	static public bool Run(Mat refMat, Mat tplMat, List<Rect> outRects, Scalar color, bool draw = false)
	{
		if (refMat == null)
		{
			Console.WriteLine("refMat is null!");
		}

		using (Mat res = new Mat(refMat.Rows - tplMat.Rows + 1, refMat.Cols - tplMat.Cols + 1, MatType.CV_32FC1))
		{
			//Convert input images to gray
			Mat gref = refMat.CvtColor(ColorConversionCodes.BGR2GRAY);
			Mat gtpl = tplMat.CvtColor(ColorConversionCodes.BGR2GRAY);

			Cv2.MatchTemplate(gref, gtpl, res, TemplateMatchModes.CCoeffNormed);		
			Cv2.Threshold(res, res, TemplateMatchThreshold, 1.0, ThresholdTypes.Tozero);
						
			while (true)
			{
				double minval, maxval;
				OpenCvSharp.Point minloc, maxloc;
				Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);
				
				if (maxval >= TemplateMatchThreshold)
				{
					//Setup the rectangle to draw
					Rect r = new Rect(
						new OpenCvSharp.Point(maxloc.X, maxloc.Y),
						new OpenCvSharp.Size(tplMat.Width, tplMat.Height));

					outRects.Add(r);

					//Fill in the res Mat so you don't find the same area again in the MinMaxLoc
					if (draw)
					{
						//Draw a rectangle of the matching area
						Cv2.Rectangle(refMat, r, color, 2);

						Rect outRect;
						Cv2.FloodFill(res, maxloc, new Scalar(0), out outRect, new Scalar(0.1), new Scalar(1.0), FloodFillFlags.Link4);
					}

					return true;
				}
				else
					return false;
			}
		}
	}
}

