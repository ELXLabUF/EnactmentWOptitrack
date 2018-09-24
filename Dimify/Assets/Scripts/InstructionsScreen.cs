using UnityEngine;
using System.Collections;

public class InstructionsScreen : MonoBehaviour {

	private string welcomeText = "Welcome to Dimeify, Follow these guidelines for best results"; 
	public GUIStyle welcomeStyle; 

	private string instructionText = "1. Download the 3d model of your choice from sites like http://tf3dm.com/ or http://www.turbosquid.com/.\n" +
		"2. You typically find a .zip file when you download them. Extract it to a suitable folder.\n"+
        "3. Use the online converter to convert different formats to obj file. http://www.greentoken.de/onlineconv/ \n" +
        "4. material file should be renamed to OBJFILENAME_mtl.txt format" +
		"5. Make sure obj file and the material file are in the same directory. Eg : Lantern.obj and Lantern_mtl.txt\n"+
		"6. Make sure .mtl has reference to images that are not in the folder.\n"+
			"7. Make sure number that the vertex count of the polygon is less than 65,534.\n"+
			"8. Make sure .mtl has reference to images that are not in the folder.\n"+
			"9. Make sure that the file does not contain any other props apart from the one you need.\n"+
			"10. If the object does not rotate around its center then change the pivot point in Maya.   \n";


	public GUIStyle instructionStyle; 

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUILayout.BeginArea (new Rect (Screen.width*0.1f,Screen.height*0.1f, Screen.width*0.8f,Screen.height*0.8f));
		GUILayout.BeginVertical (); 
		GUILayout.Space (Screen.height*0.1f);
		GUILayout.Label (welcomeText, welcomeStyle);
		GUILayout.Space (Screen.height*0.1f);
		GUILayout.Label (instructionText, instructionStyle);
		GUILayout.BeginHorizontal ();
		GUILayout.Space (Screen.width * 0.3f);
		if (GUILayout.Button ("OK, Got it")) 
		{
			Application.LoadLevel(1);
		}
		GUILayout.Space (Screen.width * 0.3f);
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
}
