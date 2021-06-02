﻿using System.Collections;
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
    private bool _hasDrag = true;
    #endregion

    #region Public fields
    public Action<Tile, Direction> TileEvent;
    public Vector3 TargetPosition;
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
            SpriteRenderer i = GetComponent<SpriteRenderer>();
            if (_masked)
                i.color = new Color(255, 255, 255, 0);
            else
                i.color = new Color(255, 255, 255, 1);
        }
    }
    #endregion

    #region API
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
        //Debug.Log(mouseDirection + " " + dir);
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
        TargetPosition = transform.position;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.1f * Time.deltaTime);
    }
    #endregion
}
