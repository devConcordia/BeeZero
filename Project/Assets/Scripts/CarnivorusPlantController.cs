using UnityEngine;

public class CarnivorusPlantController : Character
{
	
	private Animator animator;
	
	private int direction = 1;
	private float scale = 2f;
	
	private bool rised = false;
	private float attakCooldownTime = 3f;
	private float attakNextAvailableTime  = 0f;
	
	[SerializeField] public GameObject target;
	[SerializeField] public GameObject PrefabAttak;
	[SerializeField] public float distanceRise = 5f;
	[SerializeField] public float distanceBeginAttak = 2f;
	
	/// audios
	[SerializeField] public AudioClip attakSound;
	
	
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
		
		if( delta < distanceBeginAttak ) {
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if( stateInfo.IsName("Idle") ) {
				Attak();
			}
		}
		
    }
	
	void Attak() {
		
		if( Time.time > attakNextAvailableTime ) {
		
			animator.SetTrigger("Atk");
			
			///
			Vector3 pos = transform.position + new Vector3( -direction, 0f, 0f );
			
			GameObject atk = Instantiate(PrefabAttak, pos, Quaternion.identity);
		
			/// AudioClip clip, Vector3 position, float volume
		//	AudioSource.PlayClipAtPoint(attakSound, transform.position, 1f);
			
			/// ignora colisao com o proprio emissor
			Physics2D.IgnoreCollision( atk.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			
			/// Define o próximo tempo disponível
			attakNextAvailableTime = Time.time + attakCooldownTime;
			
		}
		
	}
	
}
