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
	
	//private bool attaking = false;
	
	private float attakCooldownTime = 3f;
	private float attakNextAvailableTime  = 0f;
	
	private Vector3 webPostion;
	private Vector3 ancora;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
		ancora = transform.position;
		webPostion = ancora + new Vector3( 0f, -heightAttak, 0f );
		
		fisica = new SimuladorFisica( transform );
		
        animator = GetComponent<Animator>();
		
		attakNextAvailableTime = delayAttak;
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
		bool attaking = false;
		
		/// se esta acima
		if( Mathf.Abs(transform.position.y - target.transform.position.y) < heightAttak ) {
			/// se esta na mesma posição x
			if( Mathf.Abs( transform.position.x - target.transform.position.x ) < 1f ) {
				
				attaking = true;
				
			}
		}
		
		animator.SetBool("Move", attaking);
		
		fisica.atualizar();
		
		if( attaking ) {
			
			fisica.addForcaElastica( attakSpeed, webPostion );
			
		} else {
			
			if( Vector3.Distance( transform.position, ancora ) > 0f ) {
				
				fisica.redefinir();
				fisica.addForca( (ancora - transform.position) * 10f );
				
			} else {
				
				if( web == null && Time.time > attakNextAvailableTime ) {
					
					///
				//	Vector3 pos = transform.position + new Vector3( 0f, -heightAttak, 0f );
					
					web = Instantiate(prefabWeb, webPostion, Quaternion.identity);
					
					Invoke("removeWeb", 1f);
					
					/// AudioClip clip, Vector3 position, float volume
				//	AudioSource.PlayClipAtPoint(attakSound, transform.position, 1f);
					
					/// Define o próximo tempo disponível
					attakNextAvailableTime = Time.time + attakCooldownTime;
					
				}
			
			}
			
		}
		
    }
	
	void removeWeb() {
		
		if( web != null ) {
			
			Web webscript = web.GetComponent<Web>();
			
			if( webscript.caught == false ) {
				
				Destroy( web );
				web = null;
				
			}
		}
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			Character player = collision.GetComponent<Character>(); 
			player.takeDamage( 1 );
			
		}
		
    }

}
