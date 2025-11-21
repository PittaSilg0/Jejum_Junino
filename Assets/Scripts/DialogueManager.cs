using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // --- Referências de UI ---
    // ATENÇÃO: ARRASTE OS OBJETOS DE UI (TextMeshPro e Panel) AQUI NO INSPECTOR
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel; // O Panel principal que deve sumir

    // --- Controle de Fluxo ---
    private Queue<DialogueLine> dialogueLines; // Fila que armazena a sequência de falas (Nome + Frase)
    public float typingSpeed = 0.05f; // Velocidade do efeito de digitação

    private bool isTyping = false;
    private bool dialogueActive = false;

    void Awake()
    {
        dialogueLines = new Queue<DialogueLine>();
        // Garante que o painel comece oculto
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Avança o diálogo ao pressionar Espaço ou Enter, se o diálogo estiver ativo
        if (dialogueActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            DisplayNextLine();
        }
    }

    // --- MÉTODO PRINCIPAL: Chamado pelo DialogueTrigger ---
    // Recebe a estrutura Conversation (que contém todas as DialogueLines)
    public void StartConversation(Conversation conversation)
    {
        // Impede que um diálogo comece se outro já estiver rodando
        if (dialogueActive)
            return;

        dialogueActive = true;
        dialoguePanel.SetActive(true);

        // 1. Limpa a fila e adiciona as novas linhas
        dialogueLines.Clear();
        foreach (DialogueLine line in conversation.lines)
        {
            dialogueLines.Enqueue(line);
        }

        // 2. Exibe a primeira linha
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        // 1. Se estiver digitando, apenas revela a frase completa instantaneamente
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines.Peek().sentence;
            isTyping = false;
            return;
        }

        // 2. Se não houver mais falas, encerra (a fila está vazia)
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 3. Pega a próxima linha da fila
        DialogueLine currentLine = dialogueLines.Dequeue();

        // 4. Atualiza o nome do personagem
        nameText.text = currentLine.name;

        // 5. Inicia a digitação da frase
        StartCoroutine(TypeSentence(currentLine.sentence));
    }

    // Efeito máquina de escrever
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // --- LÓGICA DE FIM DE DIÁLOGO ---
    void EndDialogue()
    {
        // Inicia o coroutine para dar um pequeno tempo antes de fechar
        StartCoroutine(CloseDialogueBox(0.5f)); // Espera 0.5 segundos (você pode ajustar este valor)
        dialogueActive = false;
    }

    // Coroutine para fechar a caixa de diálogo com atraso
    IEnumerator CloseDialogueBox(float delay)
    {
        // Aguarda o tempo especificado
        yield return new WaitForSeconds(delay);

        // O comando que faz o painel sumir (o que você estava pedindo)
        dialoguePanel.SetActive(false);

        Debug.Log("Fim da Conversa!");
    }
}