using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    GameObject toFollow;
    // Update is called once per frame

    private void Start()
    {
        toFollow = GameObject.Find("PlayerShadow");
    }
    void FixedUpdate()
    {
        transform.LookAt(toFollow.transform);
    }
}
