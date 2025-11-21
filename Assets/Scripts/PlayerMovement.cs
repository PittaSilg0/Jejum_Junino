using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variáveis públicas para configuração no Inspector
    public float moveSpeed = 5f; // Velocidade de movimento do jogador
    public Vector3 initialPosition = new Vector3(0f, -4f, 0f); // Posição de Início (ajuste na Unity)

    private Rigidbody2D rb;
    private Vector2 movement; // oi@

    void Start()
    {
        // Garante que o jogador começa na posição inicial
        rb = GetComponent<Rigidbody2D>();
        JustResetPosition(); // Usa a nova função para iniciar
    }

    void Update()
    {
        // 1. Leitura de Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normaliza o vetor para que o movimento diagonal não seja mais rápido
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        // 2. Aplica movimento baseado na física (ideal para Rigidbody)
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 3. Detecção de Colisão para "Perder"
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log($"Fui atingido por {other.gameObject.name}! Penalidade aplicada.");

            // Chama a função do GameManager que lida APENAS com a morte do Player
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDied();
            }
            else
            {
                // Fallback: Se o GameManager não existir, apenas move o Player
                JustResetPosition();
            }
        }

        // 4. Detecção de Vitória (Galinha)
        if (other.CompareTag("Goal"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
            }
        }
    }

    // FUNÇÃO REVISADA: Apenas move o Player para o ponto inicial. 
    // NÃO CHAMA NENHUMA FUNÇÃO DE RESET DE TEMPO OU GALINHA.
    public void JustResetPosition()
    {
        transform.position = initialPosition;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Player retornado ao ponto inicial.");
    }
}