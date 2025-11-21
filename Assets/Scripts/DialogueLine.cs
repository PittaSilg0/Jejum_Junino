using UnityEngine;

// Define a estrutura de uma única fala (Nome + Frase)
[System.Serializable]
public class DialogueLine
{
    public string name;

    // O [TextArea] facilita a escrita do texto no Inspector
    [TextArea(3, 10)]
    public string sentence;
}

// Classe que contém toda a sequência de falas
// Não é um script, é apenas um container de dados
[System.Serializable]
public class Conversation
{
    public DialogueLine[] lines;
}