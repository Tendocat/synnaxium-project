using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit(0);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu principal");
    }
    public IEnumerator CenterAnimation()
    {
        while (Mathf.Abs(_rectTransform.anchorMin.x) > 0.501f || Mathf.Abs(_rectTransform.anchorMin.y) > 0.501f)
        {
            _rectTransform.anchorMin = new Vector2(Mathf.Lerp(_rectTransform.anchorMin.x, 0.5f, 0.01f),
                                                   Mathf.Lerp(_rectTransform.anchorMin.y, 0.5f, 0.01f));
            _rectTransform.anchorMax = new Vector2(Mathf.Lerp(_rectTransform.anchorMax.x, 0.5f, 0.01f),
                                                   Mathf.Lerp(_rectTransform.anchorMax.y, 0.5f, 0.01f));
            yield return null;
        }
    }
}
