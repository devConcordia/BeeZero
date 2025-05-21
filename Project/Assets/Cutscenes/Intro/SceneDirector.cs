using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
	
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
		
		ChangeScene( "TutorialScene" );
		
	}
	
}
