namespace KNN
{
	class KNNHelper
	{
		int?[,] _matrix;
		string[,] _matrixVisual;

		List<int> _xPosibilities;
		List<int> _yPosibilities;

		List<Model> population = new();
		Model serchingModel = new();

		Dictionary<int, String> _ocupations = new();

		int _population = 0;
		int _arrayDimSize = 0;

		public KNNHelper(int population, int arrayDimSize, int k)
		{
			_population = population;
			_arrayDimSize = arrayDimSize;

			_matrix = new int?[_arrayDimSize, _arrayDimSize];
			_matrixVisual = new string[_arrayDimSize, _arrayDimSize];

			InitDict();

			Populate();
			SearchKNN(k);

		}

		private void SearchKNN(int k)
		{
			//licz wartości miary euklidesowej
			foreach (var item in population)
			{
				var a = Math.Pow(serchingModel.X - item.X, 2) + Math.Pow(serchingModel.Y - item.Y, 2);
				item.EuklidesValue = Math.Sqrt(a);
			}

			// sortuj ascending

			population.Sort((a, b) => a.EuklidesValue.CompareTo(b.EuklidesValue));

			foreach (var item in population)
			{
				Console.WriteLine(item.OcupationS);
				Console.WriteLine(item.EuklidesValue);
				Console.WriteLine("===========");
			}

			var l = population.GroupBy(x => x.OcupationS);

			foreach (var grp in l)
			{
				Console.WriteLine("{0} {1}", grp.Key, grp.Count());
			}

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

		public void Populate()
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

				_matrix[x, y] = r;
				_matrixVisual[x, y] = ocupation;


				population.Add(new Model()
				{
					X = x,
					Y = y,
					Ocupation = r,
					OcupationS = ocupation
				});

			}

			CheckIndex(rndx, rndy, out x, out y);
			serchingModel.X = x;
			serchingModel.Y = y;



			_matrix[x, y] = 999;
			_matrixVisual[x, y] = "XXX";
		}

		void CheckIndex(Random rndx, Random rndy, out int x, out int y)
		{
			x = rndx.Next(0, _arrayDimSize);
			y = rndy.Next(0, _arrayDimSize);

			if (_matrix[x, y] is not null)
				CheckIndex(rndx, rndy, out x, out y);
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





		//int CheckPossibilitiesX(Random rnd)
		//{
		//	if (_xPosibilities.Count == 0)
		//		return 0;
		//	int rndVal = rnd.Next(0, _arrayDimSize);

		//		if (_xPosibilities.Contains(rndVal))
		//		{
		//			var a = _xPosibilities.IndexOf(rndVal);
		//			_xPosibilities.RemoveAt(a);
		//			return rndVal;

		//		}
		//		else
		//		{
		//		return CheckPossibilitiesX(rnd);
		//		}
		//}

		//int CheckPossibilitiesY(Random rnd)
		//{
		//	if (_yPosibilities.Count == 0)
		//		return 0;

		//	int rndVal = rnd.Next(0, _arrayDimSize);

		//	if (_yPosibilities.Contains(rndVal))
		//	{
		//		var a = _yPosibilities.IndexOf(rndVal);
		//		_yPosibilities.RemoveAt(a);
		//		return rndVal;
		//	}
		//	else
		//	{
		//		return CheckPossibilitiesY(rnd);
		//	}
		//}

	}
}