using UnityEngine;

public class HUD : MonoBehaviour {
	
	[SerializeField] public GameObject PrefabHeart;
	private Vector3 offsetHearts = new Vector3(-240f,120f,0);
	
	public void clear() {
		
		/// clear HUD
		foreach( Transform child in transform )
			GameObject.Destroy(child.gameObject);
		
	}
	
	public void setHP( int points ) {
		
		clear();
		
		/// render HUD
		for( int i = 0; i < points; i++ ) {
			
			GameObject heart = Instantiate(PrefabHeart, offsetHearts + new Vector3( i*40f, 0f, 0f ), Quaternion.identity);
			
			heart.transform.SetParent(transform, false);
			
		}
		
	}
	
	
	
}
