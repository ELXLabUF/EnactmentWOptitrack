using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    GameObject previewImage;
    GameObject previewImagePhys;
    GameObject backImage;
    Sprite iconImage;
    Sprite physIcon;
    Color hideColor;
    Color showColor;
    Color showBackColor;
    public Sprite PhysPreview;

	// Use this for initialization
	void Start () {
        previewImage = GameObject.Find("PreviewImage");
        previewImagePhys = GameObject.Find("PreviewImagePhys");
        backImage = GameObject.Find("BackImage");
        iconImage = gameObject.GetComponent<Image>().sprite;
        physIcon = gameObject.GetComponent<ItemPreview>().PhysPreview;

        hideColor = new Color(1,1,1,0);
        showColor = new Color(1,1,1,1);
        showBackColor = new Color(1,1,1,1);

        previewImage.GetComponent<Image>().color = hideColor;
        previewImagePhys.GetComponent<Image>().color = hideColor;
        backImage.GetComponent<Image>().color = hideColor;
    }
	
	// Update is called once per frame
	void Update () {
    }


    //once mouse enters object
    public void OnPointerEnter(PointerEventData data)
    {
        //Debug.Log("Help");
        previewImage.GetComponent<Image>().color = showColor;
        previewImagePhys.GetComponent<Image>().color = showColor;
        backImage.GetComponent<Image>().color = showBackColor;
        iconImage = gameObject.GetComponent<Image>().sprite;
        previewImage.GetComponent<Image>().sprite = iconImage;
        physIcon = gameObject.GetComponent<ItemPreview>().PhysPreview;
        previewImagePhys.GetComponent<Image>().sprite = physIcon;

    }

    //once mouse exits object
    public void OnPointerExit(PointerEventData data)
    {
        previewImage.GetComponent<Image>().color = hideColor;
        previewImagePhys.GetComponent<Image>().color = hideColor;
        backImage.GetComponent<Image>().color = hideColor;
    }

    }
