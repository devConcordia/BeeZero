using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	private Rigidbody2D body;
	
	private bool jumping = false;
	private int direction = 1;
	[SerializeField] public float speed = 4f;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
		
    }

    // Update is called once per frame
    void FixedUpdate() {
		
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
		
		if( x < 0 ) direction = -1;
		else if( x > 0 ) direction = 1;
		
		transform.localScale = new Vector3( direction, 1, 1 );
		
		///
		if( x != 0 ) {
			
			body.linearVelocityX = x * speed;
		
		}
		
		if( !jumping && y > 0 ) {
			
			jumping = true;
			body.linearVelocityY = 8f;
			
		}
		
    }
	
	///
	private void OnCollisionEnter2D(Collision2D collision) {
		
        if( collision.gameObject.CompareTag("Ground") ) {
		
			jumping = false;
		   
        }
		
    }
	
}
