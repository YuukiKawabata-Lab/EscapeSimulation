using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed_Input : MonoBehaviour
{
    GameObject inputSpeed;
    public static float Set_speed;
    // Start is called before the first frame update
    void Start()
    {
        inputSpeed = GameObject.Find("Speed_Stimulus");
        Set_speed = float.Parse(inputSpeed.GetComponent<InputField>().text);
    }

    // Update is called once per frame
    void Update()
    {
        Set_speed = float.Parse(inputSpeed.GetComponent<InputField>().text);
        Debug.Log(Set_speed);
    }
}
