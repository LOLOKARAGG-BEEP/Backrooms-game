using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveSystemJson
{
    // Шлях до файлу збереження
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    // Метод збереження даних у файл
    public static void SaveDataToFile(SaveData data)
    {
        // Конвертуємо об'єкт у JSON-рядок
        string json = JsonUtility.ToJson(data, true);

        // Записуємо у файл
        File.WriteAllText(SavePath, json);

        Debug.Log("Збережено у: " + SavePath);
    }

    // Метод завантаження даних з файлу
    public static SaveData LoadDataFromFile()
    {
        // Якщо файл не існує — повертаємо нові дані за замовчуванням
        if (!File.Exists(SavePath))
        {
            Debug.Log("Файл збереження не знайдено. Створено нові дані.");
            return new SaveData();
        }

        // Читаємо JSON з файлу
        string json = File.ReadAllText(SavePath);

        // Конвертуємо JSON у об'єкт SaveData
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("Дані завантажено.");
        return data;
    }
}
