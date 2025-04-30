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

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Before Road Exit: DangerArea State: {MainScene.dangerArea.State}");
        MainScene.dangerArea.UpdateStateSqureOut();
        MainScene.dangerArea.UpdateState2Out();
        Debug.Log($"After Road Exit: DangerArea State: {MainScene.dangerArea.State}, Object: {other.gameObject.tag}");
    }
}
