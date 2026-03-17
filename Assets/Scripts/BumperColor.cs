using UnityEngine;

public class BumperColor : MonoBehaviour
{
     void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        switch (gameObject.tag)
        {
            case "RedCombo":
                sr.color = Color.red;
                break;

            case "CyanCombo":
                sr.color = Color.cyan;
                break;

            case "BlueCombo":
                sr.color = Color.blue;
                break;

            case "YellowCombo": 
                sr.color = Color.yellow;
                break;

            case "PinkCombo":
                sr.color = Color.magenta;
                break;

            case "GreenCombo":
                sr.color = Color.green;
                break;

            default:
                Debug.LogError("Assign color tags to bumper or remove this script.");
                break;
        }
    }
}