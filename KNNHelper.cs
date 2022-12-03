namespace KNN
{
	class KNNHelper
	{
		int?[,] _matrix;
		string[,] _matrixVisual;

		List<Model> population = new();
		List<Model> populationWithKNeighbours = new();
		List<KeyValuePair<string, int>> result = new();
		
		Model serchingModel = new();
		
		Dictionary<int, String> _ocupations = new();

		int _population = 0;
		int _arrayDimSize = 0;
		int _k = 0;

		/// <summary>
		/// KNNHelper  konstruktor
		/// </summary>
		/// <param name="population">Ilość populacji</param>
		/// <param name="arrayDimSize">Rozmiar tablicy</param>
		/// <param name="k">Parametr określający ilu sąsiadó sprawdzić</param>
		public KNNHelper(int population, int arrayDimSize, int k)
		{
			// ustawiamy wartości zgodnie z  przekazanymi parametrami
			_population = population;
			_arrayDimSize = arrayDimSize;
			_matrix = new int?[_arrayDimSize, _arrayDimSize];
			_matrixVisual = new string[_arrayDimSize, _arrayDimSize];
			_k  = k;

			InitDict();

			Populate();
			SearchKNN();
		}

		private void SearchKNN()
		{
			//licz wartości miary euklidesowej
			foreach (var item in population)
			{
				var a = Math.Pow(serchingModel.X - item.X, 2) + Math.Pow(serchingModel.Y - item.Y, 2);
				item.EuklidesValue = Math.Sqrt(a);
			}

			// sortujemy ascending
			population.Sort((a, b) => a.EuklidesValue.CompareTo(b.EuklidesValue));
			
			// ograniczenie listy do k elementów
			populationWithKNeighbours  = population.GetRange(0, _k);

			//grupujemy po zawodach
			var groupedPopulation  = populationWithKNeighbours.GroupBy(x => x.OcupationS);
			
			//zliczamy wystąpienia pogrupowanych zawodów i zapisjemy do listy wynikowej
			foreach (var grp in groupedPopulation)
			{
				result.Add(new KeyValuePair<string, int>(grp.Key, grp.Count()));
			}

			// sortujemy descending, wartość z najwyższą ilością  wystąpień to nasz  rezultat
			result.Sort((a, b) => b.Value.CompareTo(a.Value));
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

		/// <summary>
		/// Wypełniamy tablicę w losowych miejscach losowymi wartościami.
		/// </summary>
		public void Populate()
		{
			Random rnd = new Random();
			Random rndx = new Random();
			Random rndy = new Random();
			int x;
			int y;

			//wypełniamy tablicę ggodnie z ilością  populacji - w tablicy będzie tyle wartości != null co _population
			for (int i = 0; i < _population; i++)
			{
				//sprawdzamy czy już tu nie  zapisaliśmy wcześniej
				CheckIndex(rndx, rndy, out x, out y);

				string ocupation;
				// losujemy  zawód - 6 to ilość zawodów w  _ocupations
				int r = rnd.Next(0, 6);
				//pobieramy jego wartość
				_ocupations.TryGetValue(r, out ocupation);

				//zapisujemy do dwóch tablic
				_matrix[x, y] = r;
				_matrixVisual[x, y] = ocupation;

				//zapisujemy współrzędne i zawód do obiektu  reprezentanta populacji
				population.Add(new Model()
				{
					X = x,
					Y = y,
					Ocupation = r,
					OcupationS = ocupation
				});
			}

			// tu losowo wybieramy miejsce punk z którego będziemy liczyć KNN
			CheckIndex(rndx, rndy, out x, out y);
			serchingModel.X = x;
			serchingModel.Y = y;
			//  i zaznaczamy na tablicach na jednej   999  a na  drugiej XXX
			_matrix[x, y] = 999;
			_matrixVisual[x, y] = "XXX";
		}

		/// <summary>
		/// Sprawdzamy  czy dany  index tablicy  jest wolny
		/// </summary>
		void CheckIndex(Random rndx, Random rndy, out int x, out int y)
		{
			// losujemy  x i y  z przediału <0, _arrayDimSize)
			x = rndx.Next(0, _arrayDimSize);
			y = rndy.Next(0, _arrayDimSize);

			// sprawdzamy czy wylosowane  współrzędne są dostępne, jeśli nie (_matrix[x, y] ma jużjakoś wartość czyli is not null) tolosujemy jeszcze raz
			// czyli  rekurencyjnie wywołujemy CheckIndex
			if (_matrix[x, y] is not null)
				CheckIndex(rndx, rndy, out x, out y);
		}

		public void OutputResults()
		{

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine($"KNN results for {_k}:");
			Console.WriteLine();
			Console.WriteLine(" {0} - {1} results ", result[0].Key, result[0].Value);
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine();
			Console.WriteLine("Grouped results:");
			foreach (var item in result)
				Console.WriteLine(" {0} - {1} results ", item.Key, item.Value);
		}

		public void Output()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine("KNN int");
			Console.WriteLine("===========================================================");
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
			Console.WriteLine("KNN strings");
			Console.WriteLine("===========================================================");
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
		public void OutputDetails()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("===========================================================");
			Console.WriteLine("KNN details:");

			//wypisanie posortowanych wyników odległości
			foreach (var item in population)
			{
				Console.WriteLine(item.OcupationS);
				Console.WriteLine(item.EuklidesValue);
				Console.WriteLine("===========");
			}
		}
	}
}