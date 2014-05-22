using UnityEngine;
using System.Collections;
using Leap;

public class GunControl : MonoBehaviour {
	private Controller controller;
	private float cooldown;

	private float reloadTime = 0.003f;
	public GameObject bullet;
	private Light nozLight;

	private Transform nozzle;
	private ParticleSystem nozParticles;

	private float emissionLifeCycle;

	private int heat = 0;

	// Use this for initialization
	void Start () {
		controller =  new Controller();
		cooldown = 0;

		GameObject g = GameObject.FindGameObjectWithTag("Nozzle");
		nozzle = g.transform;


		nozParticles = nozzle.GetComponent<ParticleSystem>();
		nozParticles.renderer.enabled = false;

		nozLight = nozzle.gameObject.light;
		nozLight.range = 0;

		emissionLifeCycle = 0;
	}	
	// Update is called once per frame
	void Update () {


		//Debug.Log ("frame");
		if(controller.IsConnected){
			Frame frame = controller.Frame();			
			HandList hands = frame.Hands;
//Aim gun++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
			
			Vector3 newDirection = Vector3.zero;
			newDirection.y = hands[0].Direction.x * 90;
			newDirection.x = -hands[0].Direction.y * 90;			
			this.transform.localRotation = Quaternion.Euler(newDirection);
		
//Get gesture++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

			if(heat > 500){
				//overheating
				Debug.Log ("OVERHEATING");
				Invoke("Cooled", 1f);

			}
			else
			{
				cooldown += Time.deltaTime;
				if(cooldown > reloadTime){
					FingerList flist = frame.Fingers;

					if(flist.Count > 3)
					{
						if(emissionLifeCycle == 0)
						{
							emissionLifeCycle = 0.2f;
							nozParticles.renderer.enabled = true;
							//nozLight.renderer.enabled = true;
							nozLight.range = 10;
							//nozLight.light.renderer.enabled = true;
						}
						shoot(newDirection, Vector3.forward  * 1500);
						heat+=2;
						cooldown = 0;
					}else
					{
						//cooling down
						heat-=1;
					}
				}
			}	
			if(emissionLifeCycle > 0 ){
				emissionLifeCycle-=Time.deltaTime;

				nozLight.intensity = Random.Range(1,8); 

			}
			else if(emissionLifeCycle<0){
				emissionLifeCycle = 0;
				nozParticles.renderer.enabled = false;
				//nozLight.renderer.enabled = false;
				nozLight.range = 0;
				//nozLight.light.renderer.enabled = false;
			}
		}

	}
//shoot++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void shoot(Vector3 direction, Vector3 movement)
	{
		GameObject b = Instantiate(bullet) as GameObject;

		Vector3 pos = this.transform.position;

		b.transform.position = new Vector3(nozzle.transform.position.x, nozzle.transform.position.y, nozzle.transform.position.z);
		b.transform.rotation = Quaternion.Euler(direction);
		b.rigidbody.AddRelativeForce(movement);
	}
	void Cooled()
	{
		heat = 0;
	
	}
}
