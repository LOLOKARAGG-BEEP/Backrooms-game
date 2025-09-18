using UnityEngine;

public class ExitEffectsManager : MonoBehaviour
{
    [Header("Objects")]
    public Transform player;
    public Transform exit;

    [Header("Camera Scripts")]
    public CameraShake cameraShake;
    public ScreenGlitch screenGlitch;

    [Header("Settings")]
    public float triggerDistance = 10f; 
    public float maxShake = 0.3f;       

    void Update()
    {
        float dist = Vector3.Distance(player.position, exit.position);

        if (dist < triggerDistance)
        {
            float t = 1f - (dist / triggerDistance); 

           
            cameraShake.StartShake(0.1f, maxShake * t);

            
            if (Random.value < 0.1f * t) 
                screenGlitch.TriggerGlitch(t);
        }
    }
}
