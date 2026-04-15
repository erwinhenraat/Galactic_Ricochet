
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [Header("Menu Change")]
    [Tooltip("The activation list sets which game objects get disabled or enabled")]
    [SerializeField] private List<bool> activationList = new List<bool>();
    [SerializeField] private List<GameObject> objectList = new List<GameObject>();

#if UNITY_EDITOR
    private void Start()
    {
        if (activationList.Count != objectList.Count)
            Debug.LogError("Activation List and Object List are not the same size!", this);
        else if (activationList.Count == 0)
            Debug.LogWarning("Menu change script has empty lists.", this);
    }
#endif

    // Not to be used inside other scripts directly
    public void MenuChange()
    {
        for (int i = 0; i < objectList.Count; i++) {
            objectList[i].SetActive(activationList[i]);
        }
    }
}
