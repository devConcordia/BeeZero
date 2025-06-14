using UnityEngine;

public class CollectableTrigger : MonoBehaviour
{
	
	[SerializeField] public string collectableName = "";
	[SerializeField] public AudioClip triggerSound;
	
	private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			PlayerController player = collision.GetComponent<PlayerController>(); 
			
			player.addCollectable( collectableName );
			
			SoundManager.Play(triggerSound, 2f);
			
			Destroy( gameObject );
			
		}
		
    }
	
}
