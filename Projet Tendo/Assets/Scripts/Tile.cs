using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager _gameManager;
    private int _index;
    //private Texture2DArray _texture2D;

    public void Instantiate(int index)
    {
        _index = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
