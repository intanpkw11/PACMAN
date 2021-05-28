using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Pacman player;
    private Board board;
    [SerializeField] private Ghost[] ghosts;
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
        ghosts = FindObjectsOfType<Ghost>();
        Pellet[] pelletObj = FindObjectsOfType<Pellet>();

        foreach(Pellet obj in pelletObj)
        {
            pellets.Add(obj);
        }
    }

    private void Update()
    {
        //saat gameIsOver = false, jalankan semua aktivitas game
        if (!gameIsOver)
        {
            player.Execute();
            board.Execute();
            foreach(Ghost g in ghosts)
            {
                g.Execute();
            }
            WinLoseCondition();
        }
        
        ShowData();
    }

    //fungsi untuk menampilkan data score dan lives pacman
    private void ShowData()
    {
        scoresValue.text = player.Scores.ToString();
        livesValue.text = player.Lives.ToString();
    }

    //return jumlah pellet yang telah di enable, untuk di cek dengan total pellet yang ada
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
