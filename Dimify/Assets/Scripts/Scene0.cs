//PlayerPrefs Used FileBrowserDisplay- Determines whether to display File Browser or not 
// FileBrowserPath - Absoulute Path of the obj file. 
//ObjectDisplayed - int 0 means not displayed 
//FileImagePath Image file for the Icon

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
//using System.String; 

public class Scene0 : MonoBehaviour {
	//public string filePath = "";
	//private Rect labelRect = new Rect (Screen.width*0.05f, Screen.height*0.05f, Screen.width*0.1f, Screen.height*0.1f);
	[XmlAttribute("filePath")]
	string filePath;
	string imgFilePath = "";
	public ScreenCapture sc; 
	public GUISkin customSkin;
	public GUIStyle selectionGrid; 
	public Texture2D rgb; 
	//public List <RigidBodyData> rigidBodyDataList = new List<RigidBodyData> ();
	public RigidBodyDirectory xmlDataList;
	float rotateX = 0.0F;
	string rotateXString = "0.0";
	float rotateY = 0.0F;
	string rotateYString = "0.0";
	float rotateZ = 0.0F;
	string rotateZString = "0.0";

	float translateX = 0.0f;
	float translateY = 0.0f; 
	float translateZ = 0.0f; 

	string translateXString = "0.0";
	string translateYString = "0.0";
	string translateZString = "0.0";

	float manualScale = 1.0f;
	float coefficient = 1.0f; 
	float tenPower = 1.0f;
	string scaleXYZString = "0.0";

	Vector3 autoScale;
	Vector3 autoTranslate;

	private GameObject[] objectsRead;
	private MeshRenderer mr;
	
	//int selGridInt = 0;
	bool materialIncluded = false; 
	public int selGridObjectInt = 0;
	string[] selStrings = new string[] {"yes","no"};
	string[] physicalObjects = new string[] {"Big Box","Racket","SmallBox","Sphere","Stick"};
	public Texture2D[] physicalObjectImages;
    //public Sprite[] physicalSprites; 
	string status;
	public Material standardMaterial;
	public Material transparentMaterial;
	public GameObject cuteBoy;
	private static bool cuteBoyPresent = false;
	private Rect rctWindow1; 

