using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemySpawner : MonoBehaviour
{
	Renderer m_Renderer;
	SpawnerManager spawner;


	// Start is called before the first frame update
	void Start()
	{
		m_Renderer = GetComponent<Renderer>();
		spawner = gameObject.GetComponent<SpawnerManager>();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.CompareTag("Player") && PlayerData.IsInDream)
		{
			Debug.Log("Collided with player in dream");
			spawner.SetRate(2.0f, 5.0f);
		}
	}
}
