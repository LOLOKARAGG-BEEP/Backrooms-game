using UnityEngine;

public class FlashlightController : MonoBehaviour, IPickUp, IUsable
{
    public Light flashlightLight;
    public float batteryLife = 100f;
    public float batteryDrainRate = 5f;
    public float blinkThreshold = 15f;
    public float blinkInterval = 0.3f;

    private bool isHeld = false;
    private bool isOn = false;
    private float blinkTimer = 0f;

    void Update()
    {
        if (!isHeld) return;

        
        if (isOn)
        {
            DrainBattery();

            if (batteryLife <= blinkThreshold)
            {
                BlinkFlashlight();
            }
            else
            {
                flashlightLight.enabled = true;
            }
        }
    }

    public bool Use()
    {
        if (!isHeld) return true;

        if (batteryLife <= 0f)
        {
            flashlightLight.enabled = false;
            isOn = false;
            return true;
        }

        isOn = !isOn;
        flashlightLight.enabled = isOn;

        return true;
    }

    void DrainBattery()
    {
        batteryLife -= batteryDrainRate * Time.deltaTime;

        if (batteryLife <= 0f)
        {
            batteryLife = 0f;
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

    public bool PickUp(Transform hand)
    {
        isHeld = true;
        transform.SetParent(Camera.main.transform);
        transform.localPosition = new Vector3(0.4f, -0.5f, 1f);
        transform.localRotation = Quaternion.Euler(0f, -86f, 0f);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        return true;
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
        if (rb != null)
        {
            rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }

    public void AddBattery(float amount)
    {
        batteryLife = Mathf.Clamp(batteryLife + amount, 0f, 100f);
    }
}
