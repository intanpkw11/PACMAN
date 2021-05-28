using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public bool isEnergizerPellet;
    private List<Ghost> ghostsList = new List<Ghost>();

    private void Start()
    {
        Ghost[] ghosts = FindObjectsOfType<Ghost>();

        foreach(Ghost ghost in ghosts)
        {
            ghostsList.Add(ghost);
        }
    }

    //untuk deteksi trigger pada pellet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isEnergizerPellet)
            {
                //saat pacman mengonsumsi energizer pellet, semua ghost akan berubah ke mode frightened
                for(int i = 0; i < ghostsList.Count; i++)
                {
                    ghostsList[i].StartFrightenedMode();
                }

            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
