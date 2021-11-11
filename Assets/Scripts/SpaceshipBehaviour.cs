using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipBehaviour : MonoBehaviour
{

    //when you click  down key, it switches the state
    public bool shadowForm = true;
    bool shadowCooldown;

    public float ShadowZCoord; //-3.87
    public float NormalZCoord; //9.92f

    AudioSource shootsound;
    public AudioClip ting;
    public GameObject Bullet;
    ObjectPooling objectpooler;

    bool cooldown = false;
    float speed;
    float xPosition = 0;
    public float acceleration;
    public float movementRange;

    // Start is called before the first frame update
    void Start()
    {
        objectpooler = ObjectPooling.Instance;
        shootsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Accelerating
        if (Input.GetKey(KeyCode.A))
        {
            speed = speed - acceleration;
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed = speed + acceleration;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchShadowState();
        }

        speed = speed * 0.9f;
        xPosition = xPosition + speed;

        if (xPosition > movementRange)
        {
            xPosition = movementRange;
        }
        if (xPosition < -movementRange)
        {
            xPosition = -movementRange;
        }
        //xPosition = Mathf.Clamp(xPosition, -movementRange, movementRange);


        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
        
        transform.Rotate(Time.deltaTime*60, Time.deltaTime * 60, 0);
    }

    IEnumerator fireCooldown()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        cooldown = false;
    }





    void SwitchShadowState()
    {
        //play nifty sound
        if (shadowForm)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.9f);
            shadowForm = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -3.87f);
            shadowForm = true;
        }
        shootsound.PlayOneShot(ting);
        
        Debug.Log(shadowForm.ToString() + " Is the shadow form boolean.");
    }

    void ShootBullet()
    {
        if (!cooldown)
        {
            
            objectpooler.SpawnFromPool("Bullet", transform.position, Quaternion.identity);

            shootsound.Play();
            cooldown = true;
            StartCoroutine(fireCooldown());
        }
        
    }
}
