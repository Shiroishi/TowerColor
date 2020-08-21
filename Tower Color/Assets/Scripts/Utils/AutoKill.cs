using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(timer));
    }

    private IEnumerator Die(float t)
    {
        yield return new WaitForSeconds(t);

        Destroy(gameObject);
    }
}
