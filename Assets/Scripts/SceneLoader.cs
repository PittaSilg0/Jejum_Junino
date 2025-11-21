using UnityEngine;
using UnityEngine.SceneManagement; // ESSENCIAL: Importa a biblioteca de gerenciamento de cenas

public class SceneLoader : MonoBehaviour
{
    // Método público que será chamado pelo botão
    // O nome da função é claro para o propósito.
    public void LoadGameScene()
    {
        // SceneManager.LoadScene() carrega uma cena.
        SceneManager.LoadScene(1);
    }
}
