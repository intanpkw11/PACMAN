using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{ 
    private float timeToSpawnFruit = 15;

    void Start()
    {
        SpawnGhost();
    }

    //fungsi yang dipanggil di Game Manager untuk menjalankan board
    public void Execute()
    {
        SpawnFruitInDuration();
    }

    #region Ghost
    private void SpawnGhost()
    {
        ObjectFactory.Instance.GetObject("GhostRed");
        ObjectFactory.Instance.GetObject("GhostOrange");
        ObjectFactory.Instance.GetObject("GhostPink");
        ObjectFactory.Instance.GetObject("GhostBlue");
    }
    #endregion

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
