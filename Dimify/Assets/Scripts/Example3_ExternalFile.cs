using UnityEngine;
using System.Collections;

public class Example3_ExternalFile : MonoBehaviour {

	public string objFileName = "";
	public Material standardMaterial;
	public Material transparentMaterial;

	void Start () {
		/*var loadingText = GameObject.Find("LoadingText").GetComponent<GUIText>();
		loadingText.enabled = true;
		loadingText.text = "Loading...";
		yield return null;
		
		objFileName = objFileName;
		*/
		GameObject[] go = ObjReader.use.ConvertFile (objFileName, false, standardMaterial, transparentMaterial);
		
		//loadingText.enabled = false;
	}
}