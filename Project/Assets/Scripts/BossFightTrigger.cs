using UnityEngine;

public class BossFightTrigger : MonoBehaviour {
	
	
	[SerializeField] public GameObject goBird;
	
	private BirdController bird;
	
	void Start() {
		
		bird = goBird.GetComponent<BirdController>();
		
	}
	
	private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			/// mudamos as dimensoes e posição da camera
			CameraFollow cameraFollow = Camera.main.gameObject.GetComponent<CameraFollow>();
			
			Camera.main.orthographicSize = 10f;
			cameraFollow.offset = new Vector3( 4f, 2f, -20f );
			
		//	bird.fisica.addForca( new Vector3( 0f, 10f, 0f ) );
			
			bird.fight();
			Destroy(gameObject);
		
		}
		
    }
}
