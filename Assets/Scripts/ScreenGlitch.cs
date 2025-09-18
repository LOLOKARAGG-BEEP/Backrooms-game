using UnityEngine;

public class ScreenGlitch : MonoBehaviour
{
    private Vector3 originalPos;
    private bool isGlitching = false;

    public float glitchPower = 0.3f;    
    public float glitchDuration = 0.05f; 

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void TriggerGlitch(float intensity)
    {
        if (!isGlitching)
            StartCoroutine(DoGlitch(intensity));
    }

    private System.Collections.IEnumerator DoGlitch(float intensity)
    {
        isGlitching = true;

        Vector3 offset = new Vector3(
            Random.Range(-glitchPower, glitchPower) * intensity,
            Random.Range(-glitchPower, glitchPower) * intensity,
            0);

        transform.localPosition += offset;

        yield return new WaitForSeconds(glitchDuration);

        transform.localPosition = originalPos;
        isGlitching = false;
    }
}
