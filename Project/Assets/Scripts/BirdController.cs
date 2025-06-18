using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdController : MonoBehaviour {
	
	[SerializeField] public Vector3 anchor = new Vector3( 0f, 0f, 0f );
	[SerializeField] public float K = 5f;
	public SimuladorFisica fisica;
	private bool flightStarted = false;
	
	[SerializeField] public GameObject goTimeout;
	private TMP_Text timeoutText;
	private int counter = 30;
	
	[SerializeField] public GameObject target;
	[SerializeField] public GameObject featherProjectile;
	[SerializeField] public GameObject goFires;
	[SerializeField] public GameObject goSoundtrack;
	[SerializeField] public AudioClip battleSound;
	//[SerializeField] public float featherQuantity = 5;
	private float shootCooldownTime = 2f;
	private float shootNextAvailableTime  = 0f;
	private AudioSource soundtrack;
	private Coroutine coroutine;
	
	private Animator animator;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		fisica = new SimuladorFisica( transform );
		animator = GetComponent<Animator>();
		
		timeoutText = goTimeout.GetComponent<TMP_Text>();
		soundtrack = goSoundtrack.GetComponent<AudioSource>();
		
    }

    // Update is called once per frame
    void Update() {
        
		///
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		
		///
		if( stateInfo.IsName("Flying") ) {
			if( !flightStarted ) {
				
				transform.position += new Vector3( 0f, 5f, 0f );
				
				fisica.velocidade = new Vector3(3f, 3f, 0f);
				flightStarted = true;
				
			}
			
			fisica.addForcaElastica( K, anchor );
			
			shoot();
			
		} 
		
		fisica.atualizar();
		
    }
	
	///
	public void fight() {
		
		animator.SetTrigger("Notice");
		
		SoundManager.Play(battleSound, 1.5f);
		soundtrack.Stop();
		
		coroutine = StartCoroutine(victoryComing());
		
	}
	
	
	
	void shoot() {
		
		if( Time.time > shootNextAvailableTime ) {
		
		//	animator.SetTrigger("Atk1");
			/// AudioClip clip, Vector3 position, float volume
		//	SoundManager.Play(rangedSound, 2f);
			
			int featherQuantity = Random.Range( 5, 10 );
			
			///
			for( int i = 0; i < featherQuantity; i++ ) {
			
				Vector3 random = new Vector3( Random.Range(12f, 45f), Random.Range(76f, 96f), 0f );
				
				GameObject projectile = Instantiate(featherProjectile, random, Quaternion.Euler(0, 0, 230));
				
				projectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, -5f);
			
			}
			
            /// define o proximo tiro
            shootNextAvailableTime = Time.time + shootCooldownTime;
			
		}
		
	}
	
	IEnumerator victoryComing() {
		
		for( int i = 0; i < counter; i++ ) {
			
			timeoutText.text = (30-i) +" seg";
			
			if( i > 25 )
				goFires.SetActive(true);
			
			yield return new WaitForSeconds(1f);
			
		}
		
		SceneManager.LoadScene( "EndACutscene" );
		
	}
	
}
