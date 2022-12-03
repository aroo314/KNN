// See https://aka.ms/new-console-template for more information
using KNN;
using System;

Console.WriteLine("Hello, World!");

//List<Model> models = new ();

KNNHelper knn = new KNNHelper(200,  15);

knn.Output();

knn.OutputVisual();

