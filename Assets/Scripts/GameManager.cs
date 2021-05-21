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
        //StartCoroutine(TimeFood());
    }

    private void Update()
    {
        ShowScore();
        timeDuration -= Time.deltaTime;
        if(timeDuration <= 0)
        {
            SpawnFruit();
            timeDuration = 10;
        }
    }

    //fungsi yang dipanggil ketika ingin melakukan spawn fruit
    void SpawnFruit()
    {
        ObjectFactory.Instance.GetObject("Fruit");
    }

    //fungsi yang dipanggil ketika ingin melakukan spawn food (titik besar)
    /*void SpawnFood()
    {
        Food topRightCorner = (Food)ObjectFactory.Instance.GetObject("Food");
        topRightCorner.SetPosition(8.75f, 3.6f);
        topRightCorner.SpawnPosition();

        Food topLeftCorner = (Food)ObjectFactory.Instance.GetObject("Food");
        topLeftCorner.SetPosition(-8.75f, 3.6f);
        topLeftCorner.SpawnPosition();

        Food buttomRightCorner = (Food)ObjectFactory.Instance.GetObject("Food");
        buttomRightCorner.SetPosition(8.75f, -3.6f);
        buttomRightCorner.SpawnPosition();

        Food buttomLeftCorner = (Food)ObjectFactory.Instance.GetObject("Food");
        buttomLeftCorner.SetPosition(-8.75f, -3.6f);
        buttomLeftCorner.SpawnPosition();
    }*/

    //fungsi untuk menampilkan score
    void ShowScore()
    {
        scoreValue.text = player.GetScore().ToString();
    }

    /*IEnumerator TimeFood()
    {
        yield return new WaitForSeconds(0.001f);
        SpawnFood();
    }*/
}
