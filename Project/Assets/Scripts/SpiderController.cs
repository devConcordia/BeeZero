using UnityEngine;

public class SpiderController : MonoBehaviour
{
	
	private SimuladorFisica fisica;
	
	private Animator animator;
	
	[SerializeField] public GameObject target;
	
	//[SerializeField] public GameObject prefabWebTrigger;
	[SerializeField] public float heightAttak = 3f;
	[SerializeField] public float delayAttak = 0f;
	[SerializeField] public float attakSpeed = 4f;
	[SerializeField] public GameObject prefabWeb;
	private GameObject web;
	
	private bool attaking = false;
	
	private float attakCooldownTime = 3f;
	private float attakNextAvailableTime  = 0f;
	
	private Vector3 initialPostion;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
		initialPostion = transform.position + new Vector3( 0f, -heightAttak, 0f );
		
		fisica = new SimuladorFisica( transform );
		
        animator = GetComponent<Animator>();
	
		attakNextAvailableTime = delayAttak;
	
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
		bool moving = false;
		
		/// se esta acima
		if( Mathf.Abs(transform.position.y - target.transform.position.y) < heightAttak ) {
			/// se esta na mesma posição x
			if( Mathf.Abs( transform.position.x - target.transform.position.x ) < 1f ) {
				moving = true;
				attaking = true;
			}
		}
		
		animator.SetBool("Move", moving);
		
		if( attaking ) {
			
			fisica.addForcaElastica( attakSpeed, initialPostion );
			
			fisica.atualizar();
			
		} else {
		
			if( web == null && Time.time > attakNextAvailableTime ) {
				
				///
				Vector3 pos = transform.position + new Vector3( 0f, -heightAttak, 0f );
				
				web = Instantiate(prefabWeb, pos, Quaternion.identity);
				
				Invoke("removeWeb", 1f);
				
				/// AudioClip clip, Vector3 position, float volume
			//	AudioSource.PlayClipAtPoint(attakSound, transform.position, 1f);
				
				/// Define o próximo tempo disponível
				attakNextAvailableTime = Time.time + attakCooldownTime;
				
			}
		
		}
		
    }
	
	
	void removeWeb() {
		
		if( web != null ) {
			
			Web webscript = web.GetComponent<Web>();
			
			if( webscript.caught == false ) {
				
				Destroy( web );
				web = null;
				
				attaking = false;
				
			}
		}
		
	}
	
	
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        
		if( collision.CompareTag("Player") ) {
			
			Character player = collision.GetComponent<Character>(); 
			player.takeDamage( 1 );
			
		}
		
    }
}
