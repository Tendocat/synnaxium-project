using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

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
    public float Scale = 1;

    private int _nbRow = 3;
    private int _nbCol = 3;

    private GameObject[,] _grid;
    public Sprite _sprite;

    // Start is called before the first frame update
    void Start()
    {
        /** Initialisation **/
        // TODO set sprite en fonction de l'OS

        Rect cropRect = _sprite.rect;
        Image tileSprite;
        Tile tileScript;
        float xStep = _sprite.rect.width/ _nbCol;
        float yStep = _sprite.rect.height/ _nbRow;
        int index = 0;

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
        
        //TODO mix puis disable 1
    }

    /**
     * appelé par Tile lors du drag
     */
    public void TileEvent(Tile tile, Direction dir)
    {
        Tile newTile;
        // TODO CheckMoove & get the other tile
        Debug.Log(tile.Index + ":" + (((newTile=GetNextTile(tile, dir)) == null)? "out" : newTile.Index.ToString()));
        
    }
    /**
     * return the Tile next to origin toward dir or null if no any
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
        return false;   //TODO
    }

    /**
     * swap les deux Tile
     */
    private void TileMoove(Tile a, Tile b)
    {
        // TODO
    }
}
