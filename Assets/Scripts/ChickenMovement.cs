using UnityEngine;

public class ChickenMovement : MonoBehaviour
{
    public float escapeSpeed = 0.5f; // Velocidade da fuga

    void Update()
    {
        // A galinha sempre se move lentamente para cima
        transform.Translate(Vector3.up * escapeSpeed * Time.deltaTime);
    }
}