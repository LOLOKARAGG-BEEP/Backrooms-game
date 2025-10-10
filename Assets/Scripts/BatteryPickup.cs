using UnityEngine;

public class BatteryPickup : MonoBehaviour, IPickUp
{
    public float batteryAmount = 30f;
    public AudioClip pickupSound;

    public bool PickUp(Transform hand)
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
        return false;
    }
    public void Drop()
    {
        // Batteries cannot be dropped once picked up
    }
}
