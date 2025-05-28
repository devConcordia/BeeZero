using UnityEngine;

public class Web : MonoBehaviour {
	
	private Animator animator;
	private bool destroyed = false;
	public bool caught = false;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GetComponent<Animator>();
		
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        
		if( !destroyed && collision.CompareTag("Player") ) {
			
			caught = true;
			
			animator.SetTrigger("Start");
			
			PlayerController player = collision.GetComponent<PlayerController>(); 
			player.setFreeze( true );
			
		}
		
    }
	
    private void OnTriggerExit2D(Collider2D collision)
    {
		
        destroyed = true;
		Destroy( gameObject );
		
    }
	
	
}
