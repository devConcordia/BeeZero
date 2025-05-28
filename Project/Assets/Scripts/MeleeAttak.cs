using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttak : MonoBehaviour
{
	
	[SerializeField] public int damage = 1;
	[SerializeField] public string targetTag = "";
	
	[SerializeField] public float timeToDestroy = 1f;
	
    void Start() {
		
        StartCoroutine(KillAfterTime());
		
    }
	
    IEnumerator KillAfterTime() {
		
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
		
    }
	
	public void setDamageAttak( int damageValue ) {
		
		damage = damageValue;
		
	}
	
	public void setTargetAttak( string tag ) {
		
		targetTag = tag;
		
	}
	
	private void OnTriggerEnter2D( Collider2D collision ) {
		
		if( collision.CompareTag( targetTag ) ) {
			
			Character target = collision.GetComponent<Character>();
			
			if( target != null )
				target.takeDamage( damage );
			
		}
		
		Destroy(gameObject);
		
	}
	
}
