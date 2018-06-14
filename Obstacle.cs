using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private AudioSource aSource;
    public AudioClip clank;
    bool isMovingObs = false;
    bool wasMovingEver;
    Vector3 dir;
    
    Vector3 origPos;
    float speed;
    void Awake()
    {
        speed = Random.Range(6.2f, 8f);
        origPos = transform.position;
        aSource = GetComponent<AudioSource>();
        if (Random.Range(0,3) == 0)
        {
            //print("hello");
            isMovingObs = true;
            wasMovingEver = true;
            //speed = Random.Range(4f, 8f);
        }
        else
        {
            wasMovingEver = false;
        }
    }

    void Start()
    {
        Invoke("SwitchDir", .6f);
    }

    void SwitchDir ()
    {
        dir = -dir;
        Invoke("SwitchDir", 1.2f);
    }

    void Update()
    {
        if (isMovingObs)
        {
  
            Vector3 temp = transform.position + (dir * speed * Time.deltaTime);
            transform.position = temp;
          
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke();
            isMovingObs = false;
            aSource.PlayOneShot(clank);
        }
    }

    public void SetDir (Vector3 vec)
    {
        dir = vec;
    }

    public void startMovementAgain ()
    {
        if (wasMovingEver && !isMovingObs)
        {
            isMovingObs = true;
            transform.position = origPos;
            CancelInvoke();
            Invoke("SwitchDir", .6f);

        }
    }
}
