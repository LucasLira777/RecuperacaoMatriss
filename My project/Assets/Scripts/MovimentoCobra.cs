using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Referencia al gestor de la cuadrícula
    public GridManager gridManager;

    // Lista que contiene las coordenadas de los bloques ocupados por la serpiente
    private List<int> snakeCoordinates = new List<int>();

    // Enumeración de las posibles direcciones en las que la serpiente puede moverse
    public enum Direction { Up, Down, Left, Right };

    // Dirección actual de la serpiente
    private Direction snakeDirection = Direction.Right;

    // Índice del bloque donde está la fruta
    private int fruitBlockIndex = -1;

    // Puntuación total del jugador
    private int totalPoints = 0;

    // Método para inicializar la serpiente
    public void InitializeSnake()
    {
        // Limpia las coordenadas anteriores de la serpiente
        snakeCoordinates.Clear();

        // Establece la coordenada inicial de la serpiente
        int startCoordinate = Random.Range(0, gridManager.areaResolution - 1) + (gridManager.areaResolution * 3);

        // Agrega las coordenadas iniciales de la serpiente (cabeza y dos bloques adicionales)
        snakeCoordinates.Add(startCoordinate);
        snakeCoordinates.Add(startCoordinate - gridManager.areaResolution);
        snakeCoordinates.Add(startCoordinate - (gridManager.areaResolution * 2));

        // Reinicia el índice de la fruta
        fruitBlockIndex = -1;

        // Reinicia la dirección de la serpiente a la derecha
        snakeDirection = Direction.Right;
    }

    // Método para mover la serpiente
    public bool MoveSnake()
    {
        bool gameOver = false; // Bandera para determinar si el juego ha terminado

        // Calcula la nueva coordenada basada en la dirección de la serpiente
        int newCoordinate = CalculateNewCoordinate();

        // Verifica si la serpiente se ha chocado consigo misma o ha salido de los límites
        if (snakeCoordinates.Contains(newCoordinate) || gridManager.IsOutOfBounds(newCoordinate, snakeDirection))
        {
            gameOver = true; // Si hay colisión, el juego termina
        }
        else
        {
            // Inserta la nueva coordenada al frente de la lista (la cabeza se mueve)
            snakeCoordinates.Insert(0, newCoordinate);

            // Si no ha comido la fruta, elimina la última coordenada (mueve el cuerpo)
            if (newCoordinate != fruitBlockIndex)
            {
                snakeCoordinates.RemoveAt(snakeCoordinates.Count - 1);
            }
            else
            {
                // Si ha comido la fruta, incrementa los puntos y resetea la fruta
                totalPoints++;
                fruitBlockIndex = -1; // La fruta desaparece
            }

            // Actualiza la cuadrícula con las nuevas posiciones de la serpiente y la fruta
            gridManager.UpdateGrid(snakeCoordinates, fruitBlockIndex);
        }

        return gameOver; // Devuelve si el juego ha terminado
    }

    // Método para calcular la nueva coordenada de la cabeza de la serpiente
    private int CalculateNewCoordinate()
    {
        int newCoordinate = snakeCoordinates[0]; // Obtiene la posición actual de la cabeza

        // Calcula la nueva posición según la dirección
        switch (snakeDirection)
        {
            case Direction.Right:
                newCoordinate += gridManager.areaResolution; // Mover a la derecha
                break;
            case Direction.Left:
                newCoordinate -= gridManager.areaResolution; // Mover a la izquierda
                break;
            case Direction.Up:
                newCoordinate += 1; // Mover hacia arriba
                break;
            case Direction.Down:
                newCoordinate -= 1; // Mover hacia abajo
                break;
        }

        return newCoordinate; // Retorna la nueva posición
    }

    // Método para cambiar la dirección de la serpiente
    public void ChangeDirection(Direction newDirection)
    {
        // Evita que la serpiente se mueva en dirección opuesta a su movimiento actual
        if ((snakeDirection == Direction.Up && newDirection != Direction.Down) ||
            (snakeDirection == Direction.Down && newDirection != Direction.Up) ||
            (snakeDirection == Direction.Left && newDirection != Direction.Right) ||
            (snakeDirection == Direction.Right && newDirection != Direction.Left))
        {
            snakeDirection = newDirection; // Cambia la dirección si no es opuesta
        }
    }

    // Método para obtener la puntuación total
    public int GetTotalPoints()
    {
        return totalPoints; // Devuelve la puntuación total
    }

    // Método para obtener el índice de la fruta
    public int GetFruitIndex()
    {
        return fruitBlockIndex; // Retorna la posición de la fruta
    }

    // Método Update llamado una vez por frame
    void Update()
    {
        // Detecta las teclas de dirección y cambia la dirección de la serpiente en consecuencia
        if (Input.GetKeyDown(KeyCode.W)) // Tecla W: Arriba
        {
            ChangeDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Tecla S: Abajo
        {
            ChangeDirection(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Tecla A: Izquierda
        {
            ChangeDirection(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Tecla D: Derecha
        {
            ChangeDirection(Direction.Right);
        }
    }
}
