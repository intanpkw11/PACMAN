using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static int boardWidth = 28;
    public static int boardHeight = 30;

    public GameObject[,] board = new GameObject[boardWidth, boardHeight];

    // Start is called before the first frame update
    void Start()
    {
        object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        
        foreach (GameObject o in objects)
        {
            Vector2 pos = o.transform.position;

            if (o.name != "PacMan")
            {
                board[(int)pos.x, (int)pos.y] = o;
            }
            else
            {
                Debug.Log("Found Pacman at : " + pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
