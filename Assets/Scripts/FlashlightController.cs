using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlightLight;
    public float batteryLife = 100f;
    public float batteryDrainRate = 5f;
    public float blinkThreshold = 15f;
    public float blinkInterval = 0.3f;

    public AudioClip toggleSound;
    private AudioSource audioSource;

    private bool isHeld = false;
    private bool isOn = false;
    private float blinkTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isHeld)
        {
     
            if (Input.GetMouseButtonDown(0))
            {
                ToggleFlashlight();
            }

            if (isOn)
            {
                DrainBattery();
            }

            if (isOn && batteryLife <= blinkThreshold)
            {
                BlinkFlashlight();
            }
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;

        if (toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
    }

    void DrainBattery()
    {
        batteryLife -= batteryDrainRate * Time.deltaTime;

        if (batteryLife <= 0)
        {
            batteryLife = 0;
            isOn = false;
            flashlightLight.enabled = false;
        }
    }

    void BlinkFlashlight()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= blinkInterval)
        {
            flashlightLight.enabled = !flashlightLight.enabled;
            blinkTimer = 0f;
        }
    }

    public void PickUp()
    {
        isHeld = true;
        transform.SetParent(Camera.main.transform);
        transform.localPosition = new Vector3(0.4f, -0.5f, 1.0f);
        transform.localRotation = Quaternion.Euler(0f, -86f, 0f);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void Drop()
    {
        isHeld = false;
        isOn = false;
        flashlightLight.enabled = false;

        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;


        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
    }

    public bool IsHeld()
    {
        return isHeld;
    }

    public void AddBattery(float amount)
    {
        batteryLife = Mathf.Clamp(batteryLife + amount, 0, 100f);
    }
}
