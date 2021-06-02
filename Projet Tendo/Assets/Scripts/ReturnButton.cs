using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    #region Private fields
    private RectTransform _rectTransform;
    #endregion

    #region API
    public void Menu()
    {
        SceneManager.LoadScene("Menu principal");
    }
    public void StartAnimation()
    {
        StartCoroutine(CenterAnimation());
    }
    #endregion

    #region Unity methods
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        GetComponent<Button>().onClick.AddListener(Menu);
    }
    #endregion

    #region Private
    private IEnumerator CenterAnimation()
    {
        _rectTransform.anchoredPosition.Set(0, 0);
        Vector2 newPosition;
        while (Mathf.Abs(_rectTransform.anchorMin.x) > 0.501f || Mathf.Abs(_rectTransform.anchorMin.y) > 0.501f)
        {
            newPosition = new Vector2(Mathf.Lerp(_rectTransform.anchorMin.x, 0.5f, 0.01f),
                                      Mathf.Lerp(_rectTransform.anchorMin.y, 0.5f, 0.01f));
            _rectTransform.anchorMin = newPosition;
            _rectTransform.anchorMax = newPosition;
            _rectTransform.pivot     = newPosition;
            yield return null;
        }
    }
    #endregion
}
