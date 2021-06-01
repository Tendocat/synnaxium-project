using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Direction
{
    TOP,
    BOT,
    RIGHT,
    LEFT
}

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject ReturnButton;
    public float Scale = 1;

    private int _nbRow = 3;
    private int _nbCol = 3;

    private GameObject[,] _grid;
    private Sprite _sprite;

    // Start is called before the first frame update
    void Start()
    {
        // TODO set sprite en fonction de l'OS
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.IPhonePlayer:
                _sprite = Resources.Load<Sprite>("Apple");
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                _sprite = Resources.Load<Sprite>("Windows");
                break;
            case RuntimePlatform.Android:
                _sprite = Resources.Load<Sprite>("Android");
                break;
        }

        Rect cropRect = _sprite.rect;
        Image tileSprite;
        Tile tileScript;
        float xStep = _sprite.rect.width/ _nbCol;
        float yStep = _sprite.rect.height/ _nbRow;
        int index = 0;

        /** Initialisation **/

        Transform gridContainer = GameObject.Find("GridContainer").transform;
        _grid = new GameObject[_nbCol,_nbRow];
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                _grid[i,j] = Instantiate(TilePrefab, gridContainer);
                tileSprite = _grid[i, j].GetComponent<Image>();
                tileScript = _grid[i, j].GetComponent<Tile>();
                tileScript.Index = index++;
                tileScript.col = i;
                tileScript.row = j;

                cropRect.x = i * xStep;
                cropRect.y = j * yStep;
                cropRect.xMax = (i+1) * xStep;
                cropRect.yMax = (j+1) * yStep;
                tileSprite.sprite = Sprite.Create(_sprite.texture, cropRect, new Vector2(0,1), 100);
                _grid[i, j].transform.position = new Vector3(i*3-3, j*3-3, 0);
            }

        /** Mélange des tiles **/

        tileScript = _grid[Random.Range(0,_nbCol), Random.Range(0, _nbRow)].GetComponent<Tile>();
        Tile tmpTile;
        for (int i = 0; i < 100; i++)
        {
            tmpTile = GetNextTile(tileScript, (Direction)Random.Range(0, 4));
            TileSwap(tileScript, tmpTile);
        }
        while (tileScript.col!=_nbCol/2 || tileScript.row!=_nbRow/2)    //TODO refaire à la main
        {
            tmpTile = GetNextTile(tileScript, (Direction)Random.Range(0, 4));
            TileSwap(tileScript, tmpTile);
        }
        tileScript.Masked = true;
    }

    /**
     * appelé par Tile lors du drag
     */
    public void TileEvent(Tile tile, Direction dir)
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
        return _grid[nextCol, nextRow].GetComponent<Tile>();
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
                index = _grid[i, j].GetComponent<Tile>().Index;
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
                _grid[i, j].GetComponent<Tile>().Masked = false;
            }
        ReturnButton.SetActive(true);
        //SceneManager.LoadScene("Menu principal");
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
        Vector3 pos = a.transform.position;

        a.transform.position = b.transform.position;
        a.row = b.row;
        a.col = b.col;

        b.transform.position = pos;
        b.row = row;
        b.col = col;

        _grid[a.col,a.row] = a.gameObject;
        _grid[b.col,b.row] = b.gameObject;
    }
}
