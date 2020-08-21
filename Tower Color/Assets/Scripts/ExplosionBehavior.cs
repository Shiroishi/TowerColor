using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Grow
        transform.localScale *= 1f + (Time.deltaTime * 8f);
        myRenderer.material.color = new Color(1, 1, 1, myRenderer.material.color.a - Time.deltaTime * 6f);

        if (myRenderer.material.color.a <= 0)
            Destroy(gameObject);
    }
}
