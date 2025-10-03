using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gameObjects = new List<GameObject>();  
    public static GameObject ItemInHand;
    [SerializeField]
    private GameObject player;


    private void Start()
    {
        LoadGame();
    }

    private void OnDestroy()
    {
       // SaveGame();
    }
    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.SceneId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        data.PlayerPosition = player.transform.position;
        // data.PlayerStability = Player.Instance.Stability;          
        foreach (var item in gameObjects)
        {
            if (!item.activeSelf)
            {
                data.DeActivItem.Add(item.name);
            }
        }
        if (ItemInHand != null)
        {
            data.ItemInHand = ItemInHand.name;
        }
        else
        {
            data.ItemInHand = "";
        }
        SaveSystemJson.SaveDataToFile(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystemJson.LoadDataFromFile();
        if (data.SceneId == -1)
        {
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(data.SceneId);
        player.transform.position = data.PlayerPosition;
        // Player.Instance.Stability = data.PlayerStability;        
        foreach (var item in gameObjects)
        {
            if (data.DeActivItem.Contains(item.name))
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
        if (data.ItemInHand != "")
        {
           // ItemInHand = GameObject.Find(data.ItemInHand);
        }        
    }
}
