using UnityEngine;

public class BumperInstances : MonoBehaviour
{
    [SerializeField] private string _bumperColor;
    private Vector3 _startingTransform;

    private void Start()
    {
        //DragAndDropSystem.onGameObjectInstanced += BackToStartingPos;
        _startingTransform = transform.position;
    }

    /*private void BackToStartingPos()
    {
        transform.position = startingTransform.position;
    }*/
    public Vector3 startingPosition
    {
        get { return _startingTransform; }
    }

    public string BumperColor
    {
        get { return _bumperColor; }
    }
}
