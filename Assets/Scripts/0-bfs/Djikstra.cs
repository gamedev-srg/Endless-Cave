using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Djikstra
{

    public static NodeType FindMin<NodeType>(Dictionary<NodeType, float> dist, HashSet<NodeType> visited) 
    {
        float min = int.MaxValue;
        NodeType min_index = default;
        foreach(var node in dist)
        {
            if (node.Value < min&&!visited.Contains(node.Key))
            {
                min = node.Value;
                min_index = node.Key;
            }
        }
        return min_index;
    }

    public static void FindPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, List<NodeType> outputPath, int maxiterations = 1000)
    {

        //<NodeType, float> priq = new PriorityQueue<NodeType, float>();
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> visited = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        Dictionary<NodeType, float> distance = new Dictionary<NodeType, float>();
        openQueue.Enqueue(startNode);
        visited.Add(startNode);
        bool reached = false;


        distance[startNode] = 0;
        int i; for (i = 0; i < maxiterations; ++i)
        { // After maxiterations, stop and return an empty path
            while (openQueue.Count > 0 && !reached)
            {

                NodeType current = FindMin(distance, visited);
               
                    foreach (var neighbor in graph.Neighbors(current))
                    {

                        if (!visited.Contains(neighbor))
                        {
                            if (!distance.ContainsKey(neighbor))
                            {
                                distance[neighbor] = int.MaxValue;
                            }

                            if (distance[neighbor] > distance[current] + graph.GetDistance(neighbor, current))
                            {
                                previous[neighbor] = current;
                                distance[neighbor] = distance[current] + graph.GetDistance(neighbor, current);
                                if (neighbor.Equals(endNode))
                                {
                                    reached = true;
                                    break;
                                }
                            }
                        }
                        if (!openQueue.Contains(neighbor))
                        {
                            openQueue.Enqueue(neighbor);
                        }
                   // }
                    visited.Add(current);
                    openQueue.Dequeue();

                }
            }
        }

    }
    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations = 1000)
    {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }
}
