using System;
using System.Collections.Generic;
using UnityEngine;

// Клас, який описує дані для збереження
[Serializable]
public class SaveData
{
    public int SceneId = -1;
    public Vector3 PlayerPosition = Vector3.zero;
    public int PlayerStability = 100;
    public List<string> DeActivItem = new List<string>();
    public string ItemInHand = "";

}
