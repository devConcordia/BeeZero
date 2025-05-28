using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   
	[SerializeField] public string currentScene = "";
	[SerializeField] public string nextScene = "";
	[SerializeField] public GameObject gameOverCanvas;
	
	public static SceneController instance = null;
	
	private bool playing = false;
	
    private void Awake() {
		
        if (instance != null && instance != this) {
            Destroy(gameObject); // Evita duplicação
            return;
        }

        instance = this;
        
    }
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
		gameOverCanvas.SetActive(false);
		
		playing = true;
        Time.timeScale = 1f;
		
    }
	
    // Update is called once per frame
    void Update() {
		
		if( !playing ) {
		
			if( Input.GetKeyDown(KeyCode.Space) )
				Restart();
			
		}
		
	}
	
	public void Restart() {
		
		playing = true;
		
		SceneManager.LoadScene( currentScene );
        Time.timeScale = 1f;
		
	}
	
	public void GameOver() {
		
		playing = false;
		
		gameOverCanvas.SetActive(true);
        Time.timeScale = .0001f;
		
	}
	
	public void GoodGame() {
		
		playing = false;
		
		SceneManager.LoadScene( nextScene );
		
	}
	
}
