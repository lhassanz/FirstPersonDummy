using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Transform target;
	public float speed;
	private float health;
	private Vector3 offset;

	private float maxHealth;
	public GameObject explosion;
	//private Text healthText;
	//private Image healthBar;

	// Use this for initialization
	void Start () {
		speed = 1f;
		offset = transform.position - target.position;

		health = 100.0f;
		maxHealth = 100.0f;
		//healthText = transform.Find("Canvas").Find("Text").GetComponent<Text>();
		//healthBar = transform.Find("Canvas").Find("Image").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target, Vector3.up);
		transform.position += transform.forward * speed * Time.deltaTime;
		//healthText.text = health.ToString();
		//healthBar.fillAmount = health / maxHealth;
	}

	void OnCollisionEnter(Collision col){

		if(col.gameObject.tag == "Bullet"){
			health -= 10;
			if(health < 1){
				Destroy(this);
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(gameObject);
			}
		}
	}
}
