using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum InputType
{
    Mouse,
    XBox,
    Namco
}

public enum GameStateMachine
{
    Play,
    Editor
}

public class CrosshairInput : MonoBehaviour
{
    public static event Action onPressFire1;
    public static event Action onReleaseFire1;
    public static event Action<string> onSwapControls;

    public static Vector3 CrosshairPosition = Vector3.zero;
    public static InputType SelectedType;
    public static GameStateMachine PlayOrEditorMode;

    [SerializeField] private InputType _inputType = InputType.Mouse;
    [SerializeField] private float _speed = 30f;

    private int _swapPressCount = 0;
    private float _swapTimer = 0f;
    private bool _swapActive = false;
    private Gamepad _gamepad_1;

    private void Awake()
    {
        Scene tempScene = SceneManager.GetActiveScene();
        //if editor scene changes change temp.scene
        if (tempScene.name == "Drag_And_Drop") PlayOrEditorMode = GameStateMachine.Editor;
        else PlayOrEditorMode = GameStateMachine.Play;
    }

    private void Start()
    {

        Cursor.visible = false;
        CrosshairInput.SelectedType = _inputType;

        if (CrosshairInput.SelectedType == InputType.Namco)
        {
            _gamepad_1 = Gamepad.all[0];
        }
    }


    // Update is called once per frame
    private void Update()
    {
        SwapInput();
        HandleInput();
    }
    private void HandleInput()
    {
        Vector3 movement = Vector3.zero;
        switch (_inputType)
        {

            case InputType.Mouse:
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z + transform.position.z));
                CrosshairInput.CrosshairPosition = worldPoint;
                transform.position = worldPoint;
                if (Input.GetMouseButtonDown(0)) onPressFire1?.Invoke();
                if (Input.GetMouseButtonUp(0)) onReleaseFire1?.Invoke();
                break;
            case InputType.XBox:
                movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * Time.deltaTime * _speed;
                transform.position += movement;

                //handle fire input
                if (Input.GetButtonDown("Fire1")) onPressFire1?.Invoke();
                if (Input.GetButtonUp("Fire1")) onReleaseFire1?.Invoke();
                break;

            case InputType.Namco:
                movement = new Vector3(_gamepad_1.leftStick.ReadValue().x, _gamepad_1.leftStick.ReadValue().y, 0f) * Time.deltaTime * _speed;
                transform.position += movement;

                if (_gamepad_1.buttonSouth.wasPressedThisFrame) onPressFire1?.Invoke();
                if (_gamepad_1.buttonSouth.wasReleasedThisFrame) onReleaseFire1?.Invoke();
                break;
        }
        //knockback from edge
        Vector2 posInViewport = Camera.main.WorldToViewportPoint(transform.position);
        if (posInViewport.x < -0.1f) transform.position -= movement;
        if (posInViewport.y < -0.1f) transform.position -= movement;
        if (posInViewport.x > 1.1f) transform.position -= movement;
        if (posInViewport.y > 1.1f) transform.position -= movement;

        CrosshairInput.CrosshairPosition = transform.position;
    }
    private void SwapInput()
    {
        if (_swapActive)
        {
            _swapTimer += Time.deltaTime;
            if (_swapTimer > 1f)
            {
                _swapTimer = 0f;
                _swapActive = false;
                _swapPressCount = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab) && _swapTimer < 1f)
        {
            if (!_swapActive) _swapActive = true;
            _swapTimer = 0f;
            _swapPressCount++;
            if (_swapPressCount == 3)
            {
                if (_inputType == InputType.Mouse)
                {
                    _inputType = InputType.XBox;
                    onSwapControls?.Invoke("XBox  Controller  Activated!");
                }
                else if (_inputType == InputType.XBox)
                {
                    _inputType = InputType.Mouse;
                    onSwapControls?.Invoke("Mouse  Activated!");
                }
                _swapActive = false;
                _swapPressCount = 0;
                _swapTimer = 0;


            }
        }
    }
}



