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
}
