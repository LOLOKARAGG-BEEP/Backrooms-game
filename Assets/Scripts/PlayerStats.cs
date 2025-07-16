using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float stability = 100f;
    public float maxStability = 100f;

    public void RestoreStability(float amount)
    {
        stability = Mathf.Clamp(stability + amount, 0f, maxStability);
        Debug.Log("Стабильность восстановлена: " + stability);
    }
}
