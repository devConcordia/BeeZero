using UnityEngine;

public class SpiderController : MonoBehaviour
{
	
	private SimuladorFisica fisica;
	
	private Animator animator;
	
	[SerializeField] public GameObject target;
	[SerializeField] public AudioClip attakSound;
	private float nextAttakSound = 0f;
	
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
	private Vector3 spiderMove;
	private Vector3 ancora;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
		ancora = transform.position;
		spiderMove = ancora + new Vector3( 0f, -1f, 0f );
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
	//	if( Mathf.Abs(transform.position.y - target.transform.position.y) < heightAttak ) {
		if( transform.position.y > target.transform.position.y && ( Mathf.Abs(transform.position.y - target.transform.position.y) < heightAttak )) {
			/// se esta na mesma posição x
			if( Mathf.Abs( transform.position.x - target.transform.position.x ) < 1f ) {
				
				attaking = true;
				
			}
		}
		
		animator.SetBool("Move", attaking);
		
		if( attaking ) {
			
			fisica.addForcaElastica( attakSpeed, webPostion );
			
			if( nextAttakSound < Time.time ) {
				SoundManager.Play(attakSound, 1f, 1f);
				nextAttakSound = Time.time + attakSound.length;
			}
			
		} else {
			
		//	if( Vector3.Distance( transform.position, ancora ) > 0.5f ) {
			if( transform.position.y <= (ancora.y - .25f) ) {
				
				fisica.addForcaElastica( attakSpeed, webPostion );
				
			//	fisica.redefinir();
			//	fisica.addForca( (ancora - transform.position) * 20f );
				
			} else {
				
				fisica.redefinir();
				
				if( web == null && Time.time > attakNextAvailableTime ) {
					
					web = Instantiate(prefabWeb, webPostion, Quaternion.identity);
					
					Invoke("removeWeb", 1f);
					
					/// Define o próximo tempo disponível
					attakNextAvailableTime = Time.time + attakCooldownTime;
					
				}
			
			}
			
		}
		
		fisica.atualizar();
		
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
