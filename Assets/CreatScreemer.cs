using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatScreemer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3))
            {
                if (hit.collider != null && hit.collider.CompareTag("Useble"))
                {
                    RadioScreamer radioScreamer = hit.collider.GetComponent<RadioScreamer>();
                    if (radioScreamer != null)
                    {
                       // radioScreamer.TriggerScreamer();
                    }
                }
            }
        }
    }




}
