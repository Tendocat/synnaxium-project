using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu principal");
    }
}
