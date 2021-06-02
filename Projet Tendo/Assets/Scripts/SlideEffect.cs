using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideEffect : MonoBehaviour
{
    #region Serializable fields
    [SerializeField] private float Duration;
    #endregion

    #region API
    public void StartEffect(Vector3 target)
    {
        StartCoroutine(Slide(target));
    }
    #endregion

    #region Private
    private IEnumerator Slide(Vector3 target)
    {
        Vector3 _startPosition = transform.position;
        for (float t = 0f; t < Duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(_startPosition, target, t / Duration);
            yield return null;
        }
        transform.position = target;
    }
    #endregion
}
