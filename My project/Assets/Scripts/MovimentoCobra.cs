using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Referencia al gestor de la cuadr�cula
    public GridManager gridManager;

    // Lista que contiene las coordenadas de los bloques ocupados por la serpiente
    private List<int> snakeCoordinates = new List<int>();

    // Enumeraci�n de las posibles direcciones en las que la serpiente puede moverse
    public enum Direction { Up, Down, Left, Right };

    // Direcci�n actual de la serpiente
    private Direction snakeDirection = Direction.Right;

    // �ndice del bloque donde est� la fruta
    private int fruitBlockIndex = -1;

    // Puntuaci�n total del jugador
    private int totalPoints = 0;

    // M�todo para inicializar la serpiente
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

        // Reinicia el �ndice de la fruta
        fruitBlockIndex = -1;

        // Reinicia la direcci�n de la serpiente a la derecha
        snakeDirection = Direction.Right;
    }

    // M�todo para mover la serpiente
    public bool MoveSnake()
    {
        bool gameOver = false; // Bandera para determinar si el juego ha terminado

        // Calcula la nueva coordenada basada en la direcci�n de la serpiente
        int newCoordinate = CalculateNewCoordinate();

        // Verifica si la serpiente se ha chocado consigo misma o ha salido de los l�mites
        if (snakeCoordinates.Contains(newCoordinate) || gridManager.IsOutOfBounds(newCoordinate, snakeDirection))
        {
            gameOver = true; // Si hay colisi�n, el juego termina
        }
        else
        {
            // Inserta la nueva coordenada al frente de la lista (la cabeza se mueve)
            snakeCoordinates.Insert(0, newCoordinate);

            // Si no ha comido la fruta, elimina la �ltima coordenada (mueve el cuerpo)
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

            // Actualiza la cuadr�cula con las nuevas posiciones de la serpiente y la fruta
            gridManager.UpdateGrid(snakeCoordinates, fruitBlockIndex);
        }

        return gameOver; // Devuelve si el juego ha terminado
    }

    // M�todo para calcular la nueva coordenada de la cabeza de la serpiente
    private int CalculateNewCoordinate()
    {
        int newCoordinate = snakeCoordinates[0]; // Obtiene la posici�n actual de la cabeza

        // Calcula la nueva posici�n seg�n la direcci�n
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

        return newCoordinate; // Retorna la nueva posici�n
    }

    // M�todo para cambiar la direcci�n de la serpiente
    public void ChangeDirection(Direction newDirection)
    {
        // Evita que la serpiente se mueva en direcci�n opuesta a su movimiento actual
        if ((snakeDirection == Direction.Up && newDirection != Direction.Down) ||
            (snakeDirection == Direction.Down && newDirection != Direction.Up) ||
            (snakeDirection == Direction.Left && newDirection != Direction.Right) ||
            (snakeDirection == Direction.Right && newDirection != Direction.Left))
        {
            snakeDirection = newDirection; // Cambia la direcci�n si no es opuesta
        }
    }

    // M�todo para obtener la puntuaci�n total
    public int GetTotalPoints()
    {
        return totalPoints; // Devuelve la puntuaci�n total
    }

    // M�todo para obtener el �ndice de la fruta
    public int GetFruitIndex()
    {
        return fruitBlockIndex; // Retorna la posici�n de la fruta
    }

    // M�todo Update llamado una vez por frame
    void Update()
    {
        // Detecta las teclas de direcci�n y cambia la direcci�n de la serpiente en consecuencia
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
