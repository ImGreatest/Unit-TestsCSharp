using ClassLibrary1;

namespace TestProject1;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public void TestSingleAdjacentVertex()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 2;
        class1.edgeCount = 1;
        class1.fromList = new[] { 0 };
        class1.toList = new[] { 1 };
        class1.weightList = new[] { 5 };
        
        class1.fordBoolAlgorithm();
        Assert.Multiple(() =>
        {

            // верны ли вычисления
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(5));
        });
    }

    [Test]
    public void TestShortestPathToNonAdjacentVertex()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 7;
        class1.fromList = new[] { 0, 0, 1, 1, 2, 3, 3 };
        class1.toList = new[] { 1, 2, 2, 3, 4, 4, 2 };
        class1.weightList = new[] { 10, 5, 1, 2, 4, 3, 6 };
        int targetVertex = 1;
        
        class1.fordBoolAlgorithm();
        Assert.Multiple(() =>
        {

            // Вычисление кратчайшего пути от начальной до несмежных
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(10));
            Assert.That(class1.distance[2], Is.EqualTo(5));
            Assert.That(class1.distance[3], Is.EqualTo(12));
            Assert.That(class1.distance[4], Is.EqualTo(9));
            Assert.That(class1.toList[class1.distance.ToList().IndexOf(class1.distance.Min())], Is.EqualTo(targetVertex));
        });
    }

    [Test]
    public void TestShortestPathToVertexMultiplePaths()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 8;
        class1.fromList = new int[] { 0, 0, 0, 1, 1, 2, 2, 3 };
        class1.toList = new int[] { 1, 2, 3, 2, 3, 3, 4, 4 };
        class1.weightList = new int[] { 1, 2, 3, 2, 4, 1, 2, 3 };
        int targetVertex = 4;

        class1.fordBoolAlgorithm();
        Assert.Multiple(() =>
        {

            //кратчайший путь от начальной до вершины у которой несколько путей
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[2], Is.EqualTo(2));
            Assert.That(class1.distance[3], Is.EqualTo(3));
            Assert.That(class1.distance[4], Is.EqualTo(4));
        });
    }

    [Test]
    public void TestNegativeEdgeWeight()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 7;
        class1.fromList = new int[] { 0, 0, 1, 1, 2, 3, 3 };
        class1.toList = new int[] { 1, 2, 2, 3, 4, 4, 2 };
        class1.weightList = new int[] { 1, -2, 3, 2, -4, 3, 6 };
        class1.startVertex = 0;
        
        class1.fordBoolAlgorithm();
        
        // Обработка отрицательного веса ребра
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[1], Is.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[1], Is.EqualTo(1));
        });
    }


    [Test]
    public void TestGraphsWithCycles()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 7;
        class1.fromList = new int[] { 0, 0, 1, 1, 2, 3, 3 };
        class1.toList = new int[] { 1, 2, 2, 3, 4, 4, 2 };
        class1.weightList = new int[] { 1, 2, 3, 2, 4, 3, 6 };
        class1.startVertex = 0;

        bool hasCycles = class1.DetectCycles(class1);

        if (hasCycles)
        {
            Console.WriteLine("Cycle true");
        }
        
        class1.fordBoolAlgorithm();
        
        // Проверка на зацикливание при нахождении пути
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[2], Is.EqualTo(2));
            Assert.That(class1.distance[3], Is.EqualTo(3));
            Assert.That(class1.distance[4], Is.EqualTo(6));
        });
    }

    [Test]
    public void TestGraphWithParallelEdges()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 7;
        class1.fromList = new int[] { 0, 0, 1, 1, 2, 3, 3 };
        class1.toList = new int[] { 1, 2, 2, 3, 4, 4, 2 };
        class1.weightList = new int[] { 1, 2, 3, 2, 4, 3, 6 };
        class1.startVertex = 0;
        
        
        // Проверка на обработку графа с параллельными ребрами
        class1.fordBoolAlgorithm();
        
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[3], Is.EqualTo(3));
            Assert.That(class1.distance[2], Is.EqualTo(2));
            Assert.That(class1.distance[4], Is.EqualTo(6));
        });
    }

    [Test]
    public void TestWithFoursElements()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 4;
        class1.edgeCount = 4;
        class1.fromList = new int[] { 0, 1, 2, 3 };
        class1.toList = new int[] { 1, 2, 2, 3 };
        class1.weightList = new int[] { 1, 2, 3, 4 };
        class1.startVertex = 0;
        
        class1.fordBoolAlgorithm();
        
        // Тест с 3 элементами
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[2], Is.EqualTo(3));
        });
    }

    [Test]
    public void TestOneVertex()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 1;
        class1.edgeCount = 1;
        class1.fromList = new int[] { 0 };
        class1.toList = new int[] { 0 };
        class1.weightList = new int[] { 0 };
        class1.startVertex = 0;
        
        class1.fordBoolAlgorithm();
        
        // тест с 0
        Assert.That(class1.distance[0], Is.EqualTo(0));
    }

    [Test]
    public void TestNoEdgesNoVertex()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 0;
        class1.edgeCount = 0;
        class1.fromList = new int[0];
        class1.toList = new int[0];
        class1.weightList = new int[0];
        class1.startVertex = 0;
        
        Assert.Throws<IndexOutOfRangeException>(() => class1.fordBoolAlgorithm());
        
        // тест с 0
        Assert.Throws<IndexOutOfRangeException>(() =>Assert.That(class1.distance[0], Is.EqualTo(0)));
    }

    [Test]
    public void TestWithFiveElements()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 5;
        class1.edgeCount = 5;
        class1.fromList = new int[] { 0, 1, 2, 3, 4 };
        class1.toList = new int[] { 1, 2, 2, 3, 3 };
        class1.weightList = new int[] { 1, 2, 3, 4 };
        class1.startVertex = 0;
        
        Assert.Throws<IndexOutOfRangeException>(() => class1.fordBoolAlgorithm());
        
        // Тест с 5 элементами
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[2], Is.EqualTo(3));
        });
    }

    [Test]
    public void TestWithTwiceVertex()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 2;
        class1.edgeCount = 1;
        class1.fromList = new int[] { 0 };
        class1.toList = new int[] { 1 };
        class1.weightList = new int[] { 1 };
        class1.startVertex = 0;
        
        class1.fordBoolAlgorithm();
        
        // Тест на поведение алгоритма с малым количеством данных
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[1], Is.EqualTo(1));
        });
    }

    [Test]
    public void TestWithThreeElements()
    {
        Class1 class1 = new Class1();
        class1.verticeCount = 3;
        class1.edgeCount = 3;
        class1.fromList = new int[] { 0, 1, 2 };
        class1.toList = new int[] { 1, 2, 2 };
        class1.weightList = new int[] { 1, 2, 3 };
        class1.startVertex = 0;
        
        class1.fordBoolAlgorithm();
        
        // Тест с 3 элементами
        Assert.Multiple(() =>
        {
            Assert.That(class1.distance[1], Is.EqualTo(1));
            Assert.That(class1.distance[0], Is.EqualTo(0));
            Assert.That(class1.distance[2], Is.EqualTo(3));
        });
    }

    [Test]
    public void TestWithFourElements()
    {
        Class1 graph = new Class1();
        graph.verticeCount = 4;
        graph.edgeCount = 6;
        graph.fromList = new int[] { 0, 0, 1, 1, 2, 2 };
        graph.toList = new int[] { 1, 2, 2, 3, 3, 0 };
        graph.weightList = new int[] { 1, 2, 3, 4, 5, 6 };
        graph.startVertex = 0;
        
        graph.fordBoolAlgorithm();
        
        // Тест с 4 элементами
        Assert.Multiple(() =>
        {
            Assert.That(graph.distance[0], Is.EqualTo(0));
            Assert.That(graph.distance[1], Is.EqualTo(1));
            Assert.That(graph.distance[2], Is.EqualTo(2));
            Assert.That(graph.distance[3], Is.EqualTo(5));
        });
    }

    [Test]
    public void TestWithTenElements()
    {
        Class1 graph = new Class1();
        graph.verticeCount = 5;
        graph.edgeCount = 10;
        graph.fromList = new int[] { 0, 0, 0, 1, 1, 2, 2, 3, 3, 4 };
        graph.toList = new int[] { 1, 2, 3, 2, 4, 3, 4, 4, 0, 2 };
        graph.weightList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        graph.startVertex = 0;
        
        graph.fordBoolAlgorithm();
        Assert.Multiple(() =>
        {
            Assert.That(graph.distance[0], Is.EqualTo(0));
            Assert.That(graph.distance[1], Is.EqualTo(1));
            Assert.That(graph.distance[2], Is.EqualTo(2));
            Assert.That(graph.distance[3], Is.EqualTo(3));
            Assert.That(graph.distance[4], Is.EqualTo(6));
        });
    }
}