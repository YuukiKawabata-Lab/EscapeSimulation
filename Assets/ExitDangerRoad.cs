using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDangerRoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collision2D other)
    {
        //if(MainScene.dangerArea.State == "Out")
        //{
        //    MainScene.dangerArea.UpdateState("IN");
        //}
        MainScene.dangerArea.UpdateStateSqureOut();
        MainScene.dangerArea.UpdateState2Out();
    }
}
