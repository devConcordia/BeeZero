using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour
{
	
	[SerializeField] public GameObject goOutline;
	[SerializeField] public GameObject goPreview;
	[SerializeField] public GameObject goTitle;
	[SerializeField] public GameObject goPrice;
	
	private Image previewImage;
	
	private TMP_Text titleText;
	private TMP_Text priceText;
	
   
	void Awake() {
		
		previewImage = goPreview.GetComponent<Image>();
		titleText = goTitle.GetComponent<TMP_Text>();
		priceText = goPrice.GetComponent<TMP_Text>();
		
	}
	
	
	public void setPreview( Sprite source ) {
		
		previewImage.sprite = source;
		
	}
	
	public void setTitle( string text ) {
		
		titleText.text = text;
		
	}
	
	public void setPrice( int price ) {
		
		priceText.text = price +"x";
		
	}
	
	public void toggle( bool status ) {
		
		goOutline.SetActive( status );
		
	}
	
}
