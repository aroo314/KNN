using KNN;

Console.WriteLine("KNN");

KNNHeart knn = new KNNHeart(200,  15, 50);
//KNNHelper knn = new KNNHelper(80, 10, 30);
knn.Output();
knn.OutputVisual();
knn.OutputResults();
//szczegóły  obliczeń - odkomentuj jeśli chcesz znac wartości poszczególnych pól
//knn.OutputDetails();