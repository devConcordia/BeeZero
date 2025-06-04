using UnityEngine;

public class Wind : MonoBehaviour
{
	
	private Animator animator;
	[SerializeField] public AudioClip windSound;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        animator = GetComponent<Animator>();
        
    }
	
    private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			animator.SetTrigger("Start");
			
			AudioSource.PlayClipAtPoint(windSound, transform.position, 1f);
			
			PlayerController player = collision.GetComponent<PlayerController>(); 
			player.jump( 2f, true );
			
		}
		
    }
}
