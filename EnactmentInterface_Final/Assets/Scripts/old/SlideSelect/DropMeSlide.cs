using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropMeSlide : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image containerImage;
    public Image receivingImage;
    private Color normalColor;
    public Color highlightColor = Color.yellow;
    public Canvas mainCanvas;
    public GameObject begBase;
    public GameObject midBase;
    public GameObject endBase;
    private GameObject slideBase;
    public List<GameObject> slideList;
    private int currentListID = 0;
    public GameObject[] slides;
    public string thisTag;

    public void OnEnable()
    {
        if (containerImage != null)
            normalColor = containerImage.color;
    }

    public void OnDrop(PointerEventData data)
    {
        containerImage.color = normalColor;

        // if (receivingImage == null)
        //     return;

        //Sprite dropSprite = GetDropSprite(data);
        //  if (dropSprite != null)
        //     receivingImage.overrideSprite = dropSprite;
        string dropTag = GetDropTag(data);

        switch (dropTag)
        {
            case "beg_src":
                slideBase = begBase;
                break;
            case "mid_src":
                slideBase = midBase;
                break;
            case "end_src":
                slideBase = endBase;
                break;
            default:
                slideBase = null;
                break;
        }

        if (slideBase != null)
        {
            GameObject newSlide = (GameObject)Instantiate(slideBase, transform.position, transform.rotation);

            newSlide.GetComponent<SlideSelectSlide>().assignID(currentListID);
            newSlide.tag = thisTag;
            newSlide.transform.SetParent(mainCanvas.transform);

            //slideList.Add(newSlide);

            slides = GameObject.FindGameObjectsWithTag(thisTag);

            for (int i = 0; i < currentListID + 1; i++)
            {
                //  var slideLength = (dropLength - ((currentListID + 1) * buffer) - buffer) /(currentListID + 1);
                //var slideX = buffer * (currentListID + 1) + (newSlide.GetComponent<SlideSelectSlide>().listID * slideLength);
                //slides[i].GetComponent<SlideSelectSlide>().newPosition(buffer, dropLength, currentListID + 1);
            }
            currentListID++;
        }

    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (containerImage == null)
            return;

        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null)
            containerImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (containerImage == null)
            return;

        containerImage.color = normalColor;
    }

    private Sprite GetDropSprite(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcImage = originalObj.GetComponent<Image>();
        if (srcImage == null)
            return null;

        return srcImage.sprite;
    }

    private string GetDropTag(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcTag = originalObj.tag;
        if (srcTag == null)
            return null;

        return srcTag;
    }
}
