using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public bool isEnergizerPellet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isEnergizerPellet)
            {
                //letakkan kode apa yang terjadi jika pacman mengonsumsi energizer pellet
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    
}
