using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCobra : MonoBehaviour
{
    public GridManager gridManager;
    private List<int> snakeCoordinates = new List<int>();
    public enum Direction { Up, Down, Left, Right };
    private Direction snakeDirection = Direction.Right;
    private int fruitBlockIndex = -1;
    private int totalPoints = 0;

    public void InitializeSnake()
    {
        snakeCoordinates.Clear();
        int startCoordinate = Random.Range(0, gridManager.areaResolution - 1) + (gridManager.areaResolution * 3);
        snakeCoordinates.Add(startCoordinate);
        snakeCoordinates.Add(startCoordinate - gridManager.areaResolution);
        snakeCoordinates.Add(startCoordinate - (gridManager.areaResolution * 2));
        fruitBlockIndex = -1;
        snakeDirection = Direction.Right; // Inicialmente, a cobra vai para a direita
    }

    public bool MoveSnake()
    {
        bool gameOver = false;
        int newCoordinate = CalculateNewCoordinate();

        if (snakeCoordinates.Contains(newCoordinate) || gridManager.IsOutOfBounds(newCoordinate, snakeDirection))
        {
            gameOver = true; // O jogo termina se a cobra colidir com ela mesma ou sair do grid
        }
        else
        {
            snakeCoordinates.Insert(0, newCoordinate); // Adiciona a nova posi��o na frente da cobra
            if (newCoordinate != fruitBlockIndex) // Se n�o comeu a fruta
            {
                snakeCoordinates.RemoveAt(snakeCoordinates.Count - 1); // Remove a �ltima posi��o
            }
            else
            {
                totalPoints++; // Incrementa a pontua��o se a cobra comer a fruta
                fruitBlockIndex = -1; // Reseta o �ndice da fruta
            }
            gridManager.UpdateGrid(snakeCoordinates, fruitBlockIndex); // Atualiza a grade
        }
        return gameOver; // Retorna se o jogo acabou
    }
}
