using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Tamaño de la cuadrícula (resolución del área de juego)
    public int areaResolution = 22;

    // Materiales para los diferentes elementos del juego
    public Material groundMaterial;  // Material para el suelo
    public Material snakeMaterial;   // Material para el cuerpo de la serpiente
    public Material headMaterial;    // Material para la cabeza de la serpiente
    public Material fruitMaterial;   // Material para la fruta

    // Array de renderizadores para los bloques de la cuadrícula
    private Renderer[] gameBlocks;

    // Método que se ejecuta al iniciar el juego
    void Start()
    {
        // Genera la cuadrícula del juego
        GenerateGrid();
    }

    // Genera la cuadrícula del área de juego
    void GenerateGrid()
    {
        // Inicializa el array de bloques de la cuadrícula
        gameBlocks = new Renderer[areaResolution * areaResolution];

        // Bucle para crear los bloques de la cuadrícula en un área cuadrada
        for (int x = 0; x < areaResolution; x++)
        {
            for (int y = 0; y < areaResolution; y++)
            {
                // Crea un objeto primitivo tipo Quad para representar cada bloque
                GameObject quadPrimitive = GameObject.CreatePrimitive(PrimitiveType.Quad);

                // Establece la posición del bloque en la cuadrícula
                quadPrimitive.transform.position = new Vector3(x, 0, y);

                // Elimina el componente colisionador para evitar colisiones innecesarias
                Destroy(quadPrimitive.GetComponent<Collider>());

                // Rota el Quad 90 grados para que quede paralelo al suelo
                quadPrimitive.transform.localEulerAngles = new Vector3(90, 0, 0);

                // Establece el Quad como hijo del GridManager en la jerarquía de la escena
                quadPrimitive.transform.SetParent(transform);

                // Guarda el componente Renderer del Quad en el array gameBlocks
                gameBlocks[(x * areaResolution) + y] = quadPrimitive.GetComponent<Renderer>();
            }
        }
    }

    // Actualiza la cuadrícula con la posición de la serpiente y la fruta
    public void UpdateGrid(List<int> snakeCoordinates, int fruitIndex)
    {
        // Recorre todos los bloques de la cuadrícula
        for (int i = 0; i < gameBlocks.Length; i++)
        {
            // Establece el material del bloque como el material del suelo
            gameBlocks[i].sharedMaterial = groundMaterial;

            // Si la serpiente ocupa este bloque, cambia el material
            if (snakeCoordinates.Contains(i))
            {
                // Si es la cabeza de la serpiente, usa el material de la cabeza
                gameBlocks[i].sharedMaterial = (snakeCoordinates[0] == i) ? headMaterial : snakeMaterial;
            }
            // Si este bloque es donde está la fruta, cambia el material a la fruta
            else if (i == fruitIndex)
            {
                gameBlocks[i].sharedMaterial = fruitMaterial;
            }
        }
    }

    // Verifica si una coordenada está fuera de los límites de la cuadrícula
    public bool IsOutOfBounds(int coordinate, SnakeController.Direction direction)
    {
        // Si la coordenada está fuera del rango válido, está fuera de los límites
        if (coordinate < 0 || coordinate >= gameBlocks.Length)
        {
            return true;
        }
        return false;
    }

    // Restablece la cuadrícula al material del suelo
    public void ResetGrid()
    {
        // Recorre todos los bloques y restablece su material al del suelo
        for (int i = 0; i < gameBlocks.Length; i++)
        {
            gameBlocks[i].sharedMaterial = groundMaterial;
        }
    }
}
