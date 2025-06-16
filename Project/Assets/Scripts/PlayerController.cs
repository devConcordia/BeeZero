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
	
	/// CollectablesItens
	[SerializeField] public int collectionBees = 0;
	[SerializeField] public int collectionBeetles = 0;
	
	/// audios
	[SerializeField] public AudioClip meleeSound;
	[SerializeField] public AudioClip healingSound;
	[SerializeField] public AudioClip jumpSound;
	[SerializeField] public AudioClip damageSound;
	
	
	[SerializeField] public GameObject hudCanvas;
	private HUD hud;
	
	[SerializeField] public GameObject MeleeAttak;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake() {
        
	//	fisica = new SimuladorFisica( transform );
		
     	body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		
		hud = hudCanvas.GetComponent<HUD>();
		
	}
		
	void Start() {	
		
		disableAutoDestroy();
		
		loadCache();
		
	//	setMaxHitPoints( 3 );
	//	setHitPoints( 3 );
	//	
	//	hud.setHP( hitPoints );
		
    }

    // Update is called once per frame
    void Update() {
		
		if( Input.GetKeyDown(KeyCode.Z) ) {
			
			attak01();
			
		} else if( Input.GetKeyDown(KeyCode.X) ) {
		
		} else if( Input.GetKeyDown(KeyCode.C) ) {
			
		} else {
			
			if( !freezed ) move();
			
		}
		
	}
	
	public void attak01() {
		
		animator.SetBool("Run", false);
		animator.SetBool("Jump", false);
		animator.SetTrigger("Atk1");
		
		/// AudioClip clip, Vector3 position, float volume
	//	AudioSource.PlayClipAtPoint(meleeSound, transform.position, 1f);
		SoundManager.Play(meleeSound, 2f);
		
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
				
			//	Vector3 forward = new Vector3( direction, 0f, 0f );
			//	
			//	RaycastHit hit;
			//	
			//	if( Physics.Raycast(transform.position, forward, out hit, 1f) ) {
			//		if( hit.collider.CompareTag("Wall") ) {
						
						body.linearVelocityX = x * speedX;
						
						/// anima somente se estiver no chão
						if( !jumping )	{
							
							animator.SetBool("Run", true);
						
						}
						
			//		}
			//	}
				
			} else {
				
				animator.SetBool("Run", false);
				body.linearVelocityX = 0;
				
			}
			
			if( y > 0 ) jump();
		
	//	}
		
	}
	
	public void jump( float force = 1f, bool external = false ) {
		
		if( !jumping || external ) {
			
			jumping = true;
			
			//fisica.addForca( new Vector3( 0f, 8f * speed, 0f ) );
			body.linearVelocityY = speedY * force;
			
			animator.SetBool("Run", false);
			animator.SetBool("Jump", true);
			
			if( !external ) 
				SoundManager.Play(jumpSound, 2f, 3f);
			
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
	
	public void addCollectable( string name ) {
		
		switch( name ) {
			
			case "bee":
				collectionBees++;
				break;
			
			case "beetle":
				collectionBeetles++;
				break;
			
		}
		
		saveCache();
		hud.setCollectables( collectionBees, collectionBeetles );
		
	}
	
	public override void takeDamage( int points = 1 ) {
		
		base.takeDamage( points );
		
		SoundManager.Play(damageSound, 2f);
		
		hud.setHP( hitPoints );
		
		saveCache();
		
	}
	
	public override void recovery( int points = 1 ) {
		
		base.recovery( points );
		
	//	AudioSource.PlayClipAtPoint(healingSound, transform.position, 1f);
		SoundManager.Play(healingSound, 2f);
		
		hud.setHP( hitPoints );
		
		saveCache();
	
	}
	
	public override void onDestroy() {
		
		SceneController.instance.GameOver();
		
		/// restart
		setHitPoints( 3 );
		saveCache();
		
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
			
		} else if( collision.gameObject.CompareTag("Rock") ) {
			
			jumping = false;
			animator.SetBool("Jump", false);
			
			if( transform.position.y >= collision.gameObject.transform.position.y ) {
				
				RockController rock = collision.gameObject.GetComponent<RockController>();
				
				rock.drop();
				
			}
			
		} 
		
		
		/*else if( collision.gameObject.CompareTag("GG") ) {
			
			SceneController.instance.GoodGame();
			
		}*/
		
    }
	
	
	
	
	private void saveCache() {
		
		PlayerPrefs.SetInt("maxHitPoints", maxHitPoints);
		PlayerPrefs.SetInt("hitPoints", hitPoints);
		PlayerPrefs.SetInt("collectionBeetles", collectionBeetles);
		PlayerPrefs.SetInt("collectionBees", collectionBees);
		
		PlayerPrefs.Save();
		
	}
	
	private void loadCache() {
		
		maxHitPoints = PlayerPrefs.GetInt("maxHitPoints", 3);
		hitPoints = PlayerPrefs.GetInt("hitPoints", 3);
		collectionBees = PlayerPrefs.GetInt("collectionBees", 0);
		collectionBeetles = PlayerPrefs.GetInt("collectionBeetles", 0);
		
		setMaxHitPoints( maxHitPoints );
		setHitPoints( hitPoints );
		
	//	if( hitPoints == 0 ) {
	//		
	//		hitPoints = 3;
	//		maxHitPoints = 3;
	//		
	//		saveCache();
	//		
	//	}
		
		
	//	Debug.Log( "hp: "+ hitPoints );
	//	Debug.Log( "bees: "+ collectionBees );
	//	Debug.Log( "beetles: "+ collectionBeetles );
		
		hud.setHP( hitPoints );
		hud.setCollectables( collectionBees, collectionBeetles );
		
	}
	
}
