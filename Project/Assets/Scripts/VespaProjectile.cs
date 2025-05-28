using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeToDestroy = 5f;
    //public GameObject hitVfx;
	
    void Start()
    {
        StartCoroutine(KillAfterTime());
    }
	
    IEnumerator KillAfterTime()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        
		if( collision.CompareTag("Player") ) {
			
			Character player = collision.GetComponent<Character>(); 
			player.takeDamage( 1 );
			
		}
		
		Destroy(gameObject);
		
    }
}
