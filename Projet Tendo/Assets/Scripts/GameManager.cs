using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum Direction
{
    TOP,
    BOT,
    RIGHT,
    LEFT
}

public class GameManager : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private Tile TilePrefab;
    [SerializeField] private ExplosionEffect ExplosionPrefab;
    [SerializeField] private ReturnButton ReturnButton;
    #endregion

    #region Private fields
    private int _nbRow = 3;
    private int _nbCol = 3;
    private Tile[,] _grid;
    #endregion

    #region API
    /**
     * appelé par Tile lors du drag
     */
    public void TileAction(Tile tile, Direction dir)
    {
        if (tile.Masked == true)
            return;
        Tile newTile = GetNextTile(tile, dir);
        //Debug.Log(tile.Index + ":" + ((newTile == null)? "out" : newTile.Index.ToString()));
        if (newTile == null || !newTile.Masked)
            return;
        TileSwap(tile, newTile);
        CheckWinCondition();
    }
    #endregion

    #region Unity methods
    void Awake()
    {
        Sprite[] sprite;
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.IPhonePlayer:
                sprite = Resources.LoadAll<Sprite>("Apple");
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                sprite = Resources.LoadAll<Sprite>("Windows");
                break;
            case RuntimePlatform.Android:
                sprite = Resources.LoadAll<Sprite>("Android");
                break;
            default:
                sprite = Resources.LoadAll<Sprite>("Windows");
                break;
        }

        SpriteRenderer tileSprite;
        Tile tileInit;
        int index = 0;

        /** Initialisation **/

        Transform gridContainer = GameObject.Find("GridContainer").transform;
        _grid = new Tile[_nbCol,_nbRow];
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                tileInit = Instantiate(TilePrefab, gridContainer);
                tileSprite = tileInit.GetComponent<SpriteRenderer>();
                _grid[i, j] = tileInit;
                tileInit.TileEvent += TileAction;
                tileInit.Index = index;
                tileInit.col = i;
                tileInit.row = j;
                tileSprite.sprite = sprite[j+i*3];
                tileInit.TargetPosition = new Vector3(i-1, j-1, 0);
                index++;
            }

        /** Mélange des tiles **/    //TODO refaire mieux

        tileInit = _grid[Random.Range(0,_nbCol), Random.Range(0, _nbRow)];
        Tile tmpTile;
        for (int i = 0; i < 0; i++)
        {
            tmpTile = GetNextTile(tileInit, (Direction)Random.Range(0, 4));
            TileSwap(tileInit, tmpTile);
        }
        while (tileInit.col!=_nbCol/2 || tileInit.row!=_nbRow/2)
        {
            tmpTile = GetNextTile(tileInit, (Direction)Random.Range(0, 4));
            TileSwap(tileInit, tmpTile);
        }
        tileInit.Masked = true;
        float delay = 0;
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                tileInit = _grid[i, j];
                if (!tileInit.Masked) {
                    tileInit.Spawn(delay);
                    delay += 1 / 4f;
                }
            }
        }
    #endregion

    #region Private
    /**
     * return la Tile à coté d'origin vers dir, ou null si aucune
     */
    private Tile GetNextTile(Tile origin, Direction dir)
    {
        int nextCol = origin.col;
        int nextRow = origin.row;
        switch (dir)
        {
            case Direction.TOP:
                nextRow++;
                break;
            case Direction.BOT:
                nextRow--;
                break;
            case Direction.RIGHT:
                nextCol++;
                break;
            case Direction.LEFT:
                nextCol--;
                break;
        }
        if (nextCol < 0 || nextCol >= _nbCol || nextRow < 0 || nextRow >= _nbRow)
            return null;
        return _grid[nextCol, nextRow];
    }

    /**
     * vérifie si la partie est terminée
     */
    private bool CheckWinCondition()
    {
        bool win = true;
        int lastIndex = -1;
        int index;
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                index = _grid[i, j].Index;
                if (lastIndex + 1 != index)
                    win = false;
                lastIndex = index;
            }
        if (win)
            Win();
        return win;
    }

    /**
     * lance l'animation de fin et ouvre le menu
     */
    private void Win()
    {
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                _grid[i, j].Masked = false;
            }
        ExplosionPrefab.StartExplosion();
        ReturnButton.StartAnimation();
    }

    /**
     * swap les deux Tile
     */
    private void TileSwap(Tile a, Tile b)
    {
        if (a == null || b == null)
            return;

        /* Temporary value for swap */
        int row = a.row;
        int col = a.col;
        Vector3 pos = a.TargetPosition;

        a.TargetPosition = b.TargetPosition;
        a.row = b.row;
        a.col = b.col;

        b.TargetPosition = pos;
        b.row = row;
        b.col = col;

        _grid[a.col,a.row] = a;
        _grid[b.col,b.row] = b;
    }
    #endregion
}
