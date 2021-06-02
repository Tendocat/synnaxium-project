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
        GameObject particule;
        for (int i=0; i<100; i++)
        {
            particule = Instantiate(gameObject);
            particule.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            particule.transform.position = new Vector3(particule.transform.position.x, particule.transform.position.y, 0);
            particule.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
            Destroy(particule, 1);
        }
    }
    #endregion
}
