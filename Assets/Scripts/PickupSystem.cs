using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 4f;
    public LayerMask interactableLayer;

    private FlashlightController heldFlashlight = null;
    private EdibleItem heldEdible = null;

    void Update()
    {
        // ➤ Подобрать (G)
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, interactableLayer))
            {
                // 1. Если это съедобное
                EdibleItem edible = hit.collider.GetComponent<EdibleItem>();
                if (edible != null && heldEdible == null && heldFlashlight == null)
                {
                    edible.PickUp(Camera.main.transform);
                    heldEdible = edible;
                    return;
                }

                // 2. Батарейка
                BatteryPickup battery = hit.collider.GetComponent<BatteryPickup>();
                if (battery != null)
                {
                    battery.PickUp();
                    return;
                }

                // 3. Фонарик
                FlashlightController fc = hit.collider.GetComponent<FlashlightController>();
                if (fc != null && heldFlashlight == null && heldEdible == null)
                {
                    fc.PickUp();
                    heldFlashlight = fc;
                    return;
                }
            }
        }

        // ➤ Выбросить (H)
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (heldFlashlight != null)
            {
                heldFlashlight.Drop();
                heldFlashlight = null;
            }

            if (heldEdible != null)
            {
                heldEdible.Drop();
                heldEdible = null;
            }
        }

        // ➤ Использовать предмет (ЛКМ / Mouse0)
        if (Input.GetMouseButtonDown(0))
        {
            if (heldFlashlight != null)
            {
                heldFlashlight.ToggleFlashlight(); // включение фонаря
            }
            else if (heldEdible != null)
            {
                heldEdible.Use(); // съесть
                heldEdible = null;
            }
        }
    }
}

