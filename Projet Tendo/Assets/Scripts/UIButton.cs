using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu principal");
    }

    public IEnumerator CenterAnimation()
    {
        _rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
        while (Mathf.Abs(_rectTransform.localPosition.x) > 0.1 && Mathf.Abs(_rectTransform.localPosition.y) > 0.1)
        {
            _rectTransform.localPosition = new Vector3(0, 0, 0);
            yield return null;
        }
    }
}
