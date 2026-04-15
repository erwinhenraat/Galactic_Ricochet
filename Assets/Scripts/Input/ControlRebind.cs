using UnityEngine;
using UnityEngine.InputSystem;

public class ControlRebind : MonoBehaviour
{
    void RemapButtonClicked(InputAction actionToRebind)
    {
        var rebindOperation = actionToRebind.PerformInteractiveRebinding()
        // To avoid accidental input from mouse motion
        .WithControlsExcluding("Mouse")
        .OnMatchWaitForAnother(0.1f)
        .Start();
    }
}
