
using System;
class GFG
{
	static int V = 9;
	int minDistance(int[] dist,bool[] visited)
	{
		int min = int.MaxValue, min_index = -1;

		for (int v = 0; v < V; v++)
			if (visited[v] == false && dist[v] <= min)
			{
				min = dist[v];
				min_index = v;
			}

		return min_index;
	}
	void printSolution(int[] dist, int n)
	{
		Console.Write("Vertex	 Distance "
					+ "from Source\n");
		for (int i = 0; i < V; i++)
			Console.Write(i + " \t\t " + dist[i] + "\n");
	}

	void dijkstra(int[,] graph, int src)
	{
		int[] dist = new int[V]; 
	
		bool[] visited = new bool[V];

		for (int i = 0; i < V; i++)
		{
			dist[i] = int.MaxValue;
			visited[i] = false;
		}

		// Distance of source vertex
		// from itself is always 0
		dist[src] = 0;

		// Find shortest path for all vertices
		for (int count = 0; count < V - 1; count++)
		{
			int u = minDistance(dist, visited);

			visited[u] = true;

			for (int v = 0; v < V; v++)
				if (!visited[v] && graph[u, v] != 0 &&
					dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
					dist[v] = dist[u] + graph[u, v];
		}

		// print the constructed distance array
		printSolution(dist, V);
	}

	// Driver Code
	public static void Main()
	{
		/* Let us create the example
graph discussed above */
		int[,] graph = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
									{ 4, 0, 8, 0, 0, 0, 0, 11, 0 },
									{ 0, 8, 0, 7, 0, 4, 0, 0, 2 },
									{ 0, 0, 7, 0, 9, 14, 0, 0, 0 },
									{ 0, 0, 0, 9, 0, 10, 0, 0, 0 },
									{ 0, 0, 4, 14, 10, 0, 2, 0, 0 },
									{ 0, 0, 0, 0, 0, 2, 0, 1, 6 },
									{ 8, 11, 0, 0, 0, 0, 1, 0, 7 },
									{ 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
		GFG t = new GFG();
		t.dijkstra(graph, 0);
	}
}

