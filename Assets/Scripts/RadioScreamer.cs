using UnityEngine;

public class RadioScreamer : MonoBehaviour, IPickUp
{
    public GameObject screamerObject;      
    public AudioClip screamSound;          
    public AudioSource radioAudioSource;   
    public float screamerDuration = 2f;    
     
    private bool hasActivated = false;
    private AudioSource screamAudioSource;
    void Start()
    {
        screamerObject.SetActive(false);
        screamAudioSource = gameObject.AddComponent<AudioSource>();
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

    public bool PickUp(Transform hand)
    {        
        if (!hasActivated)
        {           
            hasActivated = true;
            TriggerScreamer();
        }
        return false;
    }

    public void Drop()
    {
       
    }
}
