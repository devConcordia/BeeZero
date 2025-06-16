using UnityEngine;

public class Wind : MonoBehaviour
{
	
	private Animator animator;
	[SerializeField] public float force = 2f;
	[SerializeField] public AudioClip windSound;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        animator = GetComponent<Animator>();
        
    }
	
    private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			animator.SetTrigger("Start");
			
		//	AudioSource.PlayClipAtPoint(windSound, transform.position, 1f);
			SoundManager.Play(windSound, 2f, 3f);
			
			PlayerController player = collision.GetComponent<PlayerController>(); 
			player.jump( force, true );
			
		}
		
    }
}
