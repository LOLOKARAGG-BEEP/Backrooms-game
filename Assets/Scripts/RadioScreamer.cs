using UnityEngine;

public class RadioScreamer : MonoBehaviour
{
    public GameObject screamerObject;      
    public AudioClip screamSound;          
    public AudioSource radioAudioSource;   
    public float screamerDuration = 2f;    

    private bool playerInRange = false;
    private bool hasActivated = false;
    private AudioSource screamAudioSource;

    void Start()
    {
        screamerObject.SetActive(false);
        screamAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.G) && !hasActivated)
        {
            hasActivated = true;
            TriggerScreamer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void TriggerScreamer()
    {
        radioAudioSource.Stop();                      
        screamerObject.SetActive(true);               
        screamAudioSource.PlayOneShot(screamSound);   
        Invoke(nameof(HideScreamer), screamerDuration);
    }

    void HideScreamer()
    {
        screamerObject.SetActive(false);
    }
}
