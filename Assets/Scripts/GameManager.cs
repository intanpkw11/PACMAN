using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Pacman player;
    private Board board;
    [SerializeField] private Text scoresValue;
    [SerializeField] private Text livesValue;
    [SerializeField] private Text winText;
    [SerializeField] private Text gameoverText;

    [SerializeField] private List<Pellet> pellets;

    private bool gameIsOver = false;

    void Start()
    {
        player = FindObjectOfType<Pacman>();
        board = FindObjectOfType<Board>();
        Pellet[] pelletObj = FindObjectsOfType<Pellet>();

        foreach(Pellet obj in pelletObj)
        {
            pellets.Add(obj);
        }
    }

    private void Update()
    {
        if (!gameIsOver)
        {
            player.Execute();
            board.Execute();
            WinLoseCondition();
        }
        
        ShowData();
    }

    //fungsi untuk menampilkan data score dan lives pacman
    void ShowData()
    {
        scoresValue.text = player.Scores.ToString();
        livesValue.text = player.Lives.ToString();
    }

    //untuk cek jumlah pellet yang telah di 
    private List<Pellet> CheckPelletsList()
    {
        List<Pellet> removePellet = new List<Pellet>();

        foreach (Pellet p in pellets)
        {
            if (!p.GetComponent<SpriteRenderer>().enabled)
            {
                removePellet.Add(p);
            }
        }

        return removePellet;
    }

    private void WinLoseCondition()
    {
        if(CheckPelletsList().Count == pellets.Count && player.Lives > 0)
        {
            winText.gameObject.SetActive(true);
            gameIsOver = true;
        }
        else if(player.Lives <= 0)
        {
            gameoverText.gameObject.SetActive(true);
            gameIsOver = true;
        }  
    }
}
