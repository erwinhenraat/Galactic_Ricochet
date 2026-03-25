using System;
using UnityEngine;

public enum InputType { 
    Mouse,
    XBox
}

public class CrosshairInput : MonoBehaviour
{
    public static event Action onPressFire1;
    public static event Action onReleaseFire1;
    public static event Action<string> onSwapControls;
    
    public static Vector3 CrosshairPosition = Vector3.zero;
    public static InputType SelectedType;

    [SerializeField] private InputType _inputType = InputType.Mouse;
    [SerializeField] private float _speed = 30f;
    
    private int _swapPressCount = 0;
    private float _swapTimer = 0f;
    private bool _swapActive = false;

    private void Start()
    {
        Cursor.visible = false;
        CrosshairInput.SelectedType = _inputType;
    }


    // Update is called once per frame
    private void Update()
    {
        SwapInput();
        HandleInput();       
    }
    private void HandleInput() {
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
                Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * Time.deltaTime * _speed;
                transform.position += movement;
                //knockback from edge
                Vector2 posInViewport = Camera.main.WorldToViewportPoint(transform.position);
                if (posInViewport.x < -0.1f) transform.position -= movement;
                if (posInViewport.y < -0.1f) transform.position -= movement;
                if (posInViewport.x > 1.1f) transform.position -= movement;
                if (posInViewport.y > 1.1f) transform.position -= movement;

                CrosshairInput.CrosshairPosition = transform.position;
                if (Input.GetButtonDown("Fire1")) onPressFire1?.Invoke();
                if (Input.GetButtonUp("Fire1")) onReleaseFire1?.Invoke();
                break;
        }
    }
    private void SwapInput() {
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



