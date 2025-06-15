using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyController : MonoBehaviour
{
   
//	[SerializeField] public string currentScene = "";
//	[SerializeField] public string nextScene = "";
//	[SerializeField] public GameObject gameOverCanvas;
	
	[SerializeField] public GameObject goFerris;
	[SerializeField] public GameObject goPortalRight;
	[SerializeField] public GameObject goPortalLeft;
	
	[SerializeField] public GameObject goEngineerMessage;
	private TMP_Text engineerMessageText;
	
	private string nextScene = "";
	
	public static LobbyController instance = null;
	
    private void Awake() {
		
        if (instance != null && instance != this) {
            Destroy(gameObject); // Evita duplicação
            return;
        }

        instance = this;
        
    }
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		nextScene = PlayerPrefs.GetString("nextScene", "TutorialScene");
		engineerMessageText = goEngineerMessage.GetComponent<TMP_Text>();
		
		///
		PlayerController playerController = goFerris.GetComponent<PlayerController>();
		playerController.recovery( playerController.maxHitPoints );
		
        Time.timeScale = 1f;
		
		setupScene();
		
    }

	
	private void setupScene() {
		
		///
		PortalController portalLeft = goPortalLeft.GetComponent<PortalController>();
		PortalController portalRight = goPortalRight.GetComponent<PortalController>();
		
		switch( nextScene ) { 
			
			case "ActIScene":
				engineerMessageText.text = "Se você me trouxer cascas de bezouro, posso construir alguns equipamentos para você.";
				portalRight.setNextScene( nextScene );
				break;
			
			case "ActIIScene":
				engineerMessageText.text = "Tenho novos recursos na loja, quer conferir?";
				portalRight.setNextScene( nextScene );
				
				break;
			
			case "ChooseScene":
				engineerMessageText.text = "Ouvi dizer que uma abelha foi raptada por Vespas ... Volte se quiser ajuda-la.";
				
				portalRight.setNextScene( "ActIIIAScene" );
				portalLeft.setNextScene( "ActIIIBScene" );
				goPortalLeft.SetActive(true);
				
				break;
			
		}
		
	}
	
	
/*
	
    // Update is called once per frame
    void Update() {
		
		if( !playing ) {
		
			if( Input.GetKeyDown(KeyCode.Space) )
				Restart();
			
		}
		
	}
	
	
/*	public void Restart() {
		
		playing = true;
		
		SceneManager.LoadScene( currentScene );
        Time.timeScale = 1f;
		
	}
	
	public void GameOver() {
		
		playing = false;
		
	//	gameOverCanvas.SetActive(true);
        Time.timeScale = .0001f;
		
	}
	
	public void GoodGame() {
		
		playing = false;
		
		SceneManager.LoadScene( nextScene );
		
	}

*/
	
}
