using UnityEngine;

// Torna a classe visível no Inspector da Unity
[System.Serializable]
public class Dialogue
{
    // O nome do personagem que está falando
    public string name;

    // Onde você colocará as frases do diálogo.
    // [TextArea] permite múltiplas linhas no Inspector para facilitar a escrita.
    [TextArea(3, 10)]
    public string[] sentences;
}
