using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTransition : MonoBehaviour
{
    public string nextLevelName = "Lvl 2";
    public Transform doorPivot;            
    public float openAngle = 90f;          
    public float openTime = 1.2f;         
    public float delayBeforeLoad = 1f;     

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
            StartCoroutine(OpenAndLoad());
    }

    private IEnumerator OpenAndLoad()
    {
        isOpen = true;

        if (doorPivot != null)
        {
            Quaternion startRot = doorPivot.localRotation;
            Quaternion endRot = startRot * Quaternion.Euler(0f, openAngle, 0f);

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / openTime;
                doorPivot.localRotation = Quaternion.Slerp(startRot, endRot, t);
                yield return null;
            }
        }

        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(nextLevelName);
    }
}
