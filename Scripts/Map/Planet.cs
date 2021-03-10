using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Planet : MonoBehaviour
{
	[Tooltip("Spin: Yes or No")]
	public bool spin;
	[Tooltip("Spin the parent object instead of the object this script is attached to")]
	public bool spinParent;
	public float speed = 10f;

	[HideInInspector]
	public bool clockwise = true;
	[HideInInspector]
	public float direction = 1f;
	[HideInInspector]
	public float directionChangeSpeed = 2f;

	private Rigidbody rigidbody;
	private BoxCollider box;
	private AudioSource audio;
	private Scene1Manager scene1Manager;

	private void Start()
	{
		rigidbody = this.GetComponent<Rigidbody>();
		box = this.GetComponent<BoxCollider>();
		audio = this.GetComponent<AudioSource>();
		scene1Manager = GameObject.Find("Scene1Manager").GetComponent<Scene1Manager>();

		box.size = new Vector3(50f, 50f, 50f);
		rigidbody.useGravity = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (direction < 1f)
		{
			direction += Time.deltaTime / (directionChangeSpeed / 2);
		}

		if (spin)
		{
			if (clockwise)
			{
				if (spinParent)
					transform.parent.transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
				else
					transform.Rotate(Vector3.up, (speed * direction) * Time.deltaTime);
			}
			else
			{
				if (spinParent)
					transform.parent.transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
				else
					transform.Rotate(-Vector3.up, (speed * direction) * Time.deltaTime);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		audio.Play();
		if (other.gameObject.GetComponent<ShipHome>())
		{
			scene1Manager.GoHome();
		}
	}
}
