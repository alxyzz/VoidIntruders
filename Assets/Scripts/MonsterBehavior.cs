using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{

    public float VisibleStateZ = 0.83f;
    public float InvisibleStateZ = -3.25f;

    public bool shadowState = false;


    private void Start()
    {
        if (Random.value > 0.5) //%50 percent chance to be shadowy
        {
            SwitchShadowState();
        }
    }

    private void Update()
    {
    }

    public void GotHit()
    {
        StartCoroutine(Pop());
    }
    

    void SwitchShadowState()
    {
        if (!shadowState) //if it's visible, makes it invisible
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, InvisibleStateZ);
            shadowState = true;
        }
        else if (shadowState)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, VisibleStateZ);
            shadowState = false;
        }


    }

    IEnumerator Pop()
    {
        
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
