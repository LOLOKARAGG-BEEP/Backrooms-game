using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public float pickupRange = 4f;
    public LayerMask interactableLayer;

    private FlashlightController heldFlashlight = null;
    private EdibleItem heldEdible = null;
    private KeyPickup heldKey = null; 

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, interactableLayer))
            {
                
                EdibleItem edible = hit.collider.GetComponent<EdibleItem>();
                if (edible != null && heldEdible == null && heldFlashlight == null && heldKey == null)
                {
                    edible.PickUp(Camera.main.transform);
                    heldEdible = edible;
                    return;
                }

                BatteryPickup battery = hit.collider.GetComponent<BatteryPickup>();
                if (battery != null)
                {
                    battery.PickUp();
                    return;
                }

                KeyPickup key = hit.collider.GetComponent<KeyPickup>();
                if (key != null && heldKey == null && heldFlashlight == null && heldEdible == null)
                {
                    key.PickUp(Camera.main.transform);
                    heldKey = key;
                    return;
                }

            
                FlashlightController fc = hit.collider.GetComponent<FlashlightController>();
                if (fc != null && heldFlashlight == null && heldEdible == null && heldKey == null)
                {
                    fc.PickUp();
                    heldFlashlight = fc;
                    return;
                }
            }
        }

  
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

            if (heldKey != null)
            {
                heldKey.Drop();
                heldKey = null;
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            if (heldFlashlight != null)
            {
                heldFlashlight.ToggleFlashlight();
            }

            else if (heldEdible != null)
            {
                heldEdible.Use();
                heldEdible = null;
            }
           
            else if (heldKey != null)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, interactableLayer))
                {
                    DoorController door = hit.collider.GetComponent<DoorController>();
                    if (door != null)
                    {
                        door.OpenDoor(); 
                        Destroy(heldKey.gameObject); 
                        heldKey = null;
                    }
                }
            }
        }
    }
}
