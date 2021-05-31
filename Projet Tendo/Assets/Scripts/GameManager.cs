using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SpriteRenderer tileSprite = null;
        float xStep = _sprite.rect.width/ _nbCol;
        float yStep = _sprite.rect.height/ _nbRow;

        _grid = new GameObject[_nbCol,_nbRow];
        for (int i = 0; i < _nbCol; i++)
            for (int j = 0; j < _nbRow; j++)
            {
                _grid[i,j] = Instantiate(TilePrefab);
                tileSprite = _grid[i, j].GetComponent<SpriteRenderer>();
                cropRect.x = i * xStep;
                cropRect.y = j * yStep;
                cropRect.xMax = (i+1) * xStep;
                cropRect.yMax = (j+1) * yStep;
                tileSprite.sprite = Sprite.Create(_sprite.texture, cropRect, new Vector2(0,1), 1);
                _grid[i, j].transform.localScale = new Vector3(Scale, Scale, 0);
                _grid[i, j].transform.position = new Vector3(i*xStep*Scale - Scale*xStep*_nbCol/2, j*yStep*Scale, 0);
            }
    }

    /*
     * vérifie si la partie est terminée
     */
    public bool CheckWinCondition()
    {
        return false;   //TODO
    }

    /*
     * vérifie si le coup peut être effectué
     */
    public bool CheckMoove()
    {
        return false;   //TODO
    }
}
