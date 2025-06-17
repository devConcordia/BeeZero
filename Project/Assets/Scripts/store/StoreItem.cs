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
	
	private string id = "";
	private string itemName = "";
	private string desc = "";
	private int price = -1;
	
	void Awake() {
		
		previewImage = goPreview.GetComponent<Image>();
		
		titleText = goTitle.GetComponent<TMP_Text>();
		priceText = goPrice.GetComponent<TMP_Text>();
			
	}
	
	public string getID() {
		
		return id;
		
	}
	
	public string getName() {
		
		return itemName;
		
	}
	
	public int getPrice() {
		
		return price;
		
	}
	
	public void setID( string id ) {
		
		this.id = id;
		
	}
	
	public void setPreview( Sprite source ) {
		
		previewImage.sprite = source;
		
	}
	
	public void setTitle( string name, string desc ) {
		
		titleText.text = name +" - "+ desc;
		
		this.itemName = name;
		this.desc = desc;
		
	}
	
	public void setPrice( int price ) {
		
		priceText.text = price +"x";
		this.price = price;
		
	}
	
	public void toggle( bool status ) {
		
		goOutline.SetActive( status );
		
	}
	
}
