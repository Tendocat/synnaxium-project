using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab;

    private int _nbRow = 3;
    private int _nbCol = 3;
    private GameObject[,] _grid;
    private Sprite _sprite;

    // Start is called before the first frame update
    void Start()
    {
        /** Initialisation **/
        // TODO set sprite en fonction de l'OS

        _grid = new GameObject[3,3];
        for (int i = 0; i < _nbRow; i++)
            for (int j = 0; i < _nbCol; j++)
            {
                _grid[i,j] = Instantiate(TilePrefab);
                _grid[i, j].GetComponent<Tile>();
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
