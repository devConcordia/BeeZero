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
	
	[SerializeField] public GameObject goStoreCanvas;
	
	[SerializeField] public GameObject goEngineerMessage;
	private TMP_Text engineerMessageText;
	
	/// aqui deve ser add as sprites (os icones) que irão aparece na loja
	[SerializeField] public GameObject goStoreHelper;
	[SerializeField] public Sprite heartSprite;
	private Store store;
	
	private PortalController portalLeft;
	private PortalController portalRight;
	
	private PlayerController playerController;
	
	///
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
        
		///
		nextScene = PlayerPrefs.GetString("nextScene", "TutorialScene");
		engineerMessageText = goEngineerMessage.GetComponent<TMP_Text>();
		
		///
		portalLeft = goPortalLeft.GetComponent<PortalController>();
		portalRight = goPortalRight.GetComponent<PortalController>();
		
		store = goStoreCanvas.GetComponent<Store>();
		
		///
		playerController = goFerris.GetComponent<PlayerController>();
		playerController.recoveryAll();
	//	playerController.recovery( playerController.maxHitPoints );
		
        Time.timeScale = 1f;
		
		setupScene();
		
		goStoreCanvas.SetActive(false);
		
    }
	
	
	void Update() {
		
		/// se a loja estiver ativa
		if( goStoreCanvas.activeSelf ) {
			
			if( Input.GetKeyDown( KeyCode.Q ) ) {
				
				goFerris.SetActive( true );
				goStoreCanvas.SetActive( false );
				
			} else if( Input.GetKeyDown( KeyCode.Space ) ) {
				
				/// compra o item selecionado
				int price = store.getItemPrice();
				
				if( price > 0 && playerController.pay( price ) ) {
					
					store.buyItem();
					playerController.loadCache();
					playerController.recoveryAll();
					
				} else {
					
					store.displayMessage("Saldo insuficiente!");
					
				}
				
			}
			
		/// se a loja estiver desativada
		} else {
			
			if( Input.GetKeyDown( KeyCode.Space ) ) {
				
				goStoreHelper.SetActive(false);
				
				goFerris.SetActive( false );
				goStoreCanvas.SetActive( true );
			
			}

		}
	
	}
	
	private void setupScene() {
		
		switch( nextScene ) { 
			
			case "ActIScene":
				setupActI();
				break;
			
			case "ActIIScene":
				setupActII();
				break;
			
			case "ChooseScene":
				setupChoosePath();
				break;
			
		}
		
	}
	
	
	void setupActI() {
		
		engineerMessageText.text = "Se você me trouxer cascas de bezouro, podera trocar por algum item.";
		portalRight.setNextScene( nextScene );
		
		buildStore( 0 );
		
	}
	
	void setupActII() {
		
		engineerMessageText.text = "Tenho novos recursos na loja, quer conferir?";
		portalRight.setNextScene( nextScene );
		
		buildStore( 1 );
		
	}
	
	void setupChoosePath() {
		
		engineerMessageText.text = "Ouvi dizer que uma abelha foi raptada por Vespas ... Volte se quiser ajuda-la.";
				
		portalRight.setNextScene( "ActIIIAScene" );
		portalLeft.setNextScene( "ActIIIBScene" );
		goPortalLeft.SetActive(true);
		
		buildStore( 2 );
		
	}
	
	
	
	void buildStore( int access = 0 ) {
		
		access = 3;
		
		store.createItem( "store_item_1", "Coração de Mel", "Aumenta o HP em 1 ponto", 2, heartSprite );
		
		if( access > 0 ) {
			
			store.createItem( "store_item_2", "Coração de Mel", "Aumenta o HP em 1 ponto", 3, heartSprite );
			
		}
		
		if( access > 1 ) {
			
			store.createItem( "store_item_3", "Coração de Mel", "Aumenta o HP em 1 ponto", 3, heartSprite );
			
		}
		
	}
	
}
