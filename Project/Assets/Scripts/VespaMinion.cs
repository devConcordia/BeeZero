using UnityEngine;

public class VespaMinion : Character
{
	
	private SimuladorFisica fisica;
	
	private Animator animator;
	
	private int direction = 1;
	private float scale = 1.5f;
	
//	private float moveCooldownTime = 3f;
//	private float moveNextAvailableTime  = 0f;
	
	private float attackCooldownTime = 3f;
	private float attackNextAvailableTime  = 0f;
	
	private Vector3 anchorPosition;
	
	[SerializeField] public float K = 3.0f;
	[SerializeField] public float moveRange = 1.0f;
	[SerializeField] public GameObject target;
	[SerializeField] public GameObject prefabAttack;
	//[SerializeField] public float distanceRise = 5f;
	[SerializeField] public float distanceBeginAttack = 2f;
	
	/// audios
	[SerializeField] public AudioClip attackSound;
	[SerializeField] public AudioClip damageSound;
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		setHitPoints( 1 );
		
		fisica = new SimuladorFisica( transform );
		
        animator = GetComponent<Animator>();
		
		anchorPosition = transform.position + new Vector3( -moveRange, 0f, 0f  );
		
    }
	
    void FixedUpdate() {
        
		float delta = Vector3.Distance( target.transform.position, transform.position );
		
		/*if( delta <= distanceRise ) {
		
			if( !rised ) {
				animator.SetTrigger("Rise");
				rised = true;
			}
			
		} else {
			
			rised = false;
			
		}*/
		
		
		if( delta > 5f ) {
			
			Move();
			
		} else {
			
			animator.SetBool("Move", false);
			
			direction = ( transform.position.x < target.transform.position.x )? -1 : 1;
			
			/// inverte a direção do personagem que o personagem está olhando
			transform.localScale = new Vector3( scale*direction, scale, scale );
			
			///
			if( delta <= distanceBeginAttack ) {
				
				AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
				if( stateInfo.IsName("Idle") ) {
					Attack();
				}
			}
			
		}
		
    }
	
	public override void takeDamage( int points = 1 ) {
		
		base.takeDamage( points );
		
		SoundManager.Play(damageSound, 2f);
		
	}
	
	void Move() {
		
	/*	if( Time.time > moveNextAvailableTime ) {
			
			
			
			/// Define o próximo tempo disponível
			attackNextAvailableTime = Time.time + moveCooldownTime;
			
		}
	*/
	
		animator.SetBool("Move", true);
		
		direction = ( transform.position - fisica.ultimaPosicao ).normalized.x > 0f? -1 : 1;
		
		/// inverte a direção do personagem que o personagem está olhando
		transform.localScale = new Vector3( scale*direction, scale, scale );
			
		fisica.addForcaElastica( K, anchorPosition );
	//	fisica.addForcaElastica( K );
		fisica.atualizar();
		
	//	Debug.Log("move");
		
	}
	
	void Attack() {
		
		if( Time.time > attackNextAvailableTime ) {
		
			animator.SetTrigger("Atk");
			
			///
			Vector3 pos = transform.position + new Vector3( -direction, 0f, 0f );
			
			GameObject atk = Instantiate(prefabAttack, pos, Quaternion.identity);
		
			/// AudioClip clip, Vector3 position, float volume
			SoundManager.Play(attackSound, 2f);
			
			/// ignora colisao com o proprio emissor
			Physics2D.IgnoreCollision( atk.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			
			/// Define o próximo tempo disponível
			attackNextAvailableTime = Time.time + attackCooldownTime;
			
		//	Debug.Log("Attack");
		
		}
		
	}
	
}
