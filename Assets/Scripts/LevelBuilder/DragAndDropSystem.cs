using System.Collections.Generic;
using UnityEngine;

public class DragAndDropSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _bumperLayer;

    private Transform _newBumper;
    private bool _attached;

    private void Start()
    {
        CrosshairInput.onPressFire1 += DraggingCrosshair;
        CrosshairInput.onReleaseFire1 += DroppingCrosshair;
    }
    private void DraggingCrosshair()
    {
        if (!_attached)
        {
            _newBumper = GetClosestBumper(GameObject.FindObjectsOfType<GameObject>());
            _attached = true;
        }
        if (_newBumper == null)return;
        _newBumper.position = CrosshairInput.CrosshairPosition;
    }
    private void DroppingCrosshair()
    {

    }
    private Transform GetClosestBumper(GameObject[] gameObjects)
    {
        List<Transform> bumpers = new List<Transform>();
        SaveFile saveFile = new SaveFile();

        foreach (GameObject bumper in gameObjects)
        {
                if (bumper.layer != _bumperLayer)
                    continue;
            bumpers.Add(gameObject.transform);
        }
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = CrosshairInput.CrosshairPosition;
        foreach (Transform potentialTarget in bumpers)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
