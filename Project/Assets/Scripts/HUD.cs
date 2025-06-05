using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
	
	
	[SerializeField] public GameObject PrefabHeart;
	private Vector3 offsetHearts = new Vector3(-240f,120f,0);
	
	
	private Stack<GameObject> hearts = new Stack<GameObject>();
	
	public void clear() {
		
		/// clear HUD
		foreach( Transform child in transform )
			GameObject.Destroy(child.gameObject);
		
	}
	
	public void setHP( int points ) {
		
		
		if( hearts.Count > points ) {
			
			while( hearts.Count > points ) {
				
				GameObject.Destroy( hearts.Pop() );
				
			}

		} else {
			
			while( hearts.Count < points ) {
				
				GameObject heart = Instantiate(PrefabHeart, offsetHearts + new Vector3( hearts.Count*40f, 0f, 0f ), Quaternion.identity);
				heart.transform.SetParent(transform, false);
				hearts.Push(heart);
				
			}

		}
		
	}
	
	
	
}
