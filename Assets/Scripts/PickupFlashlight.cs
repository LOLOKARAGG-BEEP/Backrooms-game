using UnityEngine;

public class PickupFlashlight : MonoBehaviour
{
    public float pickupRange = 4f;
    public LayerMask interactableLayer;

    private FlashlightController heldFlashlight = null;

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                BatteryPickup battery = hit.collider.GetComponent<BatteryPickup>();
                if (battery != null)
                {
                    battery.PickUp();
                    return;
                }

                if (heldFlashlight == null)
                {
                    FlashlightController fc = hit.collider.GetComponent<FlashlightController>();
                    if (fc != null)
                    {
                        fc.PickUp();
                        heldFlashlight = fc;
                        return;
                    }
                }
            }
        }

       
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (heldFlashlight != null && heldFlashlight.IsHeld())
            {
                heldFlashlight.Drop();
                heldFlashlight = null;
            }
        }
    }
}
