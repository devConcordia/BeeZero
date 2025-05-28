using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManager : MonoBehaviour {
	
	[SerializeField] public GameObject gameOverCanvas;
	
	public static TutorialSceneManager instance = null;
	
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
		
		SceneManager.LoadScene("TutorialScene");
        Time.timeScale = 1f;
		
	}
	
	public void GameOver() {
		
		playing = false;
		
		gameOverCanvas.SetActive(true);
        Time.timeScale = .0001f;
		
	}
	
	public void GoodGame() {
		
		playing = false;
		
	//	SceneManager.LoadScene("ActIScene");
		gameOverCanvas.SetActive(true);
        Time.timeScale = .0001f;
		
		
	}
	

}
