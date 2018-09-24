using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


[Serializable]
public class RigidBodyData 
{
	[XmlAttribute("name")]
	public string name;
	public string objFilePath; 
	public string imgFilePath; 
	public float tx,ty,tz, rx,ry,rz, sx,sy,sz;
	public int physicalObjectId;
    public bool materialIncluded; 
}


