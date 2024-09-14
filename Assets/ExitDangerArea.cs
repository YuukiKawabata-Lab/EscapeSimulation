using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDangerArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        MainScene.dangerArea.UpdateState("Out");
        Debug.Log("Exit: " + other.gameObject.tag);
    }

}
