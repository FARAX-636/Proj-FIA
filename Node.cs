using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Node parent;
	public List<Node> neighbors;
	public bool isObstacle;
	public Vector2Int position;
	public float g;
	public float h;
	public float f;

	public Node(Vector2Int position, bool isObstacle)
	{
		this.parent = null;
		this.neighbors = new List<Node>();
		this.position = position;
		this.isObstacle = isObstacle;
		this.g = float.PositiveInfinity;
		this.h = 0f;
		this.f = 0f;
	}
}