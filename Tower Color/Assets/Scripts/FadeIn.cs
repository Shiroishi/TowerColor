using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image myImage;

    // Start is called before the first frame update
    void Start()
    {
        myImage = gameObject.GetComponent<Image>();

        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        // Fade
        if (myImage.color.a > 0)
        {
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a - Time.deltaTime / 2f);
        }
    }
}
