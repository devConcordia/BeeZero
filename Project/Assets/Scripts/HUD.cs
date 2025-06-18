using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {
	
	[SerializeField] public GameObject PrefabHeart;
	[SerializeField] public Vector3 offsetHearts = new Vector3(-240f,120f,0);
	
	[SerializeField] public GameObject goBeesCount;
	[SerializeField] public GameObject goBeetlesCount;
	
	private TMP_Text beesCountText;
	private TMP_Text beetlesCountText;
	
	private Stack<GameObject> hearts = new Stack<GameObject>();
	
	
	void Awake() {
		
		beesCountText = goBeesCount.GetComponent<TMP_Text>();
		beetlesCountText = goBeetlesCount.GetComponent<TMP_Text>();
		
	}
	
	public void clear() {
		
		/// clear HUD
		foreach( Transform child in transform )
			GameObject.Destroy(child.gameObject);
		
	}
	
	public void setHP( int points ) {
		
		if( hearts.Count > points ) {
			
			while( hearts.Count > points )
				GameObject.Destroy( hearts.Pop() );
			
		} else {
			
			while( hearts.Count < points ) {
				GameObject heart = Instantiate(PrefabHeart, offsetHearts + new Vector3( hearts.Count*40f, 0f, 0f ), Quaternion.identity);
				heart.transform.SetParent(transform, false);
				hearts.Push(heart);
			}

		}
		
	}
	
	public void setCollectables( int bees, int beetle ) {
		
		beesCountText.text = bees +"x";
		beetlesCountText.text = beetle +"x";
		
	}
	
}
