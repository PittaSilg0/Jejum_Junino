using UnityEngine;
// using UnityEngine.SceneManagement; // Desnecessário se estivermos apenas resetando posições

public class GameManager : MonoBehaviour
{
    // Padrão Singleton: Permite acesso fácil a partir de outros scripts
    public static GameManager Instance { get; private set; }

    // Variáveis de Jogo
    public float timeLimit = 30f; // Tempo máximo para pegar a galinha
    public PlayerMovement player; // Arraste o objeto Player aqui
    public Transform chicken; // Arraste o objeto Chicken aqui
    public Vector3 chickenInitialPosition = new Vector3(0f, 10f, 0f); // Posição de Início da Galinha (ajuste)

    private float timer;

    void Awake()
    {
        // Implementação do Singleton
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Opcional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetAll(); // Chama a nova função de reset completo
    }

    void Update()
    {
        // 1. Contador de Tempo
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            TimeIsUp();
        }
    }

    // NOVA FUNÇÃO: Chamada quando o Player colide com um Obstáculo.
    // APENAS penaliza o Player, mantendo o tempo e a Galinha em movimento.
    public void PlayerDied()
    {
        if (player != null)
        {
            player.JustResetPosition();
        }
        // O tempo continua correndo (não chamamos ResetTimer)
    }

    // Função de Reset Completo (Chamada em Vitória ou Fim de Tempo)
    public void ResetAll()
    {
        timer = timeLimit;

        // Reinicia a posição da galinha
        if (chicken != null)
        {
            chicken.position = chickenInitialPosition;
        }

        // Reinicia a posição do Player
        if (player != null)
        {
            player.JustResetPosition();
        }
        Debug.Log("Mini-jogo Reiniciado (Completo).");
    }

    void TimeIsUp()
    {
        Debug.Log("Tempo esgotado! Recomeçando mini-jogo completo.");
        ResetAll();
    }

    // Função de Vitória (O Player alcançou a Galinha)
    public void WinGame()
    {
        Debug.Log("Vitória! Galinha pega!");
        // Por exemplo, prepare o próximo nível ou reinicie para um novo desafio.
        ResetAll();
    }
}