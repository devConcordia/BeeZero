using UnityEngine;

public class CarnivorusPlantController : Character
{
	
	private Animator animator;
	
	private int direction = 1;
	private float scale = 2f;
	
	private bool rised = false;
	private float attackCooldownTime = 3f;
	private float attackNextAvailableTime  = 0f;
	
	[SerializeField] public GameObject target;
	[SerializeField] public GameObject prefabAttack;
	[SerializeField] public float distanceRise = 5f;
	[SerializeField] public float distanceBeginAttack = 2f;
	
	/// audios
	[SerializeField] public AudioClip attackSound;
	
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		setHitPoints( 2 );
		
        animator = GetComponent<Animator>();
	
    }
	
    void FixedUpdate() {
        
		float delta = Vector3.Distance( target.transform.position, transform.position );
		
		if( delta <= distanceRise ) {
		
			if( !rised ) {
				animator.SetTrigger("Rise");
				rised = true;
			}
			
		} else {
			
			rised = false;
			
		}
		
		direction = ( transform.position.x < target.transform.position.x )? -1 : 1;
		
		/// inverte a direção do personagem que o personagem está olhando
		transform.localScale = new Vector3( scale*direction, scale, scale );
		
		if( delta < distanceBeginAttack ) {
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if( stateInfo.IsName("Idle") ) {
				Attak();
			}
		}
		
    }
	
	void Attak() {
		
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
			
		}
		
	}
	
}
