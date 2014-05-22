using UnityEngine;
using System.Collections.Generic;

public class BulletControl : MonoBehaviour {

	public List<AudioClip> audioList = new List<AudioClip>();
	// Use this for initialization
	void Start () {
//Play random audio++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		AudioSource audioSource = this.GetComponent<AudioSource>();
		int index = (int)Mathf.Floor(Random.Range(0,audioList.Count));
		audioSource.PlayOneShot(audioList[index]);
	}
	// Update is called once per frame
	void Update () {
//Destroy Bullet++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		Destroy(this.gameObject,2);		

	}
}
