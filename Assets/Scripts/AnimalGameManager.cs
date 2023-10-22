using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGameManager : MonoBehaviour
{
    void OnMouseDown()
    {
        // Called when the object is clicked
        Debug.Log("Clicked on: " + gameObject.name);
    }
}
