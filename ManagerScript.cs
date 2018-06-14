using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ManagerScript : MonoBehaviour {
    public Text txt;
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartButton ()
    {
        if (player.transform.position.Equals(new Vector3(0, 1, 0)))
        {
            UpdateText("New Map");
            StartCoroutine("NewLevel", 0.4f);
        } else
        {
            player.GetComponent<PlayerScript>().resetPos();
            player.GetComponent<PlayerScript>().StartPosReset();
            AllowObsToMove();
        }
    }

   IEnumerator NewLevel (float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Gameplay");
    }

    public void AllowObsToMove ()
    {

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].name == "Obstacle(Clone)")
            {
                //print("aa");
                obstacles[i].GetComponent<Obstacle>().startMovementAgain();
            }
        }
    }

    public void PositionResetText ()
    {
        UpdateText("Position Reset");
    }

    private void UpdateText (string text)
    {
        txt.gameObject.SetActive(true);
        txt.text = text;
        StartCoroutine("HideText", .8f);
    }

    IEnumerator HideText (float time)
    {
        yield return new WaitForSeconds(time);
        txt.gameObject.SetActive(false);
    }
}
