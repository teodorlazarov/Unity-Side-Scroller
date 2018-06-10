using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public int maxHealth;
    public static int playerHealth;

    Text text;

    private LevelManager levelManager;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        playerHealth = maxHealth;
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth <= 0)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
        }

        text.text = "" + playerHealth + "%";
	}

    public static void HurtPlayer(int damage)
    {
        playerHealth -= damage;
    }

    public void ResetHealth()
    {
        playerHealth = maxHealth;
    }
}
