using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    public GameObject can;

    public int linesTotal;
    public int perLine;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        // Every other line offset
        float offset = 360f / (perLine - 1);

        for (int j = 0; j < linesTotal; j++)
        {
            // Circle pattern
            for (int i = 0; i < perLine; i++)
            {
                float angle = i * Mathf.PI*2f / perLine;
                if(j % 2 == 0) angle += offset;

                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, j + 1f, Mathf.Sin(angle) * radius);
                GameObject item = Instantiate(can, newPos, Quaternion.identity);
            }
        }
    }
}
