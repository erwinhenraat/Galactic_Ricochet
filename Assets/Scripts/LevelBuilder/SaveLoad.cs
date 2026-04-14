using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SavedObject
{
    public string tag;
    public int layer;
    public Vector3 position;
}

[Serializable]
public class SaveFile
{
    public List<SavedObject> objects = new List<SavedObject>();
}

public class SaveLoad : MonoBehaviour
{
    public static event Action onSave;
    public static event Action onLoad;

    [SerializeField] private GameObject prefab;
    [SerializeField] private TextAsset loadFile;
    [SerializeField] private string _bumperLayer;

    private string savePath;

    void Awake()
    {
        savePath = Application.dataPath + "/save.json";
    }

    void Start()
    {
        onSave += Save;
        onLoad += Load;
    }

    void OnDisable()
    {
        onSave -= Save;
        onLoad -= Load;
    }

    public void TriggerSave()
    {
        onSave?.Invoke();
    }

    public void TriggerLoad()
    {
        onLoad?.Invoke();
    }

    void Save()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int playAreaLayer = LayerMask.NameToLayer(_bumperLayer);

        SaveFile saveFile = new SaveFile();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer != playAreaLayer)
                continue;
            if (obj.tag == "BumperInstance")
                continue;
            SavedObject data = new SavedObject();
            data.tag = obj.tag;
            data.layer = obj.layer;
            data.position = obj.transform.position;

            saveFile.objects.Add(data);
        }

        string json = JsonUtility.ToJson(saveFile, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved to:" + savePath);
    }

    void Load()
    {
        if (loadFile == null)
        {
            Debug.LogWarning("No load file assigned.");
            return;
        }

        string json = loadFile.text;
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(json);

        foreach (SavedObject data in saveFile.objects)
        {
            GameObject obj = Instantiate(prefab);
            obj.tag = data.tag;
            obj.layer = data.layer;
            obj.transform.position = data.position;
        }

        Debug.Log("Loaded");
    }
}