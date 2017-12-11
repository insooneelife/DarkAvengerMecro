using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplesCS.Jobs
{
	public class BlackBoard
	{
		public const int UIRectBlackBoardID = 1;
		public const int UICircleBlackBoardID = 2;

		public const int CantAccess = -1;
		const bool Logging = false;

		public static BlackBoard Instance = new BlackBoard();
		private static int _nextGenID = 1;

		private Dictionary<int, object> _sharedData = new Dictionary<int, object>();

		


		public int GetNextID()
		{
			return _nextGenID++;
		}

		public void Write(int boardAccessKey, object value, object who)
		{
			if (boardAccessKey == CantAccess)
			{
				Console.WriteLine("Can't access black board!!");
				return;
			}

			if (Logging)
			{
				Console.WriteLine("<BlackBoard>  [" + who + "] writed : " + value);
			}

			_sharedData[boardAccessKey] = value;
		}

		public object Read(int boardAccessKey, object who)
		{
			if (boardAccessKey == CantAccess)
			{
				Console.WriteLine("Can't access black board!!");
				return null;
			}

			var data = _sharedData[boardAccessKey];

			if (Logging)
			{ 
				Console.WriteLine("<BlackBoard>  [" + who + "] readed : " + data);
			}

			return data;
		}
	}
}
