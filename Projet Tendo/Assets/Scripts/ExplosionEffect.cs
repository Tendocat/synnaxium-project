using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    #region API
    public void StartExplosion()
    {
        StartCoroutine(WinExplosion());
    }
    #endregion

    #region Private
    private IEnumerator WinExplosion()
    {
        GameObject [] particule = new GameObject[2];
        particule[0] = gameObject;
        particule[1] = Instantiate(gameObject);
        for (int i=0; i<100; i++)
        {
            particule[i % 2].transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            particule[i % 2].transform.position = new Vector3(particule[i % 2].transform.position.x, particule[i % 2].transform.position.y, 0);
            particule[i % 2].GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
        }
    }
    #endregion
}
