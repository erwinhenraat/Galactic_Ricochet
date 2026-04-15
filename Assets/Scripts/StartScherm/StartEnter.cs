using Unity.VisualScripting;
using UnityEngine;

public class StartEnter : MonoBehaviour
{
    [SerializeField] private GameObject levelMenu;

    private float timer = 0f;
    private CanvasGroup CanvasGroup;

    void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();

        levelMenu.SetActive(false);
        gameObject.SetActive(true);
    }

    void Update()
    {
        ToggleVisibility();
        ToggleMenu();
    }

    private void ToggleVisibility()
    {
        timer += Time.deltaTime;

        if (timer < 1) return;
        CanvasGroup.alpha = (CanvasGroup.alpha == 1f) ? 0f : 1f;
        timer = 0;
    }

    private void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetButtonDown("Fire1"))
        {
            levelMenu.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
