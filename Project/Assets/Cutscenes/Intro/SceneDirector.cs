using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneDirector : MonoBehaviour
{
	
	public PlayableDirector timeline;  // Arraste o PlayableDirector da sua Timeline aqui no Inspector
    //public GameObject timelineUI;      // (Opcional) Caso queira desativar UI da Timeline quando pular

    void Update()
    {
		if( Input.GetKeyDown(KeyCode.Space) ) {
        if( timeline != null && timeline.state == PlayState.Playing) {
                
				// Para a Timeline imediatamente
				timeline.time = timeline.duration;
				timeline.Evaluate();
				timeline.Stop();

				// Opcional: Esconder UI da Timeline se tiver
			//	if (timelineUI != null) {
			//		timelineUI.SetActive(false);
			//	}
            
			}
		}
    }

	
	
	public void ChangeScene( string sceneName ) {
		
		SceneManager.LoadScene( sceneName );
		
	}
	
    public void StartScene01() {
		
		ChangeScene( "Cutscene01" );
		
	}
	
    public void StartScene02() {
		
		ChangeScene( "Cutscene02" );
		
	}
	
    public void StartScene03() {
		
		ChangeScene( "Cutscene03" );
		
	}
	
    public void StartScene04() {
		
		ChangeScene( "Cutscene04" );
		
	}
	
    public void StartTutorialScene() {
		
		SoundtrackManager.StopSoundtrack();
		ChangeScene( "TutorialScene" );
		
	}
	
    public void HomeScene() {
		
		ChangeScene( "HomeScene" );
		
	}
	
}
