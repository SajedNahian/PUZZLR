using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FinalObstacle : MonoBehaviour {
    private AudioSource aSource;
    public AudioClip success;
    private Button restartButton;

    void Awake()
    {
        restartButton = GameObject.FindGameObjectWithTag("ResetButton").GetComponent<Button>();
        restartButton.interactable = true;
        aSource = GetComponent<AudioSource>();
    } 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().LevelComplete();
            restartButton.interactable = false;
            aSource.PlayOneShot(success);
            Invoke("ChangeScene", success.length);   
        }
    }

    void ChangeScene ()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
