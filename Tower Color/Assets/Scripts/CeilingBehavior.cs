using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingBehavior : MonoBehaviour
{
    private PatternGenerator tower;
    private int collCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.Find("GameLogic").GetComponent<PatternGenerator>();

        // Start above tower
        transform.position = new Vector3(0, tower.linesTotal + 2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move down
        if (collCount == 0)
            transform.Translate(Vector3.down * Time.deltaTime * 3f);
    }

    void OnTriggerEnter(Collider coll)
    {
        collCount += 1;
    }

    void OnTriggerExit(Collider coll)
    {
        collCount -= 1;
    }
}
