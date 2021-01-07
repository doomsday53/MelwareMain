using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(timerStart());
    }

    private IEnumerator timerStart()
    {
        float t = 0;
        while(t < lifeTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        ObjectPool.Despawn(this.gameObject);
    }
}
