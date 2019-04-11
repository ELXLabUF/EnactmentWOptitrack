using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPreviewNoChara : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    GameObject previewImage;
    GameObject backImage;
    Sprite iconImage;
    Color hideColor;
    Color showColor;
    Color showBackColor;

    // Use this for initialization
    void Start()
    {
        previewImage = GameObject.Find("PreviewImageNoChara");
        backImage = GameObject.Find("BackImageNoChara");
        iconImage = gameObject.GetComponent<Image>().sprite;

        hideColor = new Color(1, 1, 1, 0);
        showColor = new Color(1, 1, 1, 1);
        showBackColor = new Color(1, 1, 1, 1);

        previewImage.GetComponent<Image>().color = hideColor;
        backImage.GetComponent<Image>().color = hideColor;
    }

    // Update is called once per frame
    void Update()
    {
    }


    //once mouse enters object
    public void OnPointerEnter(PointerEventData data)
    {
        //Debug.Log("Help");
        previewImage.GetComponent<Image>().color = showColor;
        backImage.GetComponent<Image>().color = showBackColor;
        iconImage = gameObject.GetComponent<Image>().sprite;
        previewImage.GetComponent<Image>().sprite = iconImage;

    }

    //once mouse exits object
    public void OnPointerExit(PointerEventData data)
    {
        previewImage.GetComponent<Image>().color = hideColor;
        backImage.GetComponent<Image>().color = hideColor;
    }

}

