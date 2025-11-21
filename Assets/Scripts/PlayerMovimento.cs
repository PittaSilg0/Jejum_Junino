using UnityEngine;

public class PlayerMovimento : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float moveSpeed = 5f; // Velocidade de movimento do jogador

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Pega a referência do Rigidbody2D no objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D ou Setas Esquerda/Direita
        movement.y = Input.GetAxisRaw("Vertical");   // W/S ou Setas Cima/Baixa
        movement.Normalize();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
