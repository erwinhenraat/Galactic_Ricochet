using UnityEngine;

public class BumperColor : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        switch (gameObject.tag)
        {
            case "RedCombo":
                sr.color = new Color32(255, 0, 0, 255);
                break;

            case "CyanCombo":
                sr.color = new Color32(0, 255, 250, 255);
                break;

            case "BlueCombo":
                sr.color = new Color32(36, 112, 255, 255);
                break;

            case "YellowCombo":
                sr.color = new Color32(242, 255, 0, 255);
                break;

            case "PinkCombo":
                sr.color = new Color32(255, 132, 232, 255);
                break;

            default:
                Debug.LogError("Assign color tags to bumper or remove this script.");
                break;
        }
    }
}