using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // Variáveis públicas para configurar velocidade e limites (ajuste na Unity)
    public float speed = 3f; // Velocidade de movimento (será diferente para cada animal)
    public float rightBoundary = 10f; // Limite X na direita para reiniciar
    public float leftBoundary = -10f; // Limite X na esquerda para respawnar

    void Update()
    {
        // 1. Movimento constante para a esquerda
        // Multiplicamos por -1 para ir para a esquerda
        transform.Translate(Vector3.right * speed * -1 * Time.deltaTime);

        // 2. Reposicionamento (Respawn)
        if (transform.position.x < leftBoundary)
        {
            // Move o objeto para a direita, pronto para cruzar a tela novamente
            transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);

            // Opcional: Adicionar variação de velocidade aqui para torná-lo mais dinâmico
            // speed = Random.Range(3f, 7f); 
        }
    }
}
