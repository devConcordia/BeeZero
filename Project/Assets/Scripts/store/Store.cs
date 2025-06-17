using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
	
	[SerializeField] public Vector3 offset = new Vector3( 0f, 100f, 0f );
//	[SerializeField] public GameObject goPlayer;
//	[SerializeField] public GameObject goCanvas;
	[SerializeField] public Canvas goCanvas;
	[SerializeField] public GameObject goMessage;
	[SerializeField] public GameObject itemPrefab;
	
	private TMP_Text messageText;
	
	private int indexItem = 0;
	private List<StoreItem> storeList = new List<StoreItem>();
	
	void Awake() {
		
		messageText = goMessage.GetComponent<TMP_Text>();
		
	}
	
    // Update is called once per frame
    void Update() {
        
		if( Input.GetKeyDown( KeyCode.Q ) ) {
			
			gameObject.SetActive( false );
			
		} else if( Input.GetKeyDown( KeyCode.UpArrow ) ) {
			
			StoreItem item = storeList[ indexItem ];
			item.toggle(false);
			
			indexItem--;
			
			if( indexItem < 0 ) indexItem = storeList.Count - 1;
			
			item = storeList[ indexItem ];
			item.toggle(true);
			
		} else if( Input.GetKeyDown( KeyCode.DownArrow ) ) {
			
			StoreItem item = storeList[ indexItem ];
			item.toggle(false);
			
			indexItem++;
			
			if( indexItem >= storeList.Count ) indexItem = 0;
			
			item = storeList[ indexItem ];
			item.toggle(true);
			
		}
		
    }
	
	
	public void displayMessage( string message ) {
		
		messageText.text = message;
		
	}
	
	public int getItemPrice() {
		
		StoreItem item = storeList[ indexItem ];
		
		if( item == null ) return -1;
		
		return item.getPrice();
	
	}
	
	public void buyItem() {
		
		StoreItem item = storeList[ indexItem ];
		
		PlayerPrefs.SetInt( item.getID(), 1 );
		
		displayMessage( item.getName() +" comprado!");
		
	}
	
	public void createItem( string id, string title, string desc, int price, Sprite source ) {
		
		if( PlayerPrefs.GetInt( id, 0 ) == 0 ) {
		
			GameObject goItem = Instantiate( itemPrefab, goCanvas.transform );
			goItem.transform.position -= new Vector3( 0f, (storeList.Count * 1.1f) - 2f, 0f );
			
			StoreItem item = goItem.GetComponent<StoreItem>();
			
			item.setID( id );
			item.setPreview( source );
			item.setTitle( title, desc ); 
			item.setPrice( price );
			
			storeList.Add( item );
			
			if( storeList.Count == 1 ) item.toggle(true);
		
		}
		
	}
	
}
