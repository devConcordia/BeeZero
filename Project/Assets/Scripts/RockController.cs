using UnityEngine;

public class RockController : MonoBehaviour
{
	
	[SerializeField] public Vector3 finalPosition;
	[SerializeField] public AudioClip fallSound;
	
	
	private bool falling = false;
	
	void FixedUpdate() {
		
		if( falling ) {
			
			float distance = Vector3.Distance(finalPosition, transform.position);
			
			if( Mathf.Abs( distance ) > .5f ) {
			
				transform.position = Vector3.Lerp(transform.position, finalPosition, 5f * Time.deltaTime);
				
			} else {
				
				falling = false;
				
			}
			
		}
		
	}
	
	public void drop() { 
        	
		falling = true;
		SoundManager.Play(fallSound, 3f);
		
    }
	
}
