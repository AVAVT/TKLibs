using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceElement : MonoBehaviour {

	private AudioSource audioSource;
	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update(){
		if(!audioSource.isPlaying){
			this.gameObject.SetActive(false);
		}
	}
}
