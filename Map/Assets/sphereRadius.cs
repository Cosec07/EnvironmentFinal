using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereRadius : MonoBehaviour
{
    public bool hit = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Whale>(out Whale whale))
        {
            hit = true;
            Debug.Log("Hit");
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent<Whale>(out Whale whale))
        {
            hit = false;
            Debug.Log("Hit");
        }
    }
}
