using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour {

    private bool playerInRadius;

    public string levelToLoad;
    public Animator endGameAnimator;
    public Text guideText;
    // Use this for initialization
    void Start () {
        playerInRadius = false;
        endGameAnimator = endGameAnimator.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Vertical") && playerInRadius)
        {
            guideText.enabled = false;
            endGameAnimator.SetTrigger("EndLevel");
            Debug.Log("LevelExited");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            guideText.enabled = true;
            playerInRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            guideText.enabled = false;
            playerInRadius = false;
        }
    }
}
