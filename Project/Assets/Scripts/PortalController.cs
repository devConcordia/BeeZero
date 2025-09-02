using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
	
	[SerializeField] public string nextScene = "";
	[SerializeField] public bool goToLobby = true;
	
	public void setNextScene( string name ) {
		
		nextScene = name;
		
	}
	
    private void OnTriggerEnter2D(Collider2D collision) { 
        
		if( collision.CompareTag("Player") ) {
			
			/// entre as fases o jogador deve passar pelo lobby
			/// porem, em determinado momento, no lobby haverá a opção de escolher
			/// entre cenas diferentes (e nesses casos a cena devera ser carregada diretamente)
			if( goToLobby ) {
				/// altera no storage o nome da proxima cena
				/// isso, porque o lobby é comum entre as cenas
			//	PlayerPrefs.SetString("nextScene", nextScene);
				GameState.SetString("nextScene", nextScene);
				GameState.Save();
				
				/// carrega o lobby
				SceneManager.LoadScene("LobbyScene");
			
			} else {
				
				/// carrega a proxima cena
				SceneManager.LoadScene( nextScene );
				
			}
			
		}
		
    }

}
