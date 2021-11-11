using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPooledObject
{
    bool shadowBullet;

    public GameObject explosionEffect;

    GameObject Monster;
    MonsterBehavior MonsterScript;

    ObjectPooling objectpooler;

    GameObject Player;

    AudioSource PlayerSource;
    public AudioClip Miss;
    public AudioClip Hit;

    public float acceleration;
    public float Range;
    // Start is called before the first frame update

    private void Start()
    {
        Player = GameObject.Find("Player");

        PlayerSource = Player.GetComponent<AudioSource>();
        objectpooler = ObjectPooling.Instance;
        
    }

    public void OnObjectSpawn()
    {
        objectpooler = ObjectPooling.Instance;
        if (transform.position.z <= 0.8f)
        {
            shadowBullet = true;
        }
        else
        {
            shadowBullet = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Range)
        {
            gameObject.SetActive(false);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + acceleration, transform.position.z);
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            Monster = other.gameObject;
            MonsterScript = Monster.GetComponent<MonsterBehavior>();
            if (MonsterScript.shadowState && shadowBullet)
            {
                PlayerSource.PlayOneShot(Hit);
                other.gameObject.GetComponent<MonsterBehavior>().GotHit();

                Vector3 posi = new Vector3(transform.position.x, transform.position.y, 0.39f);
                Instantiate(explosionEffect, posi, transform.rotation);
            }
            else if (MonsterScript.shadowState == false && !shadowBullet)
            {
                PlayerSource.PlayOneShot(Hit);
                other.gameObject.GetComponent<MonsterBehavior>().GotHit();

                Vector3 posi = new Vector3(transform.position.x, transform.position.y, 0.39f);
                Instantiate(explosionEffect, posi, transform.rotation);
            }
            else
            {
                PlayerSource.PlayOneShot(Miss); //play silly sound
            }
        }
        gameObject.SetActive(false);

    }

}
