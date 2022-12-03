namespace KNN
{
	class KNNHelper
	{
		int?[,] _matrix;
		string[,] _matrixVisual;

		List<int> _xPosibilities;
		List<int> _yPosibilities;

		Dictionary<int, String> _ocupations  = new();

		int _population = 0;
		int _arrayDimSize = 0;

		public KNNHelper(int population, int arrayDimSize)
		{
			_population = population;
			_arrayDimSize = arrayDimSize;

			_matrix = new int?[_arrayDimSize, _arrayDimSize];
			_matrixVisual = new string[_arrayDimSize, _arrayDimSize];

			InitDict();
			Populate(_matrix);
		}

		void InitDict()
		{
			_ocupations.Add(0, "Bnkr");
			_ocupations.Add(1, "Hand");
			_ocupations.Add(2, "IT");
			_ocupations.Add(3, "Lek");
			_ocupations.Add(4, "Nucz");
			_ocupations.Add(5, "Budo");
			_ocupations.Add(6, "Kuch");
			_ocupations.Add(7, "Keln");
		}

		public void Populate(int?[,] matrix)
		{
			Random rnd = new Random();
			Random rndx = new Random();
			Random rndy = new Random();
			_xPosibilities = Enumerable.Range(0, _arrayDimSize).ToList();
			_yPosibilities = Enumerable.Range(0, _arrayDimSize).ToList();
			int x;
			int y;
			
			for (int i = 0; i < _population; i++)
			{
				CheckIndex(rndx, rndy, out x, out y);

				string ocupation;
				int r = rnd.Next(0, 6);
				_ocupations.TryGetValue(r, out ocupation);

				matrix[x, y] = r;
				_matrixVisual[x, y] = ocupation;

			}

			CheckIndex(rndx, rndy, out x, out y);
			
			matrix[x, y] = 999;
			_matrixVisual[x, y] = "XXX";
		}

		void CheckIndex(Random rndx, Random rndy, out int x, out int y)
		{
			x = rndx.Next(0, _arrayDimSize);
			y = rndy.Next(0, _arrayDimSize);

			if (_matrix[x, y] is not null)
				CheckIndex(rndx, rndy, out x, out y);
		}

		int CheckPossibilitiesX(Random rnd)
		{
			if (_xPosibilities.Count == 0)
				return 0;
			int rndVal = rnd.Next(0, _arrayDimSize);
			
				if (_xPosibilities.Contains(rndVal))
				{
					var a = _xPosibilities.IndexOf(rndVal);
					_xPosibilities.RemoveAt(a);
					return rndVal;

				}
				else
				{
				return CheckPossibilitiesX(rnd);
				}
		}

		int CheckPossibilitiesY(Random rnd)
		{
			if (_yPosibilities.Count == 0)
				return 0;

			int rndVal = rnd.Next(0, _arrayDimSize);

			if (_yPosibilities.Contains(rndVal))
			{
				var a = _yPosibilities.IndexOf(rndVal);
				_yPosibilities.RemoveAt(a);
				return rndVal;
			}
			else
			{
				return CheckPossibilitiesY(rnd);
			}
		}

		public void Output()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine("KNN widok z int");
			Console.WriteLine("===========================================================");
			Console.WriteLine();
			Console.WriteLine();

			for (int i = 0; i < _matrix.GetLength(0); i++)
			{
				for (int j = 0; j < _matrix.GetLength(1); j++)
				{
					Console.Write($"{_matrix[i, j]}\t");
				}
				Console.WriteLine();
			}
		}

		public void OutputVisual()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine("KNN widok ze stringami");
			Console.WriteLine("===========================================================");
			Console.WriteLine();
			Console.WriteLine();

			for (int i = 0; i < _matrix.GetLength(0); i++)
			{
				for (int j = 0; j < _matrix.GetLength(1); j++)
				{
					Console.Write($"{_matrixVisual[i, j]}\t");
				}
				Console.WriteLine();
			}
		}
	}
}
