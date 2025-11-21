using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public Transform target; // Arraste o objeto Player para este slot no Inspector
    public float smoothSpeed = 0.125f; // Suavidade do movimento (quanto menor, mais suave)

    // A posição Z (distância da câmera) deve ser mantida constante
    private float zOffset;

    void Start()
    {
        // Armazena a posição Z inicial da câmera (geralmente -10 para 2D)
        zOffset = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return; // Segurança para verificar se o alvo existe

        // 1. Calcula a posição desejada
        // Mantém o X da câmera (o X da câmera NUNCA muda)
        // Usa o Y do Player (target.position.y)
        // Usa o Z original da câmera (zOffset)
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, zOffset);

        // 2. Interpolação (movimento suave)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 3. Aplica a nova posição
        transform.position = smoothedPosition;
    }
}
