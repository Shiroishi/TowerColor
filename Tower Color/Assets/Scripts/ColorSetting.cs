using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetting : MonoBehaviour
{
    public Renderer myRenderer;
    public MyColor myColor;

    // Start is called before the first frame update
    void Awake()
    {
        // Set random color
        myColor = (MyColor)Random.Range(0, System.Enum.GetValues(typeof(MyColor)).Length);
        
        if (myRenderer != null)
            myRenderer.material = (Material)Resources.Load("RuntimeMaterials/mat" + myColor.ToString());
    }
}
