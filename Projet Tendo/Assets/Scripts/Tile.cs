using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    #region Private fields
    private int _index;
    private bool _masked = false;
    private Vector2 _originalMousePosition = Vector2.zero;
    private Vector3 _targetPosition;
    private bool _hasDrag = true;
    private SpriteRenderer _sprite;
    private SlideEffect _slideEffect;
    private UpscaleEffect _upScaleEffect;
    #endregion

    #region Public fields
    public Action<Tile, Direction> TileEvent;
    public int col, row;
    public int Index
    {
        get => _index;
        set
        {
            _index = value;
        }
    }
    public bool Masked
    {
        get => _masked;
        set
        {
            _masked = value;
            if (_masked)
                _sprite.enabled = false;
            else
                _sprite.enabled = true;
        }
    }
    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set
        {
            _targetPosition = value;
            if (_slideEffect != null)
                _slideEffect.StartEffect(_targetPosition);
            else
                transform.position = _targetPosition;
        }
    }
    #endregion

    #region API
    public void Spawn(float delay)
    {
        StartCoroutine(SpawnEffect(delay));
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_hasDrag)
            return;
        _hasDrag = true;
        Direction dir = Direction.BOT;
        Vector3 mouseDirection = eventData.position - _originalMousePosition;
        if (Mathf.Abs(mouseDirection.x) < Mathf.Abs(mouseDirection.y))
        {
            if (mouseDirection.y > 0)
                dir = Direction.TOP;
            else
                dir = Direction.BOT;
        }
        else
        {
            if (mouseDirection.x > 0)
                dir = Direction.RIGHT;
            else
                dir = Direction.LEFT;
        }
        TileEvent.Invoke(this, dir);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_hasDrag)
            return;
        _hasDrag = false;
        _originalMousePosition = eventData.position;
    }
    #endregion

    #region Unity methods
    void Awake()
    {
        _upScaleEffect = GetComponent<UpscaleEffect>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _slideEffect = GetComponent<SlideEffect>();
    }
    #endregion

    #region Private
    private IEnumerator SpawnEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        _upScaleEffect.StartEffect();
        Masked = false;
    }
    #endregion
}
