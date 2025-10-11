using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public float maxStability = 100f;
    public float currentStability;
    public float decreaseRate = 2f;
    public Slider stabilityBar;
    public GameObject deathScreen;

    private bool isDead = false;

    void Start()
    {
        currentStability = maxStability;
        if (stabilityBar != null)
            stabilityBar.maxValue = maxStability;

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        currentStability -= decreaseRate * Time.deltaTime;
        currentStability = Mathf.Clamp(currentStability, 0, maxStability);

        if (stabilityBar != null)
            stabilityBar.value = currentStability;

        if (currentStability <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        isDead = true;

        if (deathScreen != null)
            deathScreen.SetActive(true);

        yield return new WaitForSeconds(3f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestoreStability(float amount)
    {
        currentStability = Mathf.Min(currentStability + amount, maxStability);
        if (stabilityBar != null)
            stabilityBar.value = currentStability;
    }
}
