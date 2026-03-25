using System;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropSystem : MonoBehaviour
{
    //public static event Action onGameObjectInstanced;
    [SerializeField] private float _castDistance;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private LayerMask _bumperLayer;
    [SerializeField] private GameObject _bumperPrefab;

    private SpriteRenderer _newBumperSpriteRenderer;
    private RaycastHit2D _hit;
    private Transform _newBumper;
    private bool _attached;
    private void Start()
    {
        CrosshairInput.onPressFire1 += DraggingCrosshair;
        CrosshairInput.onReleaseFire1 += DroppingCrosshair;
    }
    private void Update()
    {
        _hit = Physics2D.BoxCast(transform.position, _boxSize, 0, -transform.up, _castDistance, _bumperLayer);
        if (_attached)
        {
            _newBumper.position = CrosshairInput.CrosshairPosition;
            if(_hit.transform != _newBumper)
            {
                _newBumperSpriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }
        }
    }
    private void DraggingCrosshair()
    {
        if (!_attached && _hit)
        {
            _newBumper = _hit.transform;
            _newBumperSpriteRenderer = _newBumper.gameObject.GetComponent<SpriteRenderer>();
            _attached = true;
        }
    }
    private void DroppingCrosshair()
    {
        if (_newBumper == null) return;
        if (_newBumper.tag == "BumperInstance")
        {
            BumperInstances tempBumperInstance = _newBumper.GetComponent<BumperInstances>();
            GameObject tempBumper = Instantiate(_bumperPrefab, _newBumper.position, Quaternion.identity);
            tempBumper.tag = tempBumperInstance.BumperColor;
            _newBumper.position = tempBumperInstance.startingPosition;
            //onGameObjectInstanced?.Invoke();
            _attached = false;
            return;
        }
        _attached = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _castDistance, _boxSize);
    }
}
