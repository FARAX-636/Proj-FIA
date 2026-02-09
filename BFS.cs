using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BFS : MonoBehaviour
{
	public static List<Node> exploredNodes;
	public static List<Node> FindPathToGoal(Node root)
	{
		Queue<Node> openList = new Queue<Node>();     // frontiera
		HashSet<Node> closedList = new HashSet<Node>();    // nodi esplorati

		// inizializziamo la frontiera col nodo di partenza
		root.parent = null;
		openList.Enqueue(root);

		while(openList.Count > 0)
		{
			Node current = openList.Dequeue();
			if (TestGoal(current))
			{
				exploredNodes = closedList.ToList<Node>();
				return ReconstructPath(current);
			}

			closedList.Add(current);

			foreach (Node n in current.neighbors)
			{
				if (!n.isObstacle && !closedList.Contains(n) && n != null)
				{
					if (!openList.Contains(n))
					{
						openList.Enqueue(n);
					}

					n.parent = current;
					n.previouslyExploredByBFS = true;
				}
			}
		}
		
		return null;   // nessun percorso trovato
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

	public static bool TestGoal(Node n)
	{
		if(GameObject.Find("Food(Clone)").transform.position == n.transform.position)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}