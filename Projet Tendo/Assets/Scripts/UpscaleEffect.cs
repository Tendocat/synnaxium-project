using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpscaleEffect : MonoBehaviour
{
    #region Serializable fields
    [SerializeField] private float Duration;
    #endregion

    #region API
    public void StartEffect()
    {
        StartCoroutine(UpScale());
    }
    #endregion

    #region Private
    private IEnumerator UpScale()
    {
        Vector3 _startScale = transform.localScale;
        for (float t = 0f; t < Duration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, _startScale, t / Duration);
            yield return null;
        }
        transform.localScale = _startScale;
    }
    #endregion
}
