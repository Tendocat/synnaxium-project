using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private GameManager _gameManager;
    private int _index;
    private bool _masked = false;
    private Vector2 _originalMousePosition = Vector2.zero;
    private bool _hasDrag = true;

    public int col, row;
    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            Text text = GetComponentInChildren<Text>();
            text.text = _index.ToString();
        }
    }
    public bool Masked
    {
        get => _masked;
        set
        {
            _masked = value;
                Image i = GetComponentInChildren<Image>();
            if (_masked)
                i.CrossFadeColor(new Color(0, 0, 0, 0), 1f, false, true);
            //i.color = new Color(0, 0, 0, 0);
            else
                i.CrossFadeColor(new Color(0, 0, 0, 1), 1f, false, true);
        }
    }

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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
        //Debug.Log(mouseDirection + " " + dir);
        _gameManager.TileEvent(this, dir);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_hasDrag)
            return;
        _hasDrag = false;
        _originalMousePosition = eventData.position;
    }
}
