using UnityEngine;

public class PickupFlashlight : MonoBehaviour
{
    public float pickupRange = 4f;
    public LayerMask interactableLayer;

    private FlashlightController heldFlashlight = null;

    void Update()
    {
        // Подбор предметов на G
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                Debug.Log("Hit: " + hit.collider.name);

                // Сначала батарейка
                BatteryPickup battery = hit.collider.GetComponent<BatteryPickup>();
                if (battery != null)
                {
                    Debug.Log("Picked up battery!");
                    battery.PickUp();
                    return;
                }

                // Затем фонарик — только если у тебя ещё нет фонарика
                if (heldFlashlight == null)
                {
                    FlashlightController fc = hit.collider.GetComponent<FlashlightController>();
                    if (fc != null)
                    {
                        Debug.Log("Picked up flashlight!");
                        fc.PickUp();
                        heldFlashlight = fc;
                        return;
                    }
                }
                else
                {
                    Debug.Log("Фонарик уже в руке. Нажми H, чтобы выбросить.");
                }
            }
            else
            {
                Debug.Log("Raycast didn't hit anything");
            }
        }

        // Выбросить фонарик на H
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (heldFlashlight != null && heldFlashlight.IsHeld())
            {
                Debug.Log("Dropped flashlight!");
                heldFlashlight.Drop();
                heldFlashlight = null;
            }
        }
    }
}
