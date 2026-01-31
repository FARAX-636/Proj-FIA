using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
	public static List<Node> FindPathToGoal(Node start, Node goal)
	{
		PriorityQueue<Node> openList = new PriorityQueue<Node>();		 // frontiera
		HashSet<Node> closedList = new HashSet<Node>();  	// nodi esplorati

		// inizializziamo i valori del nodo di partenza
		start.g = 0f;
		start.h = EuclideanDistance(start, goal);
		start.f = start.h;
		start.parent = null;

		openList.Enqueue(start, start.f);  // inizializziamo la frontiera col nodo di partenza
		
		while (openList.Count > 0)
		{
			Node current = openList.Dequeue();
			if (current == goal)
				return ReconstructPath(current);
			
			closedList.Add(current);

			// controlliamo i vicini e calcoliamo le funzioni f, g, h di ognuno di loro
			foreach (Node n in current.neighbors)
			{
				if (!n.isObstacle && !closedList.Contains(n))
				{
					float tentativeG = current.g + 1f;  // per ora, 1f è un placeholder
					
					if (tentativeG < n.g)
					{
						n.g = tentativeG;
						n.h = EuclideanDistance(n, goal);
						n.f = n.g + n.h;
						n.parent = current;

						openList.Enqueue(n, n.f);
					}
				}
			}
		}

		return null; 	// nessun percorso trovato
	}

	/* ricostruiamo tutto il percorso dal nodo iniziale al nodo goal */
	public static List<Node> ReconstructPath(Node current)
	{
		List<Node> newPath = new List<Node>();

		while (current != null)
		{
			newPath.Add(current);
			current = current.parent;
		}
		newPath.Reverse();

		return newPath;
	}

	/* la nostra euristica; è stata scelta la distanza Euclidea perché è la scelta migliore in quanto la muffa si sposta in 8 direzioni */
	public static float EuclideanDistance(Node a, Node b)
	{
		return Mathf.Sqrt(Mathf.Pow(a.transform.position.x - b.transform.position.x, 2) + Mathf.Pow(a.transform.position.y - b.transform.position.y, 2));
	}

}
