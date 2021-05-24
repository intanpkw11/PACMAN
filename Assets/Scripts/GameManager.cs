using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Pacman player;
    [SerializeField] private Text scoreValue;

    private float timeDuration = 10;

    void Start()
    {
        player = FindObjectOfType<Pacman>();
    }

    private void Update()
    {
        player.Run();
        ShowScore();

        timeDuration -= Time.deltaTime;
        if(timeDuration <= 0)
        {
            //SpawnFruit();
            timeDuration = 10;
        }
    }

    //fungsi yang dipanggil ketika ingin melakukan spawn fruit
    void SpawnFruit()
    {
        ObjectFactory.Instance.GetObject("Fruit");
    }

    //fungsi untuk menampilkan score
    void ShowScore()
    {
        scoreValue.text = player.Score.ToString();
    }
}
