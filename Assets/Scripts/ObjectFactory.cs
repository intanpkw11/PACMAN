using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class factory pattern
public class ObjectFactory : MonoBehaviour
{
    #region singleton
    private static ObjectFactory instance = null;
    
    public static ObjectFactory Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ObjectFactory>();
            }

            return instance;
        }
    }
    #endregion

    [SerializeField] private Fruit fruitPrefab;
    [SerializeField] private Ghost ghostRedPrefab;
    [SerializeField] private Ghost ghostBluePrefab;
    [SerializeField] private Ghost ghostPinkPrefab;
    [SerializeField] private Ghost ghostOrangePrefab;

    public List<Transform> fruitPosition;

    public List<Node> ghostPosition;

    [SerializeField] private List<GameObject> objects;

    private Board board;

    private void Start()
    {
        objects = new List<GameObject>();
        board = FindObjectOfType<Board>();
        AddObjectToList();
    }

    //fungsi yang dipanggil ketika akan men-create object melalui factory 
    public ISpawn GetObject(string name) 
    {
        if (name == "Fruit")
        {
            //cari prefab food dari list objects
            foreach (GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    //clone prefab
                    Fruit fruitObj = Instantiate(obj).GetComponent<Fruit>();
                    int indexPos = Random.Range(0, fruitPosition.Count);
                    Vector2 pos = fruitPosition[indexPos].position;
                    fruitObj.SpawnPosition(pos);
                    StartCoroutine(board.DestroyFruit(fruitObj));

                    //return prefab
                    return fruitObj;
                }
            }
        } 
        else if(name == "GhostRed")
        {
            foreach (GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    Ghost g = Instantiate(obj).GetComponent<Ghost>();
                    g.SpawnPosition(ghostPosition[0].transform.position);

                    return g;
                }
            }
        }
        else if (name == "GhostOrange")
        {
            foreach (GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    Ghost g = Instantiate(obj).GetComponent<Ghost>();
                    g.SpawnPosition(ghostPosition[1].transform.position);

                    return g;
                }
            }
        }
        else if (name == "GhostPink")
        {
            foreach (GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    Ghost g = Instantiate(obj).GetComponent<Ghost>();
                    g.SpawnPosition(ghostPosition[2].transform.position);

                    return g;
                }
            }
        }
        else if (name == "GhostBlue")
        {
            foreach (GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    Ghost g = Instantiate(obj).GetComponent<Ghost>();
                    g.SpawnPosition(ghostPosition[3].transform.position);

                    return g;
                }
            }
        }
        //tambahkan else if disini untuk create object dengan tipe ISpawn lainnya 

        return null;
    }

    //untuk memasukkan prefab object ke list objects
    void AddObjectToList()
    {
        objects.Add(fruitPrefab.gameObject);
        objects.Add(ghostRedPrefab.gameObject);
        objects.Add(ghostBluePrefab.gameObject);
        objects.Add(ghostPinkPrefab.gameObject);
        objects.Add(ghostOrangePrefab.gameObject);
    }
}
