using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaanager : MonoBehaviour
{
    public SnakeController snakeController;
    public GridManager gridManager;
    public float snakeSpeed = 10f;
    private bool gameStarted = false;
    private bool gameOver = false;
    private GUIStyle mainStyle = new GUIStyle();
    private float timeTmp = 0;
    private int totalPoints = 0;

    void Start()
    {
        snakeController.InitializeSnake();
        mainStyle.fontSize = 24;
        mainStyle.alignment = TextAnchor.MiddleCenter;
        mainStyle.normal.textColor = Color.white;
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
            }
            return;
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
        else
        {
            timeTmp += Time.deltaTime * snakeSpeed;
            if (timeTmp >= 1)
            {
                timeTmp = 0;
                gameOver = snakeController.MoveSnake();
                totalPoints = snakeController.GetTotalPoints();
            }
        }
    }
}
