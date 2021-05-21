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
    //[SerializeField] private Food foodPrefab;

    public List<GameObject> objects;

    private void Start()
    {
        objects = new List<GameObject>();
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
                    fruitObj.SpawnPosition();
                    StartCoroutine(DestroyFruit(fruitObj));

                    //return prefab
                    return fruitObj;
                }
            }
        } else if(name == "Food")
        {
            foreach(GameObject obj in objects)
            {
                if (obj.name == name)
                {
                    //Food foodObj = Instantiate(obj).GetComponent<Food>();
                    //return foodObj;
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
        //objects.Add(foodPrefab.gameObject);
    }

    //untuk menghapus atau men-destroy object fruit setelah spawn selama 3 detik.
    //mungkin fungsi ini akan dipindahkan ke class Board
    IEnumerator DestroyFruit(Fruit fruitObj)
    {
        if(fruitObj != null)
        {
            yield return new WaitForSeconds(5);
            fruitObj.DestroyFruit();
        }
    }
}
