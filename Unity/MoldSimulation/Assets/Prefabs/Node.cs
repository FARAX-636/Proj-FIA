using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Node : MonoBehaviour
{
    public bool isObstacle = false;
	public float g = float.PositiveInfinity;
	public float h = 0f;
	public float f = 0f;
	public Node parent = null;
    public SpriteRenderer spriteRenderer;
    public Color pathColor;
    public Color obstacleColor;
    public Transform [] points;
    public Vector2 [] directions;
    public List<Node> neighbors;
    public LayerMask layerMask;
    private int i = 0;
    [SerializeField]    private Text fValue;
    [SerializeField]    private Text gValue;
    [SerializeField]    private Text hValue;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = pathColor;
        SearchForNearbyNodes();
    }

    //La logica del click sulle celle
    public void ToggleIsObstacle()
    {
        isObstacle = !isObstacle;

        if(!isObstacle)
        {
            spriteRenderer.color = pathColor;
        }
        else
        {
            spriteRenderer.color = obstacleColor;
        }
    }

    public void SearchForNearbyNodes()
    {
        //Crea un raycast in ognuna delle 8 direzioni e aggiunge un nodo visto nel caso lo trovi
        foreach(Transform point in points)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(point.position, directions[i], 0.75f, layerMask);
            if(hit2D.collider != null)
            {
                //Debug.Log(gameObject.name + " sees " + hit2D.collider.gameObject.name);
                neighbors.Add(hit2D.collider.gameObject.GetComponent<Node>());
            }
            i++;
        }
    }

    void FixedUpdate()
    {
        gValue.text = "g: "+ Math.Round(g, 3, MidpointRounding.AwayFromZero);
        fValue.text = "f: "+ Math.Round(f, 3, MidpointRounding.AwayFromZero);;
        hValue.text = "h: "+ Math.Round(h, 3, MidpointRounding.AwayFromZero);;
    }
}
