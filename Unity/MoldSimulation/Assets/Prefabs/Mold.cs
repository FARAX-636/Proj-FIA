using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold : MonoBehaviour
{
    public Node startNode;
    public Node foodNode;
    public List<Node> path = null;
    public List<Node> bFSExplored = null;
    public bool loop = true;
    public float unitsPerSecondCrawled = 2;
    public GameObject moldMainTentacle;
    public GameObject moldExplorerTentacle;
    public List<GameObject> tentacles;
    public SpriteRenderer spriteRenderer;
    public Color deadColor;
    public bool astarAfterBFS = false;
    private int i = 1;
    private int j = 1;

    void Start()
    {
        i = 1;
        j = 1;
    }

    void Update()
    {
        if(bFSExplored != null && loop && j < bFSExplored.Count)
        {
            loop = false;
            StartCoroutine(BFSExploreCrawl(0.2f/unitsPerSecondCrawled));
        }
        if(path != null && loop && i < path.Count)
        {
            loop = false;
            StartCoroutine(Crawl(0.4f/unitsPerSecondCrawled));
        }
    }

    public void StartSearch(int searchMode)
    {
        //Fork per decidere quale algoritmo usare, dipende dalla spunta in UIHandler
        if(searchMode == 0)
        {
            path = AStar.FindPathToGoal(startNode, foodNode, false);
        }
        else if(searchMode == 1)
        {
            path = BFS.FindPathToGoal(startNode);            
            bFSExplored = BFS.exploredNodes;
        }
        else
        {
            BFS.FindPathToGoal(startNode);
            bFSExplored = BFS.exploredNodes;
            astarAfterBFS = true;
        }

        if(path == null)
        {
            spriteRenderer.color = deadColor;
        }
    }

    public void Despawn()
    {
        foreach(GameObject t in tentacles)
        {
            Destroy(t);
        }
        Destroy(gameObject);
    }

    IEnumerator BFSExploreCrawl(float duration)
    {
        for(j = 1; j < bFSExplored.Count; j++)
        {
            var t = Instantiate(moldExplorerTentacle, bFSExplored[j].transform.position, Quaternion.identity);
            tentacles.Add(t);
            yield return new WaitForSeconds(duration);
        }
        if(astarAfterBFS)
        {
            astarAfterBFS = false;
            path = AStar.FindPathToGoal(startNode, foodNode, true);
        }
        loop = true;
    }

    IEnumerator Crawl(float duration)
    {
        for(i = 1; i < path.Count; i++)
        {
            var t = Instantiate(moldMainTentacle, path[i].transform.position, Quaternion.identity);
            tentacles.Add(t);
            yield return new WaitForSeconds(duration);   
        }
        loop = true;
    }
}
