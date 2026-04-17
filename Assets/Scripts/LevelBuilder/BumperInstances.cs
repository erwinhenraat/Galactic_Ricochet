using UnityEngine;

public class BumperInstances : MonoBehaviour
{
    [SerializeField] private string _bumperColor;
    private Vector3 _startingTransform;

    private void Start()
    {
        //DragAndDropSystem.onGameObjectInstanced += BackToStartingPos;
        //set starting pos
        _startingTransform = transform.position;
    }

    /*private void BackToStartingPos()
    {
        transform.position = startingTransform.position;
    }*/
    //make sure staring position is correct so no setter
    public Vector3 StartingPosition
    {
        get { return _startingTransform; }
    }

    // to give tag to instance
    public string BumperColor
    {
        get { return _bumperColor; }
    }
}
