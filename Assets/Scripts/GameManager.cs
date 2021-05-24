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

    private float timeDuration = 10;

    void Start()
    {
        player = FindObjectOfType<Pacman>();
        board = FindObjectOfType<Board>();
    }

    private void Update()
    {
        player.Execute();
        board.Execute();
        ShowData();
    }

    //fungsi untuk menampilkan data score dan lives pacman
    void ShowData()
    {
        scoresValue.text = player.Scores.ToString();
        livesValue.text = player.Lives.ToString();
    }
}
