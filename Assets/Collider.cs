using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public string state = null;
    // Start is called before the first frame update
    void Start()
    {
        state = "NotCaught";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collision2D other)
    {
        Debug.Log("StimulusEnter");

        if (other.gameObject.CompareTag("stimulus_c"))
        {
            state = "Caught";
            Debug.Log("C_C");
        }
    }

    private void OnTriggerExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("stimulus_c"))
        {
            state = "NotCaught";
            Debug.Log("C_NC");
        }
    }
}
