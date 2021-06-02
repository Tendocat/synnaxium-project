using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpscaleEffect : MonoBehaviour
{
    #region Serializable fields
    [SerializeField] private float Duration;
    #endregion

    #region Private fields
    private Vector3 _startScale;
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpScale());
    }
    #endregion

    #region Private
    private IEnumerator UpScale()
    {
        _startScale = transform.localScale;
        for (float t = 0f; t < Duration; t += Time.deltaTime / Duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, _startScale, t);
            Debug.Log(t);
            yield return null;
        }
        transform.localScale = _startScale;
    }
    #endregion
}
