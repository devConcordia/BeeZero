using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* 
 *	
 *	Cutscene1	  9 sec
 *	Cutscene2	 30 sec
 *	Cutscene3	 40 sec
 *	Cutscene4	 12 sec
 *				 91 sec
 *	
 */
public class SoundtrackManager : MonoBehaviour
{
    public static SoundtrackManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
		
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicação
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre cenas
        audioSource = GetComponent<AudioSource>();
		
    }
	
	void Start() {
		
		/// após 88 segundos, encerra esse objeto
		Invoke("FadeOut", 88f);
		
	}
	
	
	
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine( 3f ));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;

        while(audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop();
        //audioSource.volume = startVolume; // Restaura volume caso reinicie
		
		Destroy(gameObject); // Destrói o objeto após o fade
		
    }
}
