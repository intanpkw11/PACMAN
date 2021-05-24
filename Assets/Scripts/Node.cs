using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] aroundNodes;
    public Vector2[] direction;

    void Start()
    {
        direction = new Vector2[aroundNodes.Length];

        for (int i = 0; i < aroundNodes.Length; i++)
        {
            Node node = aroundNodes[i];
            Vector2 distance = (node.transform.position - transform.position).normalized;
            direction[i] = distance;
        }
    }
}
