using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour {

    private  ParticleSystem currentParticleSystem;
	// Use this for initialization
	void Start () {
        currentParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (currentParticleSystem.isPlaying)
        {
            return;
        }
        Destroy(gameObject);
	}
}
