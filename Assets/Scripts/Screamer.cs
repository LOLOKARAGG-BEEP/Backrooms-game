using UnityEngine;

public class OneTimeFootsteps : MonoBehaviour
{
    public AudioSource footsteps; 
    public Transform player;      
    public float distanceBehind = 2f; 

    private bool triggered = false;

    void Update()
    {
        if (triggered)
        {
            footsteps.transform.position = player.position - player.forward * distanceBehind;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            footsteps.Play(); 
        }
    }
}
