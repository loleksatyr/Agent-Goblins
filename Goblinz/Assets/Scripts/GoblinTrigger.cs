using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTrigger : MonoBehaviour
{

    public bool isOn;

    private void Start()
    {
        isOn = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isOn = false;
        }
    }
}
