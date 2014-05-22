using UnityEngine;
using System.Collections;

public class DuckBehaviour : MonoBehaviour {

	public GameObject toFollow;
	public GameObject explosion;

	private AudioSource a;

	private int lives = 5;
	// Use this for initialization
	void Start () {
		a = this.gameObject.GetComponent<AudioSource>() as AudioSource;

	}
	
	// Update is called once per frame
	void Update () {
		//movement
		transform.LookAt(toFollow.transform);
		transform.Translate(Vector3.forward * 2 * Time.deltaTime, Space.Self);


	}
	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.tag == "Bullet")
		{
			lives--;
			if(lives <= 0)
			{
				GameObject e = Instantiate(explosion) as GameObject;
				e.transform.position = this.gameObject.transform.position;
				Destroy(this.gameObject);
				Destroy(e,0.5f);

				a.PlayOneShot(a.clip);

			}

		}

	}
}
