using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezoneBehavior : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Can")
        {
            coll.gameObject.GetComponent<CanBehavior>().Unfreeze();
        }
    }
}
