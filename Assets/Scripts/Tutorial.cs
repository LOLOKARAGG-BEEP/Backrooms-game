using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Text hintText;           
    public float displayTime = 30f;
    private float timer;
    private bool isActive = true;

    void Start()
    {
        // Текст підказок
        hintText.text =
            " Керування:\n\n" +
            "ПКМ — наблизити камеру\n" +
            "G — підняти предмет\n" +
            "H — викинути предмет\n" +
            "ЛКМ — увімкнути / вимкнути ліхтарик\n" +
            "ЛКМ (з предметом) — використати предмет";

        timer = displayTime;
    }

    void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            hintText.enabled = false;
            isActive = false;
        }
    }
}
