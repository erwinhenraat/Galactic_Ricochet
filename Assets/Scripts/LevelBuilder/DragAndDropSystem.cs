using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DragAndDropSystem : MonoBehaviour
{
    //public static event Action onGameObjectInstanced;
    //serialized privates
    [SerializeField] private float _castDistance;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private LayerMask _bumperLayer;
    [SerializeField] private GameObject _bumperPrefab;
    
    //privates
    private SpriteRenderer _newBumperSpriteRenderer;
    private RaycastHit2D _hit;
    private Transform _newBumper;
    private bool _attached;
    private float _timeElapsed;
    private bool _touching;
    private string _color;
    //get setters
    public float TimeElapsed
    {
        set { _timeElapsed = value; }
    }
    public bool Attached
    {
        get { return _attached; }
    }
    public SpriteRenderer newBumperSpriteRenderer
    {
        get { return _newBumperSpriteRenderer; }
    }
    public LayerMask bumperLayer
    {
        get { return _bumperLayer; }
    }
    public Transform newBumper
    {
        get { return _newBumper; }
    }
    private void Start()
    {
        //check if it's in editormode
        if (CrosshairInput.PlayOrEditorMode != GameStateMachine.Editor) return;
        //subscribing to events
        CrosshairInput.onPressFire1 += DraggingCrosshair;
        CrosshairInput.onReleaseFire1 += DroppingCrosshair;
        BumpersNotBeingDragged.onBumpersTouching += ChangeColor;
        _newBumperSpriteRenderer = null;
        _newBumper = null;
        _attached = false;
    }
    private void OnDisable()
    {
        CrosshairInput.onPressFire1 -= DraggingCrosshair;
        CrosshairInput.onReleaseFire1 -= DroppingCrosshair;
        BumpersNotBeingDragged.onBumpersTouching -= ChangeColor;
    }
    private void Update()
    {
        if (CrosshairInput.PlayOrEditorMode != GameStateMachine.Editor) return;
        //timer (it gets resets by the bumpers)
        _timeElapsed += Time.deltaTime;
        //if crosshair is attached to an object change bumper pos and color after being in range of the other bumpers
        if (_attached)
        {
            _newBumper.position = CrosshairInput.CrosshairPosition;
            if(_timeElapsed > 0.1f)
            {
                ChangeColor(false);
            }
            return;
        }
        //only check for hits if not attached
        _hit = Physics2D.BoxCast(transform.position, _boxSize, 0, -transform.up, _castDistance, _bumperLayer);
    }
    private void DraggingCrosshair()
    {
        // on click attach the gameobject and give variables the right values
        if (!_attached && _hit)
        {
            _newBumper = _hit.transform;
            if (_newBumper.tag != "BumperInstance")
            {
                _newBumper.GetComponent<BumpersNotBeingDragged>().enabled = false;
                _color = _newBumper.tag;
            }
            else
                _color = _newBumper.GetComponent<BumperInstances>().BumperColor;
            _newBumperSpriteRenderer = _newBumper.gameObject.GetComponent<SpriteRenderer>();
            _attached = true;
        }
    }
    private void DroppingCrosshair()
    {
        //no bumper attached = no problem
        if (_newBumper == null) return;
        //if touching another object at release
        if (_touching)
        {
            Debug.Log("Please place somewhere else!");
            //play error sound

            //bumperInstances dont have a push distance
            if (_newBumper.tag == "BumperInstance")
                _newBumper.position = _newBumper.GetComponent<BumperInstances>().StartingPosition;
            else
                _newBumper.position = _newBumper.GetComponent<BumpersNotBeingDragged>().StartingPosition;

            //change color after drop
            ChangeColor(false);

            //reset variables
            _newBumperSpriteRenderer = null;
            _newBumper = null;
            _attached = false;
            return;
        }
        if (_newBumper.tag == "BumperInstance")
        {
            //get the component so that you dont have to repeat yourself too much
            BumperInstances tempBumperInstance = _newBumper.GetComponent<BumperInstances>();
            //instance and setting the bumper up
            GameObject tempBumper = Instantiate(_bumperPrefab, _newBumper.position, Quaternion.identity);
            tempBumper.tag = tempBumperInstance.BumperColor;
            tempBumper.GetComponent<BumpersNotBeingDragged>().enabled = true;

            //bring the instanciater back to the start
            _newBumper.position = tempBumperInstance.StartingPosition;
            //onGameObjectInstanced?.Invoke();

            //reset variables
            _newBumperSpriteRenderer = null;
            _newBumper = null;
            _attached = false;
            return;
        }
        //a temp would probably be better and setting the variables back correctly on the bumper
        _newBumper.GetComponent<BumpersNotBeingDragged>().enabled = true;
        _newBumper.GetComponent<BumpersNotBeingDragged>().StartingPosition = _newBumper.position;

        //reset variables
        _newBumperSpriteRenderer = null;
        _newBumper = null;
        _attached = false;
    }
    //raycast in editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _castDistance, _boxSize);
    }
    private void ChangeColor(bool touching)
    {
        //if in a bumpers treshold set the bool and color to give user feedback
        if (touching)
        {
            _newBumperSpriteRenderer.color = new Color(1, 1, 1, 0.5f);
            _touching = true;
        }
        //set the bumper color correctly after leaving bumper theshold and tell the dropping they arent touching
        else 
        { 
            switch (_color)
            {
                case "RedCombo":
                    _newBumperSpriteRenderer.color = new Color32(255, 0, 0, 255);
                    break;

                case "CyanCombo":
                    _newBumperSpriteRenderer.color = new Color32(0, 255, 250, 255);
                    break;

                case "BlueCombo":
                    _newBumperSpriteRenderer.color = new Color32(36, 112, 255, 255);
                    break;

                case "YellowCombo":
                    _newBumperSpriteRenderer.color = new Color32(242, 255, 0, 255);
                    break;

                case "PinkCombo":
                    _newBumperSpriteRenderer.color = new Color32(255, 132, 232, 255);
                    break;

                default:
                    _newBumperSpriteRenderer.color = Color.white;
                    break;
            }
            _touching = false; 
        }
    }
}
