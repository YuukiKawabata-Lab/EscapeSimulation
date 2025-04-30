using Ookii.Dialogs;
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
        Debug.Log($"Before Exit: DangerArea State: {MainScene.dangerArea.State}");
        MainScene.dangerArea.UpdateStateCircleOut();
        MainScene.dangerArea.UpdateState2Out();
        Debug.Log($"After Exit: DangerArea State: {MainScene.dangerArea.State}, Object: {other.gameObject.tag}");
    }

}
