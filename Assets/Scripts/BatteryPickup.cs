using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float batteryAmount = 30f;
    public AudioClip pickupSound;

    public void PickUp()
    {
        FlashlightController flashlight = Camera.main.GetComponentInChildren<FlashlightController>();
        if (flashlight != null)
        {
            flashlight.AddBattery(batteryAmount);

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