	// Use this for initialization
	void Start () {
        cuteBoy = Instantiate(cuteBoy, new Vector3(1.58f, 0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f))) as GameObject;
        //cuteBoyPresent = false;
        sc = new ScreenCapture();
		xmlDataList = new RigidBodyDirectory ();
		rctWindow1 = new Rect(Screen.width*0.05f, Screen.height*0.02f, Screen.width*0.9f, Screen.height*0.25f);
		filePath = "";
		status = "";
		PlayerPrefs.SetInt("FileBrowserDisplay",0);
		PlayerPrefs.SetString ("FileBrowserPath", "");
		PlayerPrefs.SetString ("FileImagePath", "");
		PlayerPrefs.SetInt ("ObjectDisplayed", 0);
		PlayerPrefs.SetInt ("AutoTransform",0);
		PlayerPrefs.SetInt ("ManualTransform", 0);
		//myDataList = RigidBodyContainer.Load(Directory.GetParent(Application.dataPath) + "/Dime/rigidBodyInfo.dime");
	}


	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("WTf");
        Debug.Log(Directory.GetParent(Application.dataPath));
    }
	void OnGUI()
	{
		//GUI.skin = customSkin;
		rctWindow1 = GUI.Window(0, rctWindow1, DoMyWindow, "Dimeify", GUI.skin.GetStyle("window"));

	}//End OnGUI

	void DoMyWindow(int windowID)
	{
		if(status != "")
			GUI.Label (new Rect (Screen.width * 0.05f, Screen.height * 0.2f, Screen.width * 0.15f, Screen.height * 0.05f), status);
		switch (PlayerPrefs.GetInt ("ObjectDisplayed"))//
		{
		case 0:
			//GUI.skin = customSkin;
			GUI.Label (new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.1f, Screen.height * 0.07f), "Select an obj file");
			
			string fileBrowserPath = PlayerPrefs.GetString ("FileBrowserPath");
			if (fileBrowserPath != "") {
				filePath = fileBrowserPath;
			}
			
			filePath = GUI.TextField (new Rect (Screen.width * 0.15f, Screen.height * 0.05f, Screen.width * 0.45f, Screen.height * 0.07f), filePath, 180);
			if (GUI.Button (new Rect (Screen.width * 0.60f, Screen.height * 0.05f, Screen.width * 0.1f, Screen.height * 0.07f), "Browse")) {
				//callFileBrowser();
				//myFileBrowser = gameObject.GetComponent<testFileBrowser>();
				PlayerPrefs.SetInt ("FileBrowserDisplay", 1);
				
			}
			
			
			//GUI.Label (new Rect (Screen.width * 0.05f, Screen.height * 0.15f, Screen.width * 0.1f, Screen.height * 0.07f), "Is .mtl file included?");
			//selGridInt = GUI.SelectionGrid (new Rect (Screen.width * 0.15f, Screen.height * 0.15f, Screen.width * 0.1f, Screen.height * 0.07f), selGridInt, selStrings, 2);

			materialIncluded = GUI.Toggle(new Rect (Screen.width * 0.05f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.05f),materialIncluded, "Is .mtl file included?");


			if (GUI.Button (new Rect (Screen.width * 0.05f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.05f), "Submit")) 
			{				
				
				if (Path.GetExtension (filePath) != ".obj") {
					status = "Please select an obj file";
					PlayerPrefs.SetString ("FileBrowserPath", "");
					return;
				}
				if (File.Exists (filePath) == false) {
					status = "The File doesn't exist, Try again";
					PlayerPrefs.SetString ("FileBrowserPath", "");
					return;
				}
				if (materialIncluded)
					objectsRead = ObjReader.use.ConvertFile(filePath, true, standardMaterial, transparentMaterial);
				else
					objectsRead = ObjReader.use.ConvertFile (filePath, false, standardMaterial, transparentMaterial);
				if (objectsRead != null) {
					Debug.Log (objectsRead [0].name);
					mr = objectsRead [0].GetComponent<MeshRenderer> ();
					Debug.Log (mr.materials);
					PlayerPrefs.SetInt ("ObjectDisplayed", 1);
					//NEEDS SOME OTHER METHOD TO CHECK IF it is missing materials 
					Debug.Log (mr.GetComponent<Renderer>().bounds);
					
					
					if (PlayerPrefs.GetInt ("AutoTransform") < 0.5) {
						autoTransform ();
						manualScale = objectsRead [0].transform.localScale.x;
					}
					if (mr.materials == null) {						
						status = "Materials were missing, Try Again";
						PlayerPrefs.SetString ("FileBrowserPath", "");
					}
				} else {
					status = "Something went wrong, try some other file";
					PlayerPrefs.SetString ("FileBrowserPath", "");
				}
			}
			if (GUI.Button (new Rect (Screen.width * 0.20f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.05f), "Reset"))
				Application.LoadLevel(1);
			break;
			/*This section happens when the object is successfully displayed and the user wants to make tweaks.  */
			
		case 1: 

			//Debug.Log(myDataList.rigidBodyDataList.ToString());
			//0.5 is just to compare 0 and 1 Nothing significant about 0.5

			//if(!cuteBoyPresent)
			//{
			//	Instantiate(cuteBoy,new Vector3(1.58f,-0.48f,0.0f), Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f)));
			//	cuteBoyPresent = true; 
			//}
            //GameObject.FindGameObjectWithTag("IMG").GetComponent<SpriteRenderer>().sprite = physicalSprites[selGridObjectInt];
			//GUI.Label(new Rect (Screen.width * 0.20f, Screen.height * 0.25f, Screen.width * 0.15f, Screen.height * 0.05f),rgb);//new Rect (0.90f*Screen.width, Screen.height * 0.9f, Screen.width * 0.1f, Screen.height * 0.1f)
			float result;
            rctWindow1.height = Screen.height * 0.35f;
            //GameObject.FindGameObjectWithTag("IMG").GetComponent<SpriteRenderer>().sprite = physicalSprites[selGridObjectInt];

            GUILayout.BeginArea (new Rect(Screen.width*0.05f, Screen.height*0.05f, Screen.width*0.8f, Screen.height*0.95f));
			GUILayout.BeginVertical ();

			/************************************ TRANSLATION *******************************/ 
			
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Translate X");
			//rotateX = GUILayout.HorizontalSlider(rotateX,-180.0f,180.0f);
			translateXString = GUILayout.TextField(translateXString,10);
			//GUILayout.Label (Math.Round (rotateX, 0).ToString ());
			
			GUILayout.Label ("Translate Y");
			//rotateY = GUILayout.HorizontalSlider(rotateY,-180.0f,180.0f);
			translateYString = GUILayout.TextField(translateYString,10);
			//GUILayout.Label(Math.Round(rotateY,0).ToString());
			
			GUILayout.Label ("Translate Z");
			//rotateZ = GUILayout.HorizontalSlider(rotateZ,-180.0f,180.0f);
			translateZString = GUILayout.TextField(translateZString,10);
			//GUILayout.Label(Math.Round(rotateZ,0).ToString());
			GUILayout.EndHorizontal ();

			/************************************Rotation *******************************/ 		
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Rotate X");
			//rotateX = GUILayout.HorizontalSlider(rotateX,-180.0f,180.0f);
			rotateXString = GUILayout.TextField(rotateXString,10);
			//GUILayout.Label (Math.Round (rotateX, 0).ToString ());

			GUILayout.Label ("Rotate Y");
			//rotateY = GUILayout.HorizontalSlider(rotateY,-180.0f,180.0f);
			rotateYString = GUILayout.TextField(rotateYString,10);
			//GUILayout.Label(Math.Round(rotateY,0).ToString());

			GUILayout.Label ("Rotate Z");
			//rotateZ = GUILayout.HorizontalSlider(rotateZ,-180.0f,180.0f);
			rotateZString = GUILayout.TextField(rotateZString,10);
			//GUILayout.Label(Math.Round(rotateZ,0).ToString());
			GUILayout.EndHorizontal ();


			/************************************Scale *******************************/ 	

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Uniform Scale");
			scaleXYZString = GUILayout.TextField(scaleXYZString,10);			
			GUILayout.EndHorizontal ();


			GUILayout.BeginHorizontal();
			GUILayout.Label("Map to Physical object",GUILayout.Width(Screen.width*0.1f));
			selGridObjectInt = GUILayout.SelectionGrid(selGridObjectInt,physicalObjectImages,5,GUILayout.Height(75),GUILayout.Width(Screen.width*0.7f));
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
                if (GUILayout.Button("Back"))
                    Application.LoadLevel(Application.loadedLevel - 1); 
			if (GUILayout.Button ("Preview")) 
			{
				try {
					manualScale = float.Parse(scaleXYZString, CultureInfo.InvariantCulture);
					rotateX = float.Parse(rotateXString, CultureInfo.InvariantCulture);
					rotateY = float.Parse(rotateYString, CultureInfo.InvariantCulture);
					rotateZ = float.Parse(rotateZString, CultureInfo.InvariantCulture);
					translateX = float.Parse(translateXString, CultureInfo.InvariantCulture);
					translateY = float.Parse(translateYString, CultureInfo.InvariantCulture);
					translateZ = float.Parse(translateZString, CultureInfo.InvariantCulture);

				}
				catch(Exception ex)
				{
					status = "Not a valid Float entered for scale";
				}
				PlayerPrefs.SetInt ("ManualTransform", 0);
				if (PlayerPrefs.GetInt ("ManualTransform") < 0.5)
					manualTransform ();				
			}
			if(GUILayout.Button("Finish"))
			{
			    loadFromXml();
                cuteBoy.SetActive(false);
                GameObject.FindGameObjectWithTag("Cube").SetActive(false);
                GameObject.FindGameObjectWithTag("UI").SetActive(false);
                //GameObject.FindGameObjectWithTag("RGB").SetActive(false);
                if (!Directory.Exists(Directory.GetParent(Application.dataPath) + "/GraphicObjects/Screenshots"))
                Directory.CreateDirectory(Directory.GetParent(Application.dataPath) + "/GraphicObjects/Screenshots/");
                imgFilePath = Directory.GetParent(Application.dataPath)+ "/GraphicObjects/Screenshots/" + Path.GetFileNameWithoutExtension(filePath)+".png";
                
                sc.SaveScreenshot(CaptureMethod.RenderToTex_Synch, imgFilePath);
                //File.Copy()
                //Application.CaptureScreenshot(imgFilePath); 
                //imgFilePath = imgFilePath.Replace("/","\\");
                //sc.SaveScreenshot(CaptureMethod.RenderToTex_Asynch,imgFilePath);
                saveToXml ();
				Application.LoadLevel(Application.loadedLevel);
			}
			GUILayout.EndHorizontal();

			GUILayout.EndVertical ();
			GUILayout.EndArea ();
			break; 
		}
	}
    /// <summary>
    /// Changes the Transforms of the Rigid Body based on User Input 
    /// </summary>
	void manualTransform()
	{
		Vector3 temp = objectsRead [0].transform.localScale;
		temp.x = temp.y = temp.z = (float)manualScale;
		objectsRead [0].transform.localScale = temp;

		temp = objectsRead [0].transform.localEulerAngles;
		temp.x = rotateX;
		temp.y = rotateY;
		temp.z = rotateZ;
		objectsRead [0].transform.localEulerAngles = temp;

		temp = objectsRead [0].transform.position;
		temp.x = translateX; 
		temp.y = translateY; 
		temp.z = translateZ; 
		objectsRead [0].transform.localPosition = temp;

		//Debug.Log("Lossy Scale is "+objectsRead[0].transform.lossyScale);
		//Debug.Log ("Local scale is " + objectsRead [0].transform.localScale);


		/*
		objectsRead[0].transform.RotateAround(Vector3.zero,Vector3.right,rotateX);
		objectsRead[0].transform.RotateAround(Vector3.zero,Vector3.up,rotateY);
		objectsRead[0].transform.RotateAround(Vector3.zero,Vector3.forward,rotateX);*/
		PlayerPrefs.SetInt ("ManualTransform",1);
	}

    /// <summary>
    /// Changes the Transforms automatically to be centered around the origin and scaled to 1x1x1 Cube. 
    /// </summary>

	void autoTransform()
	{
		float scaleAxis = Mathf.Max (mr.GetComponent<Renderer>().bounds.extents.x,mr.GetComponent<Renderer>().bounds.extents.y,mr.GetComponent<Renderer>().bounds.extents.z);
		/************** SCALE *************/
		autoScale = new Vector3 (0.5f / scaleAxis, 0.5f / scaleAxis, 0.5f / scaleAxis);
		objectsRead[0].transform.localScale = autoScale;
		scaleXYZString = autoScale.x.ToString ();

		/***************TRANSLATE*******/
		autoTranslate = new Vector3 (-mr.GetComponent<Renderer>().bounds.center.x, -mr.GetComponent<Renderer>().bounds.center.y, -mr.GetComponent<Renderer>().bounds.center.z);
		objectsRead[0].transform.Translate (autoTranslate);
		translateXString = autoTranslate.x.ToString ();
		translateYString = autoTranslate.y.ToString (); 
		translateZString = autoTranslate.z.ToString ();

		PlayerPrefs.SetInt ("AutoTransform",1);
		//Debug.Log (autoScale);
		//Debug.Log (autoTranslate);
	}

    /// <summary>
    /// Saves Data to .dime file 
    /// </summary>
	void saveToXml()
	{
        //var myDataList = RigidBodyContainer.Load (Directory.GetParent(Application.dataPath) + "/Dime/rigidBodyInfo.dime");
        if (!Directory.Exists(Directory.GetParent(Application.dataPath) + "/GraphicObjects/Objs/"))
            Directory.CreateDirectory(Directory.GetParent(Application.dataPath) + "/GraphicObjects/Objs/");
        XmlSerializer xs = new XmlSerializer (typeof(RigidBodyDirectory));
		FileStream dimeFile = File.Open (Directory.GetParent(Application.dataPath) + "/GraphicObjects.xml", FileMode.Create);
        
        File.Copy(filePath, Directory.GetParent(Application.dataPath) + "/GraphicObjects/Objs/" + Path.GetFileName(filePath), true);
         
        RigidBodyData data = new RigidBodyData ();
		data.name = Path.GetFileNameWithoutExtension (filePath);
		data.objFilePath = Directory.GetParent(Application.dataPath) + "/GraphicObjects/Objs/" + Path.GetFileName(filePath);
        data.materialIncluded = materialIncluded; 
		data.imgFilePath = imgFilePath;
		data.tx = objectsRead [0].transform.position.x;
		data.ty = objectsRead [0].transform.position.y;
		data.tz = objectsRead [0].transform.position.z;
		data.rx = objectsRead [0].transform.localEulerAngles.x; 
		data.ry = objectsRead [0].transform.localEulerAngles.y; 
		data.rz = objectsRead [0].transform.localEulerAngles.z;
		data.sx = objectsRead [0].transform.localScale.x;
		data.sy = objectsRead [0].transform.localScale.y;
		data.sz = objectsRead [0].transform.localScale.z;
		data.physicalObjectId = selGridObjectInt; 
		//myDataList.Save(Directory.GetParent(Application.dataPath) + "/Dime/rigidBodyInfo.dime");

		//Create a List 

		//Populate the list with Deserialized data 

		//Append data to the last element of the list
		//rigidBodyDataList.Add (data);
		xmlDataList.rigidBodyList.Add (data);
		//Debug.Log (myDataList.GetLength ().ToString());
		xs.Serialize (dimeFile, xmlDataList);
        dimeFile.Close ();
	}
	void loadFromXml()
	{
        if(!Directory.Exists(Directory.GetParent(Application.dataPath) + "/GraphicObjects/"))
            Directory.CreateDirectory(Directory.GetParent(Application.dataPath) + "/GraphicObjects/");
        if (File.Exists(Directory.GetParent(Application.dataPath) + "/GraphicObjects.xml"))
		{
			XmlSerializer ds = new XmlSerializer (typeof(RigidBodyDirectory));
			FileStream file = File.Open (Directory.GetParent(Application.dataPath) + "/GraphicObjects.xml", FileMode.Open);
			object obj = ds.Deserialize (file);
			xmlDataList = (RigidBodyDirectory)obj;
			file.Close();
		}
	}

}


