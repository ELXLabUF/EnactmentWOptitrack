#pragma strict
import UnityEngine;
import System.Collections;

private var barrel : GameObject;
private var assetCollider:BoxCollider;
private var barrelRender : MeshRenderer;
function Start () {
	//barrel = Instantiate(UnityEngine.Resources.Load("Big Box Object/FinalBoxObject/BarrelMaya"),Vector3(0,1,0),Quaternion.identity); 
	var barrel : GameObject = this.gameObject;
	//barrel = Instantiate(UnityEngine.Resources.Load("Online/Prefabs/SoccerBall"),Vector3(0,0,0),Quaternion.identity); 
	//Debug.Log(bag.transform.position);
	assetCollider = barrel.AddComponent(BoxCollider);
	
	Debug.Log("My Transform is"+this.transform.position);
	//barrelRender = barrel.GetComponent(MeshRenderer);
	//Debug.Log("Barrel Bounds"+barrel.collider.bounds);
	//barrel.transform.localScale = Vector3(1/barrel.collider.bounds.extents.x,1/barrel.collider.bounds.extents.y,1/barrel.collider.bounds.extents.z);
	//barrel.transform.position = Vector3(0.0f,0.0f,0.0f);
	//Debug.Log(barrel.transform.localScale);
	//barrel.renderer.enabled = true;
	//Debug.Log("pos,localpos"+barrel.transform.position+barrel.transform.localPosition);
	
	//var myScale : Vector3 = Vector3(1/barrel.collider.bounds.extents.x,1/barrel.collider.bounds.extents.y,1/barrel.collider.bounds.extents.z);
	var maxScaleAxis : float = Mathf.Max(this.GetComponent.<Collider>().bounds.extents.x,this.GetComponent.<Collider>().bounds.extents.y,this.GetComponent.<Collider>().bounds.extents.z);
	var myScale : Vector3 = Vector3(0.5/maxScaleAxis,0.5/maxScaleAxis,0.5/maxScaleAxis);
	barrel.transform.localScale = myScale;
	
	var myVector : Vector3 = new Vector3(-this.GetComponent.<Collider>().bounds.center.x,-this.GetComponent.<Collider>().bounds.center.y,-this.GetComponent.<Collider>().bounds.center.z);
	barrel.transform.Translate(myVector);
	//Debug.Log("Renderer Bounds"+barrel.collider.bounds);
	/*
	bag.renderer.enabled = true; 
	bag.renderer.bounds.Expand(10.0);
	
	Debug.Log("position is "+bag.transform.position);
	var cube:GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
	cube.renderer.bounds.Expand(2.0);
	Debug.Log("Cube is"+cube.renderer.bounds);
	
	//Add boxcollider to the mesh 
	bagCollider = bag.AddComponent(BoxCollider);
	Debug.Log(bagCollider.bounds);
	*/
}
function Update () {
	
	//Debug.Log(barrel.renderer.bounds);
}