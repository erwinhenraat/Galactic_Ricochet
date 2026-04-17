using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    //events
    //public static event Action onSave;
    //public static event Action onLoad;
    //serializable objects
    [SerializeField] private string _bumperLayer;
    //privates
    private string _savePath;
    
    void Awake()
    {
        //get path to save
        _savePath = Application.dataPath + "/save.json";
    }

    /*void Start()
    {
        onLoad += LoadButton;
        onSave += Save;
    }

    void OnDisable()
    {
        onLoad -= LoadButton;
        onSave -= Save;
    }
    */
    //save button
    public void TriggerSave()
    {
        Save();
    }
    //load button
    public void TriggerLoad()
    {
        LoadButton();
    }

    void Save()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        //find bumperlayer
        int playAreaLayer = LayerMask.NameToLayer(_bumperLayer);

        SaveFile saveFile = new SaveFile();
        //sort based on layer
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
        //write json file
        string json = JsonUtility.ToJson(saveFile, true);
        File.WriteAllText(_savePath, json);

        Debug.Log("Saved to:" + _savePath);
    }

    void LoadButton()
    {
        //input
        //GameManager.loadFile = Resources.Load<TextAsset>(savePath);
        //Debug.Log(savePath);
        //load scene
        SceneManager.LoadScene("Loaded_Scene");
    }
}