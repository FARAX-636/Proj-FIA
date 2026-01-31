using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject moldPrefab;
    private GameObject moldRef;
    public GameObject foodPrefab;
    private GameObject foodRef;
    public int dimensionX;
    public int dimensionY;
    public InputField inputFieldX;
    public InputField inputFieldy;
    public GameObject [,] nodeGrid = new GameObject[0,0];
    public Camera mainCamera;
    
    void Start()
    {
        GenerateGrid(dimensionX, dimensionY);
    }

    public void GenerateGrid()
    {
        //Prende in input dai textfield a schermo e verifica se il numero inserito è maggiore di 0
        //La verifica della validità è fatta dall'oggetto: non accetta altri caratteri in input
        if(int.Parse(inputFieldX.text) > 0 && int.Parse(inputFieldy.text) > 0)
        {
            GenerateGrid(int.Parse(inputFieldX.text),int.Parse(inputFieldy.text));
        }
    }

    private void GenerateGrid(int xdim, int ydim)
    {
        //Resettiamo la griglia se ha già elementi al suo interno
        if(nodeGrid.Length > 0)
        {
            foreach(GameObject node in nodeGrid)
            {
                Destroy(node);
            }   
        }

        //Resettiamo cibo e muffa se esistono
        if(foodRef != null)
        {
            Destroy(foodRef);
        }

        if(moldRef != null)
        {
            moldRef.GetComponent<Mold>().Despawn();
        }

        //Generiamo una nuova griglia delle dimensioni specificate nei campi a schermo
        nodeGrid = new GameObject[xdim, ydim];
        for(int i = 0; i < xdim; i++)
        {
            for(int j = 0; j < ydim; j++)
            {
                nodeGrid[i,j] = Instantiate(nodePrefab, transform.position + new Vector3(i, j, 0), Quaternion.identity, transform);
            }
        }

        //Spostiamo la telecamera a metà griglia
        mainCamera.transform.position = new Vector3((float)xdim/2f - 0.5f,(float)ydim/2f - 0.5f,-10);

        //Creiamo cibo e muffa in punti casuali della griglia (Random è inclusivo nello 0 e esclusivo nei *dim)
        foodRef = Instantiate(foodPrefab, transform.position + new Vector3(UnityEngine.Random.Range(0, xdim), UnityEngine.Random.Range(0, ydim), 0), Quaternion.identity);
        moldRef = Instantiate(moldPrefab, transform.position + new Vector3(UnityEngine.Random.Range(0, xdim), UnityEngine.Random.Range(0, ydim), 0), Quaternion.identity);
        
        //Inizializziamo le variabili nello script Mold di moldRef
        moldRef.GetComponent<Mold>().startNode = nodeGrid[(int)moldRef.transform.position.x, (int)moldRef.transform.position.y].GetComponent<Node>();
        moldRef.GetComponent<Mold>().foodNode = nodeGrid[(int)foodRef.transform.position.x, (int)foodRef.transform.position.y].GetComponent<Node>();

        GameObject.Find("EventSystem").GetComponent<UIHandler>().mold = moldRef.GetComponent<Mold>();
    }
}
