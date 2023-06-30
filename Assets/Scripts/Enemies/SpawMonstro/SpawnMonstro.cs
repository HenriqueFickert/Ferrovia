using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonstro : MonoBehaviour {

	public GameObject monstro;

	public bool estaNumaCasa;
	public bool estaNoComeco;

	private float spawnTimer;
	private int spawnRandom;
	public float timerDeSpawn;

	public AudioSource spawnAudioS;
	public AudioClip spawnSound;

	public Vector3 rayOrigin;
	int b;
	int c;

	public int rateSpawn;

	void Start () {
		estaNoComeco = true;
		timerDeSpawn = 10;
		rateSpawn = 15;
	}

	void Update () 
	{
		if (!estaNumaCasa && !estaNoComeco && !monstro.activeSelf) {
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= timerDeSpawn) {
				for (int i = 0; i < 1; i++) {
					spawnRandom = Random.Range (1, rateSpawn);
				//	print(spawnRandom);
				}
				if (spawnRandom == 1) {
					SpawnMonster ();
				} else {
					spawnTimer = 0;
					spawnRandom = 0;
				}
			}
		}
	}


	void SpawnMonster()
	{
		for (int i = 0; i < 10; i++) {
			b = Random.Range (0, 10);
			c = Random.Range (0, 10);
			rayOrigin = new Vector3 (transform.position.x + b, transform.position.y, transform.position.z + c);
			RaycastHit hit;
			if (Physics.Raycast (rayOrigin, -transform.up, out hit)) { 
				Debug.DrawLine (rayOrigin, hit.point);
				if (!hit.collider.gameObject.CompareTag ("Casa")) {
					monstro.SetActive (true);
					monstro.transform.position = new Vector3 (hit.point.x, hit.point.y + monstro.transform.lossyScale.y / 2, hit.point.z);
					spawnAudioS.clip = spawnSound;
					spawnAudioS.PlayOneShot (spawnSound);
					spawnTimer = 0;
					spawnRandom = 0;
				} else {
					spawnTimer = 0;
				}
			}
		}
		spawnRandom = 0;
	}
}
