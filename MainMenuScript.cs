using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = transform.localRotation.eulerAngles;
        temp.y += 180f * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(temp);
	}

    public void PlayButton ()
    {
        SceneManager.LoadScene("Gameplay");
    }

    //public void QuitButton ()
    //{
    //    Application.Quit();
    //}

    public void AboutButton ()
    {
        SceneManager.LoadScene("About");
    }

    public void BackButton ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
