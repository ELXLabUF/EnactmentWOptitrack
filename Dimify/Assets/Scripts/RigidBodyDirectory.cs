using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[Serializable, XmlRoot("RigidBodyDirectory")]

public class RigidBodyDirectory
{
	//[XmlArray("RigidBodies")]
	//[XmlArrayItem("RigidBodyData")]
	[XmlElement("RigidBodyData")]
	public List <RigidBodyData> rigidBodyList = new List<RigidBodyData> ();
	//public RigidBodyData[] rigidBodyDataList;
	/*
	public void Save(string path)
	{
		var serializer = new XmlSerializer (typeof(RigidBodyContainer));
		using (var stream = new FileStream(path,FileMode.Create))
		{
			serializer.Serialize(stream,this);
		}
	}
	public static RigidBodyContainer Load(string path)
	{
		var serializer = new XmlSerializer (typeof(RigidBodyContainer));
		using(var stream = new FileStream(path,FileMode.Open))
		{
			return serializer.Deserialize(stream) as RigidBodyContainer;
		}
	}*/

}