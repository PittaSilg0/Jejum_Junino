using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // --- Referências de Setup ---
    // Arraste o objeto "DialogueManager" (da etapa 2) para este campo no Inspector.
    public DialogueManager manager;

    // ONDE VOCÊ COLOCARÁ TODAS AS FALAS (Preencha no Inspector)
    public Conversation conversation;

    // Para garantir que o diálogo só comece uma vez (opcional)
    private bool hasTriggered = false;

    // Chamado quando outro Collider2D entra neste Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // IMPORTANTE: Seu Player deve ter a Tag "Player"
        if (other.gameObject.CompareTag("Player") && !hasTriggered)
        {
            // Tenta encontrar o Manager se a referência não foi definida no Inspector
            if (manager == null)
            {
                manager = FindObjectOfType<DialogueManager>();
            }

            // Inicia a conversa e marca como iniciada
            if (manager != null)
            {
                manager.StartConversation(conversation);
                hasTriggered = true;
            }
        }
    }
}