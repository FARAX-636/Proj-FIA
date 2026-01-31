using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold : MonoBehaviour
{
    public Node startNode;
    public Node foodNode;
    public List<Node> path = null;
    public bool loop = true;
    public float unitsPerSecondCrawled = 2;
    public GameObject moldTentacle;
    public List<GameObject> tentacles;
    public SpriteRenderer spriteRenderer;
    public Color deadColor;
    private int i = 1;

    void Start()
    {
        i = 1;
    }

    void Update()
    {
        if(path != null && loop && i < path.Count)
        {
            loop = false;
            StartCoroutine(Crawl(1f/unitsPerSecondCrawled));
        }
    }

    public void StartSearch()
    {
        path = AStar.FindPathToGoal(startNode, foodNode);
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

    IEnumerator Crawl(float duration)
    {
        var t = Instantiate(moldTentacle, path[i].transform.position, Quaternion.identity);
        tentacles.Add(t);
        yield return new WaitForSeconds(duration);
        i++;
        loop = true;
    }
}
