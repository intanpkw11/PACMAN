using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, ISpawn
{
    private Pacman player;

    void Start()
    {
        player = FindObjectOfType<Pacman>();
    }

    //fungsi implementasi dari interface ISpawn.
    //dipanggil saat akan mengatur posisi spawn dari object fruit.
    public void SpawnPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    //fungsi yang dpanggil saat akan men-destroy object fruit
    public void DestroyFruit()
    {
        Destroy(gameObject);
    }

    //fungsi built-in untuk mendeteksi trigger collision pada fruit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.SetScore(100);
            gameObject.SetActive(false);
        }
    }
}
