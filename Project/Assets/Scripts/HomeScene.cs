using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
	
	void Start() {
		
		/// limpa todos os dados de jogos anterirores
		PlayerPrefs.DeleteAll();
		
	}
	
	
    // Update is called once per frame
    void Update() {
        
		if( Input.GetKeyDown(KeyCode.Space) ) {
			
			SceneManager.LoadScene( "Cutscene01" );
		
		}
		
    }
	
}
