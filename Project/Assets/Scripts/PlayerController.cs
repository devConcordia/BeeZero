using UnityEngine;

public class PlayerController : Character
{
	
//	private SimuladorFisica fisica;
	
	private Animator animator;
	private Rigidbody2D body;
	
	private bool jumping = false;
	private bool freezed = false;
	private int direction = 1;
	private float scale = 1.5f;
	[SerializeField] public float speedX = 4f;
	[SerializeField] public float speedY = 8f;
	
	/// audios
	[SerializeField] public AudioClip meleeSound;
	[SerializeField] public AudioClip healingSound;
	
	
	[SerializeField] public GameObject hudCanvas;
	private HUD hud;
	
	[SerializeField] public GameObject MeleeAttak;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		disableAutoDestroy();
		
	//	fisica = new SimuladorFisica( transform );
		
     	body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
		hud = hudCanvas.GetComponent<HUD>();
		
		
		setMaxHitPoints( 3 );
		setHitPoints( 3 );
		
		hud.setHP( hitPoints );
		
    }

    // Update is called once per frame
    void Update() {
		
		if( Input.GetKeyDown(KeyCode.Alpha1) ) {
			
			attak01();
			
		} else {
			
			if( !freezed ) move();
			
		}
		
	}
	
	public void attak01() {
		
		animator.SetBool("Run", false);
		animator.SetBool("Jump", false);
		animator.SetTrigger("Atk1");
		
		/// AudioClip clip, Vector3 position, float volume
		AudioSource.PlayClipAtPoint(meleeSound, transform.position, 1f);
		
		///
		Vector3 pos = transform.position + new Vector3( direction*2f, 0f, 0f );
		
		GameObject atk = Instantiate(MeleeAttak, pos, Quaternion.identity);
		
		setFreeze( false );
		
	}
	
	public void move() {
		
	//	if( !jumping ) {
			
			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");
			
			///
			if( x != 0 ) {
				
				direction = ( x < 0 )? -1 : 1;
				
				/// inverte a direção do personagem que o personagem está olhando
				transform.localScale = new Vector3( scale*direction, scale, scale );
				
				//fisica.addForca( new Vector3( x * speed, 0f, 0f ) );
				body.linearVelocityX = x * speedX;
				animator.SetBool("Run", true);
				
			} else {
				
				animator.SetBool("Run", false);
				
			}
			
			if( y > 0 ) jump();
		
	//	}
		
	}
	
	public void jump( float force = 1f ) {
		
		if( !jumping ) {
		
			jumping = true;
			
			//fisica.addForca( new Vector3( 0f, 8f * speed, 0f ) );
			body.linearVelocityY = speedY * force;
			
			animator.SetBool("Run", false);
			animator.SetBool("Jump", true);
		
		}
		
	}
	
	public void setFreeze( bool freezeValue ) {
		
		freezed = freezeValue;
		
		if( freezeValue ) {
			body.linearVelocityX = 0f;
			body.linearVelocityY = 0f;
		}
		
	}
	
	public void stum() {
		
		/// se a velocidade do objeto no eixo y menor ou igual a zero
		/// considera que ele estava caindo
		float dy = (body.linearVelocity.y <= 0f)? -1f : 1f;
		
		body.linearVelocityX = -direction * speedX;
		body.linearVelocityY = -dy * speedY;
		
		animator.SetBool("Run", false);
		animator.SetBool("Jump", true);
		
	}
	
	public override void takeDamage( int points = 1 ) {
		
		base.takeDamage( points );
		
		hud.setHP( hitPoints );
		
	}
	
	public override void recovery( int points = 1 ) {
		
		base.recovery( points );
		
		AudioSource.PlayClipAtPoint(healingSound, transform.position, 1f);
		
		hud.setHP( hitPoints );
		
	}
	
	public override void onDestroy() {
		
		SceneController.instance.GameOver();
		
	}
	
	///
	private void OnCollisionEnter2D(Collision2D collision) {
		
        if( collision.gameObject.CompareTag("Ground") ) {
			
			jumping = false;
			animator.SetBool("Jump", false);
			
        } else if( collision.gameObject.CompareTag("Thorn") ) {
			
			takeDamage( 1 );
			stum();
			
		} else if( collision.gameObject.CompareTag("Heart") ) {
			
			recovery( 1 );
			Destroy( collision.gameObject );
			
		} else if( collision.gameObject.CompareTag("GG") ) {
			
			SceneController.instance.GoodGame();
			
		}
		
    }
	
}
