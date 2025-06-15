using UnityEngine;

public class Character : MonoBehaviour
{
	
	private bool destroyDisabled = false;
	
	[SerializeField] public int maxHitPoints = 1;
	[SerializeField] public int hitPoints = 1;
	
	public void disableAutoDestroy() {
		
		destroyDisabled = true;
		
	}
	
	public virtual void onDestroy() {
		/// TODO
	}
	
	
	public virtual void setHitPoints( int hp ) {
		
		hitPoints = hp;
		
	}
	
	public virtual void takeDamage( int points = 1 ) {
		
		hitPoints -= points;
		
		if( hitPoints <= 0 ) {
			
			if( !destroyDisabled ) Destroy( gameObject );
			
			onDestroy();
			
		}
		
	}
	
	public virtual void setMaxHitPoints( int max ) {
		
		maxHitPoints = max;
		
	}
	
	public virtual void recovery( int points = 1 ) {
		
		int total = hitPoints + points;
		
		if( total <= maxHitPoints ) {
			
			hitPoints = total;
			
		} else {
			
			hitPoints = maxHitPoints;
			
		}
		
	}
	
}
