using System;
using UnityEngine;

public class ExtraBall : MonoBehaviour
{
    public static event Action<string> onExtraBall;
    [SerializeField] private int comboLevelReached = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Combo.onComboAchieved += ExtraBallCheck;
    }
    private void OnDisable()
    {
        Combo.onComboAchieved -= ExtraBallCheck;
    }

    private void ExtraBallCheck(int comboLevel, string _) {
        if (comboLevel == comboLevelReached) { 
            onExtraBall?.Invoke("Extra Life");
        }
    }


}