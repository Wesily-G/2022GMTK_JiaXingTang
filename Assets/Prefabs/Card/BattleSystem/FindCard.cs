using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCard : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player" && Input.GetMouseButtonDown(0))
        {
            CardController.Ins.FindIt(gameObject.name);
            Debug.Log(gameObject.name);
        }
    }
}
