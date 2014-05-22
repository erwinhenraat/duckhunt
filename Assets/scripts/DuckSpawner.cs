using UnityEngine;
using System.Collections;

public class DuckSpawner : MonoBehaviour {

	public float spawnTimer;
	public GameObject spawnable;
	public GameObject explosion;

	private float time = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time > spawnTimer)
		{
			GameObject d = Instantiate(spawnable) as GameObject;
			d.transform.position = new Vector3(Random.Range(-20,20), 50 ,Random.Range(8,30));
			time = 0;

			DuckBehaviour mScript = d.gameObject.GetComponent<DuckBehaviour>();
			mScript.toFollow = this.gameObject;
			mScript.explosion = explosion;

		}
	}
}
