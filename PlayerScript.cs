using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    private Rigidbody rb;
    private float speed = 20f;
    public GameObject horizontal, vertical;
    bool levelComplete = false;
    Vector2 startPos = Vector2.zero;
    float minDrag;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        minDrag = Screen.height * 7f / 100f;
    }
	public void StartPosReset ()
    {
        if (Input.touches.Length > 0)
        {
            if (Input.touches.Length > 1)
            {
                startPos = Input.touches[1].position;
            } else
            {
                startPos = Input.touches[0].position;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (!levelComplete)
        {
            KeyBoardInput();
            MobileInput();
        }
        CheckIfOutofBounds();
    }

    public void LevelComplete ()
    {
        levelComplete = true;
    }

    void CheckIfOutofBounds ()
    {
        if (Mathf.Abs(transform.position.x) > 80f || Mathf.Abs(transform.position.z) > 80f)
        {
            resetPos();
            GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>().AllowObsToMove();
        }
    }

    public void resetPos ()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 1, 0);
        GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>().PositionResetText();
    }

    void MobileInput ()
    {
        if (Input.touches.Length > 0 && rb.velocity.magnitude == 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startPos = Input.touches[0].position;
            }
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Vector2 inputDif = Input.touches[0].position - startPos;
                if (inputDif.magnitude > minDrag)
                {
                    if (Mathf.Abs(inputDif.x) > Mathf.Abs(inputDif.y))
                    {
                        if (inputDif.x < 0)
                        {
                            Leftward();
                        }
                        else
                        {
                            Rightward();
                        }
                    }
                    else
                    {
                        if (inputDif.y < 0)
                        {
                            Backwards();
                        }
                        else
                        {
                            Forward();
                        }
                    }
                }
            }
        }
    }

    void KeyBoardInput ()
    {
        if (Input.GetKeyDown(KeyCode.W) && rb.velocity.magnitude == 0)
        {
            Forward();
        }
        if (Input.GetKeyDown(KeyCode.A) && rb.velocity.magnitude == 0)
        {
            Leftward();
        }
        if (Input.GetKeyDown(KeyCode.S) && rb.velocity.magnitude == 0)
        {
            Backwards();
        }
        if (Input.GetKeyDown(KeyCode.D) && rb.velocity.magnitude == 0)
        {
            Rightward();
        }
    }


    void addForce (Vector3 force)
    {
        //rb.AddForce(force * 20, ForceMode.Impulse);
        rb.velocity = force * speed;
    }

    public void Forward()
    {
        GoingHorizontal(true);
        addForce(new Vector3(0, 0, 1));
    }

    public void Backwards()
    {
        GoingHorizontal(true);
        addForce(new Vector3(0, 0, -1));
    }

    public void Leftward()
    {
        GoingHorizontal(false);
        addForce(new Vector3(-1, 0, 0));
    }

    public void Rightward()
    {
        GoingHorizontal(false);
        addForce(new Vector3(1, 0, 0));
    }

    private void GoingHorizontal (bool goHorizontal)
    {
        if (goHorizontal)
        {
            horizontal.SetActive(true);
            vertical.SetActive(false);
        } else
        {
            horizontal.SetActive(false);
            vertical.SetActive(true);
        }
    }
}
