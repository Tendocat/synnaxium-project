using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
    #endregion

    #region API
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit(0);
    }
    #endregion

    #region Unity methods
    private void Start()
    {
        PlayButton.onClick.AddListener(Play);
        QuitButton.onClick.AddListener(Quit);
    }
    #endregion

}
