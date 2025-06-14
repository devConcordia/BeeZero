using UnityEngine;

public class HUDTutorial : MonoBehaviour
{
	
	[SerializeField] public GameObject target;
	
	[SerializeField] public GameObject moveHelp;
	[SerializeField] public GameObject jumpHelp;
	[SerializeField] public GameObject atkHelp;
	
    // Update is called once per frame
    void FixedUpdate() {
        
		float x = target.transform.position.x;
		
		if( x < 6f ) {
			
			moveHelp.SetActive(true);
			jumpHelp.SetActive(false);
			atkHelp.SetActive(false);
			
		} else if( x < 18f ) {
			
			moveHelp.SetActive(false);
			jumpHelp.SetActive(true);
			atkHelp.SetActive(false);
			
		} else if( x < 30f ) {

			moveHelp.SetActive(false);
			jumpHelp.SetActive(false);
			atkHelp.SetActive(true);
			
		} else {
			
			moveHelp.SetActive(false);
			jumpHelp.SetActive(false);
			atkHelp.SetActive(false);
			
		}
		
    }
	
}
