using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //public GameObject prefabPellet;

    private static int WIDTH = 28;
    private static int HEIGHT = 30;

    //public Transform[] pelletPosition;

    private float timeToSpawnFruit = 15;

    void Start()
    {
        /*Kode di bawah error. Index array ga sesuai sama posisi pellet dll. Kalo ga butuh, hapus aja gpp 
         * Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach(GameObject obj in objects)
        {
            Vector2 pos = obj.transform.position;

            if(obj.name != "Pacman")
            {
                Debug.Log(obj.name + " -> " + (int)pos.x + ", " + (int)pos.y);
                //board[(int)pos.x, (int)pos.y] = obj;
            }
        }

        Debug.Log("Objects length: " + objects.Length);*/
    }

    //fungsi yang dipanggil di Game Manager untuk menjalankan board
    public void Execute()
    {
        SpawnFruitInDuration();
    }

    #region Fruit
    //fungsi untuk create object (fruit) dengan menggunakan factory pattern
    private void SpawnFruit()
    {
        ObjectFactory.Instance.GetObject("Fruit");
    }

    //fungsi untuk menghitung durasi kemunculan fruit
    private void SpawnFruitInDuration()
    {
        timeToSpawnFruit -= Time.deltaTime;
        if (timeToSpawnFruit <= 0)
        {
            SpawnFruit();
            timeToSpawnFruit = 15;
        }
    }

    //untuk menghapus atau men-destroy object fruit setelah spawn selama 5 detik.
    public IEnumerator DestroyFruit(Fruit fruitObj)
    {
        if (fruitObj != null)
        {
            yield return new WaitForSeconds(5);
            fruitObj.DestroyFruit();
        }
    }
    #endregion
}
