namespace ClassLibrary1;

public class Class1
{
    public int verticeCount;
    public int edgeCount;
    public int[] fromList;
    public int[] toList;
    public int[] weightList;
    public int[] distance;
    public int startVertex;

    public void fordBoolAlgorithm()
    {
        int vertices = verticeCount; 
        int edges = edgeCount; 
        int[] from = fromList; 
        int[] to = toList; 
        int[] weight = weightList;
    
        distance = new int[vertices];
    
        for (int i = 0; i < vertices; i++) 
        { 
            distance[i] = int.MaxValue;
        }
    
        distance[startVertex] = 0;
    
        for (int i = 0; i < vertices - 1; i++) 
        { 
            for (int j = 0; j < edges; j++) 
            { 
                int u = from[j]; 
                int v = to[j]; 
                int w = weight[j]; 
                if (u < vertices && v < vertices && distance[u] != int.MaxValue && distance[u] + w < distance[v])
                { 
                    distance[v] = distance[u] + w;
                }
            }
        }
        for (int i = 0; i < vertices; i++)
        {
            if (i >= distance.Length)
            {
                throw new IndexOutOfRangeException();
            }
            Console.WriteLine("Distance from vertex " + startVertex + " to vertex " + i + " is " + distance[i]);
        }
    }

    public bool DetectCycles(Class1 class1)
    {
        int[,] adjMatrix = new int[class1.verticeCount, class1.verticeCount];
        for (int i = 0; i < class1.edgeCount; i++)
        {
            adjMatrix[class1.fromList[i], class1.toList[i]] = class1.weightList[i];
        }
        
        int tortoise = 0;
        int hare = 0;
        while (true)
        {
            tortoise = adjMatrix[0, tortoise];
            hare = adjMatrix[0, adjMatrix[0, hare]];
            if (tortoise == hare)
            {
                return true;
            }
        }
    }
}