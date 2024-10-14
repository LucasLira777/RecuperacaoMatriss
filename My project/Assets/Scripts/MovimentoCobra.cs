using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
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

    private int CalculateNewCoordinate()
    {
        int newCoordinate = snakeCoordinates[0]; // Obt�m a posi��o da cabe�a da cobra

        // Calcula a nova posi��o com base na dire��o
        switch (snakeDirection)
        {
            case Direction.Right:
                newCoordinate += gridManager.areaResolution;
                break;
            case Direction.Left:
                newCoordinate -= gridManager.areaResolution;
                break;
            case Direction.Up:
                newCoordinate += 1; // Move para cima
                break;
            case Direction.Down:
                newCoordinate -= 1; // Move para baixo
                break;
        }
        return newCoordinate; // Retorna a nova posi��o
    }

    public void ChangeDirection(Direction newDirection)
    {
        // Verifica se a nova dire��o n�o � oposta � dire��o atual
        if ((snakeDirection == Direction.Up && newDirection != Direction.Down) ||
            (snakeDirection == Direction.Down && newDirection != Direction.Up) ||
            (snakeDirection == Direction.Left && newDirection != Direction.Right) ||
            (snakeDirection == Direction.Right && newDirection != Direction.Left))
        {
            snakeDirection = newDirection; // Muda a dire��o se n�o for oposta
        }
    }

    public int GetTotalPoints()
    {
        return totalPoints; // Retorna a pontua��o total
    }

    public int GetFruitIndex()
    {
        return fruitBlockIndex; // Retorna o �ndice da fruta
    }

    void Update() // M�todo chamado a cada frame
    {
        // Verifica a entrada do jogador para mudar a dire��o
        if (Input.GetKeyDown(KeyCode.W)) // Para cima
        {
            ChangeDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Para baixo
        {
            ChangeDirection(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Para esquerda
        {
            ChangeDirection(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Para direita
        {
            ChangeDirection(Direction.Right);
        }
    }
}
