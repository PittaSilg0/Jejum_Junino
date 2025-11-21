using UnityEngine;

public class ObstacleMovementRight : MonoBehaviour
{
    // Variáveis públicas para configurar velocidade e limites (ajuste na Unity)
    public float speed = 3f; // Velocidade de movimento (será diferente para cada animal)
    public float leftBoundary = -10f; // Limite X na esquerda para reiniciar
    public float rightBoundary = 10f; // Limite X na direita para respawnar

    void Update()
    {
        // 1. Movimento constante para a direita
        // Usamos Vector3.right * speed * 1 (ou simplesmente * speed) para mover para a direita
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // 2. Reposicionamento (Respawn)
        // Se o objeto ultrapassar o limite da direita (rightBoundary)
        if (transform.position.x > rightBoundary)
        {
            // Move o objeto para a esquerda, pronto para cruzar a tela novamente
            transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);

            // Opcional: Adicionar variação de velocidade aqui para torná-lo mais dinâmico
            // speed = Random.Range(3f, 7f); 
        }
    }
}