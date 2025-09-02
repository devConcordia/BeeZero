
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
	
	
	public static Dictionary<string, object> Data;
    
	static GameState() {
		
		Load();
		
	}
	
	public static string getPath() { 
		
		return Application.persistentDataPath +"/gamestate.json";
	
	}
	
	public static void Save() {
		
		string json = MiniJSON.Json.Serialize( Data );
        
#if UNITY_WEBGL
		WebLocalStorage.SaveData( getPath(), json );
#else
		File.WriteAllText( getPath(), json );
#endif
		Debug.Log("GameState.Save");

	}
	
	public static void Load() {
		
#if UNITY_WEBGL
		string json = WebLocalStorage.LoadData(getPath());
#else
		string json = File.ReadAllText( getPath() );
#endif
		Data = MiniJSON.Json.Deserialize( json ) as Dictionary<string, object>;
		
		Debug.Log("GameState.Load");
		
		if( Data == null )
			Debug.Log("GameState is NULL");
	
	}
	
	public static void Clear() {
		
		Data = new Dictionary<string, object>();
		
		Debug.Log("GameState.Clear");
	//	Debug.Log( getPath() );
		
		Save();
		
	//	string json = MiniJSON.Json.Serialize( Data );
    //    
	//	File.WriteAllText( getPath(), json );
	//	
	//	
		
	}
	
	public static string GetString( string key, string defaultValue = "" ) {
		
		if( Data.ContainsKey( key ) )
			return Data[ key ].ToString();
		
		return defaultValue;
		
	}
	
	public static string GetString( string key ) {
		
		if( Data.ContainsKey( key ) )
			return Data[ key ].ToString();
		
		return "";
		
	}
	
	public static int GetInt( string key, int defaultValue = 0 ) {
		
		if( Data.ContainsKey( key ) )
			return Convert.ToInt32( Data[ key ] );
		
		return defaultValue;
		
	}
	
	public static int GetInt( string key ) {
		
		if( Data.ContainsKey( key ) )
			return Convert.ToInt32( Data[ key ] );
		
		return 0;
		
	}
	
	
	public static void SetString( string key, string v = "" ) {
		
		Data[ key ] = (object) v;
		
	}
	
	public static void SetInt( string key, int v = 0 ) {
		
		Data[ key ] = (object) v;
		
	}
	
	
}

