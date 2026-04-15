using System;
using Unity.VisualScripting;
using UnityEngine;

public class BumpersNotBeingDragged : MonoBehaviour
{
    //call change color with event
    public static event Action<bool> onBumpersTouching;
    private DragAndDropSystem _dragAndDropSystem;
    private RaycastHit2D _hit;
    private Vector3 _startingTransform;
    [SerializeField] private Vector3 _borderCheck;

    //to change pos after placing
    public Vector3 StartingPosition
    {
        get { return _startingTransform; }
        set { _startingTransform = value; }
    }

    private void Start()
    {
        if (CrosshairInput.PlayOrEditorMode != GameStateMachine.Editor) this.enabled = false;
        //get the component
        _dragAndDropSystem = GameObject.Find("crosshair").GetComponent<DragAndDropSystem>();
        //set pos
        _startingTransform = transform.position;
        
    }
    private void FixedUpdate()
    {
        if (!_dragAndDropSystem.Attached) return;
        //check for overlapping objects
        _hit = Physics2D.BoxCast(transform.position, _borderCheck, 0, -transform.up, 0, _dragAndDropSystem.bumperLayer);
        //if overlapping object is not bumper being dragged return
        if (_hit.transform != _dragAndDropSystem.newBumper)return; 
        //call touching and reset color change timer
        onBumpersTouching?.Invoke(true);
        _dragAndDropSystem.TimeElapsed = 0f;
    }
    // draw threshold in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * 0, _borderCheck);
    }
}
