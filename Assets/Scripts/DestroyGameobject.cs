using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    public float delay;
    public UnityEngine.Events.UnityEvent onDestroy;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        onDestroy.Invoke();
        Destroy(gameObject);
    }
}

