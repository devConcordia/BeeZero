using UnityEngine;

public class CollectableTrigger : MonoBehaviour
{
	
	[SerializeField] public string id = "";
	[SerializeField] public string collectableName = "";
	[SerializeField] public AudioClip triggerSound;
	
	void Start() {
		
		/// verifica se o coletavel ja foi obtido
        if( PlayerPrefs.GetInt(id, 0) == 1 ) {
            gameObject.SetActive(false);
        }
		
    }
	
	
	private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			///
			PlayerController player = collision.GetComponent<PlayerController>(); 
			player.addCollectable( collectableName );
			
			///
			SoundManager.Play(triggerSound, 2f);
			
			/// salva no storage que o coletavel foi obtido
			PlayerPrefs.SetInt(id, 1);
			PlayerPrefs.Save();
			gameObject.SetActive(false);
			
			Destroy( gameObject );
			
		}
		
    }
	
}
