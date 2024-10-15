using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Tama�o de la cuadr�cula (resoluci�n del �rea de juego)
    public int areaResolution = 22;

    // Materiales para los diferentes elementos del juego
    public Material groundMaterial;  // Material para el suelo
    public Material snakeMaterial;   // Material para el cuerpo de la serpiente
    public Material headMaterial;    // Material para la cabeza de la serpiente
    public Material fruitMaterial;   // Material para la fruta

    // Array de renderizadores para los bloques de la cuadr�cula
    private Renderer[] gameBlocks;

    // M�todo que se ejecuta al iniciar el juego
    void Start()
    {
        // Genera la cuadr�cula del juego
        GenerateGrid();
    }

    // Genera la cuadr�cula del �rea de juego
    void GenerateGrid()
    {
        // Inicializa el array de bloques de la cuadr�cula
        gameBlocks = new Renderer[areaResolution * areaResolution];

        // Bucle para crear los bloques de la cuadr�cula en un �rea cuadrada
        for (int x = 0; x < areaResolution; x++)
        {
            for (int y = 0; y < areaResolution; y++)
            {
                // Crea un objeto primitivo tipo Quad para representar cada bloque
                GameObject quadPrimitive = GameObject.CreatePrimitive(PrimitiveType.Quad);

                // Establece la posici�n del bloque en la cuadr�cula
                quadPrimitive.transform.position = new Vector3(x, 0, y);

                // Elimina el componente colisionador para evitar colisiones innecesarias
                Destroy(quadPrimitive.GetComponent<Collider>());

                // Rota el Quad 90 grados para que quede paralelo al suelo
                quadPrimitive.transform.localEulerAngles = new Vector3(90, 0, 0);

                // Establece el Quad como hijo del GridManager en la jerarqu�a de la escena
                quadPrimitive.transform.SetParent(transform);

                // Guarda el componente Renderer del Quad en el array gameBlocks
                gameBlocks[(x * areaResolution) + y] = quadPrimitive.GetComponent<Renderer>();
            }
        }
    }

    // Actualiza la cuadr�cula con la posici�n de la serpiente y la fruta
    public void UpdateGrid(List<int> snakeCoordinates, int fruitIndex)
    {
        // Recorre todos los bloques de la cuadr�cula
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
            // Si este bloque es donde est� la fruta, cambia el material a la fruta
            else if (i == fruitIndex)
            {
                gameBlocks[i].sharedMaterial = fruitMaterial;
            }
        }
    }

    // Verifica si una coordenada est� fuera de los l�mites de la cuadr�cula
    public bool IsOutOfBounds(int coordinate, SnakeController.Direction direction)
    {
        // Si la coordenada est� fuera del rango v�lido, est� fuera de los l�mites
        if (coordinate < 0 || coordinate >= gameBlocks.Length)
        {
            return true;
        }
        return false;
    }

    // Restablece la cuadr�cula al material del suelo
    public void ResetGrid()
    {
        // Recorre todos los bloques y restablece su material al del suelo
        for (int i = 0; i < gameBlocks.Length; i++)
        {
            gameBlocks[i].sharedMaterial = groundMaterial;
        }
    }
}
