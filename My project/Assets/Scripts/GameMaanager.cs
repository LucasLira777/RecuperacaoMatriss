using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Referencia al controlador de la serpiente
    public SnakeController snakeController;

    // Referencia al controlador de la cuadrícula
    public GridManager gridManager;

    // Velocidad de la serpiente
    public float snakeSpeed = 10f;

    // Indica si el juego ha comenzado
    private bool gameStarted = false;

    // Indica si el juego ha terminado
    private bool gameOver = false;

    // Estilo de texto para mostrar en pantalla
    private GUIStyle mainStyle = new GUIStyle();

    // Temporizador temporal para controlar el movimiento de la serpiente
    private float timeTmp = 0;

    // Total de puntos obtenidos
    private int totalPoints = 0;

    // Método que se ejecuta al iniciar el juego
    void Start()
    {
        // Inicializa la serpiente al comenzar el juego
        snakeController.InitializeSnake();

        // Configura el estilo de texto
        mainStyle.fontSize = 24;
        mainStyle.alignment = TextAnchor.MiddleCenter;
        mainStyle.normal.textColor = Color.white;
    }

    // Método que se llama cada frame
    void Update()
    {
        // Si el juego no ha comenzado
        if (!gameStarted)
        {
            // Inicia el juego si se presiona cualquier tecla
            if (Input.anyKeyDown)
            {
                gameStarted = true;
            }
            return; // Sale del método si el juego no ha comenzado
        }

        // Si el juego ha terminado
        if (gameOver)
        {
            // Reinicia el juego si se presiona la tecla Espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
        else
        {
            // Incrementa el temporizador con el tiempo transcurrido y la velocidad de la serpiente
            timeTmp += Time.deltaTime * snakeSpeed;

            // Si el temporizador ha llegado al valor deseado (1 segundo)
            if (timeTmp >= 1)
            {
                // Reinicia el temporizador
                timeTmp = 0;

                // Mueve la serpiente y actualiza el estado de gameOver
                gameOver = snakeController.MoveSnake();

                // Actualiza los puntos totales obtenidos
                totalPoints = snakeController.GetTotalPoints();
            }
        }
    }

    // Método para mostrar la interfaz gráfica (GUI)
    void OnGUI()
    {
        // Si el juego ha comenzado, muestra los puntos en pantalla
        if (gameStarted)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, 5, 200, 20), totalPoints.ToString(), mainStyle);
        }
        else
        {
            // Si el juego no ha comenzado, muestra un mensaje de inicio
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20),
                      "Press Any Key to Play\n(Use Arrows to Change Direction)", mainStyle);
        }

        // Si el juego ha terminado, muestra el mensaje de Game Over
        if (gameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 40),
                      "Game Over\n(Press 'Space' to Restart)", mainStyle);
        }
    }

    // Método para reiniciar el juego
    void RestartGame()
    {
        // Restablece los valores del juego
        gameOver = false;
        gameStarted = false;
        totalPoints = 0;

        // Reinicializa la serpiente y la cuadrícula
        snakeController.InitializeSnake();
        gridManager.ResetGrid();
    }
}
