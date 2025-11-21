using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo e Suavização")]
    public Transform target;          // O jogador para seguir
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // Suavidade do seguimento (valor entre 0.01 e 1)

    [Header("Limites (Bounds)")]
    // Limites absolutos do mapa (coordenadas de mundo)
    public float minX; // Limite Esquerdo
    public float maxX; // Limite Direito
    public float minY; // Limite Inferior
    public float maxY; // Limite Superior

    // --- Variáveis Auxiliares (Preenchidas automaticamente) ---
    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        // 1. Verificação de Segurança
        if (target == null)
        {
            Debug.LogError("O campo 'Target' não está definido no script CameraFollow! Arraste o Jogador para ele.");
            enabled = false; // Desativa o script para evitar erros contínuos.
            return;
        }

        // 2. Calcula a metade da altura e largura da tela da câmera
        // Isso é essencial para calcular a margem correta dos limites.
        Camera cam = Camera.main;
        if (cam == null || !cam.orthographic)
        {
            Debug.LogError("A Main Camera precisa ser Orthographic para o script 2D funcionar corretamente.");
            enabled = false;
            return;
        }

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        // **NOTA DE CORREÇÃO:**
        // A lógica de forçar a posição inicial da câmera foi removida do Start().
        // A câmera começará na posição que você definiu no editor.
    }

    // É usado LateUpdate para garantir que a câmera se mova DEPOIS que o jogador se moveu
    void LateUpdate()
    {
        if (target == null) return;

        // 1. Posição Alvo Desejada
        // Apenas X e Y são do Player; o Z é mantido como o Z original da câmera (-10, por exemplo).
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        // 2. Interpolar (Suavizar) para a posição desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // 3. Aplicar os Limites (Clamp)
        // O Clamp garante que a BORDA da câmera não ultrapasse o limite do mapa.
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY + camHalfHeight, maxY - camHalfHeight);

        // 4. Definir a Posição Final da Câmera
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}