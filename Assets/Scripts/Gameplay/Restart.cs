using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public static event Action<string> onRestart;
    private TMP_Text textfield;
    [SerializeField]private TMP_Text titleField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
      
        textfield = GetComponent<TMP_Text>();
        textfield.enabled = false;

        if (titleField != null) titleField.enabled = false;


        GameManager.onReadyToRestart += ActivateRestart;
        CrosshairInput.onPressFire1 += HandleFire;
      

        onRestart?.Invoke("GO!");
       
    }
    private void OnDisable()
    {
        GameManager.onReadyToRestart -= ActivateRestart;
        CrosshairInput.onPressFire1 -= HandleFire;        
      
    } 
    private void ActivateRestart() {
        //gameObject.SetActive(true);
        textfield.enabled = true;
        if (titleField != null)titleField.enabled = true;
    }

    private void HandleFire() {
        if (textfield.enabled)
        {
            textfield.enabled = false;
            if (titleField != null) titleField.enabled = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
