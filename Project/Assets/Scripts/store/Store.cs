using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
	
	[SerializeField] public GameObject goCanvas;
	[SerializeField] public GameObject itemPrefab;
	
//	private GameObject currentItem;
	private int indexItem = 0;
	
	private List<StoreItem> storeList = new List<StoreItem>();
	
	
	

    // Update is called once per frame
    void Update()
    {
        
		if( Input.GetKeyDown( KeyCode.Q ) ) {
			
			Destroy( gameObject );
			
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
	
	
	
	public void buyItem() {
		
		StoreItem item = storeList[ indexItem ];
		
	}
	
	public void createItem() {
		
		
	//	StoreItem 
	//	
	//	storeList.Add(  )
		
	}
	
}
