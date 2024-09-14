using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FID_Slider : MonoBehaviour
{
    Slider F_slider;
    public static float FID_s;
    // Start is called before the first frame update
    void Start()
    {
        F_slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        FID_s=F_slider.value*-5f;
        Debug.Log(FID_s);
    }

    public static float getFID()
    {
        return FID_s;
    }
}
