using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class DropMeSlideDetailNoChara : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject holder;
    private int charaID;
    private int itemID;
    private int backdropID;

    public void OnEnable()
    {

    }

    public void OnDrop(PointerEventData data)
    {

        if (this.GetComponent<SlideDataNoChara>().getLock() == false)
        {
            Sprite dropSprite = GetDropSprite(data);

            string dropTag = GetDropTag(data);

            int itemIndex;
            itemIndex = compareIndex(dropTag, dropSprite);

            switch (dropTag)
            {
                case "chara":
                    holder = this.transform.Find("CharaHolder(Clone)").gameObject;
                    this.GetComponent<SlideDataNoChara>().setChara(itemIndex);
                    break;

                case "item":
                    holder = this.transform.Find("ItemHolder(Clone)").gameObject;
                    this.GetComponent<SlideDataNoChara>().setItem(itemIndex);
                    break;

                case "backdrop":
                    holder = this.transform.Find("BackdropHolder(Clone)").gameObject;
                    this.GetComponent<SlideDataNoChara>().setBackdrop(itemIndex);
                    break;

                case null:
                    holder = null;
                    break;
                default:
                    break;
            }

            if (holder != null) { holder.GetComponent<Image>().overrideSprite = dropSprite; holder.GetComponent<Image>().color = Color.white; }

        }

    }

    public void OnPointerEnter(PointerEventData data)
    {

        string dropTag = GetDropTag(data);
        if (this.GetComponent<SlideDataNoChara>().getLock() == false)
        {
            switch (dropTag)
            {
                case "chara":
                    this.transform.Find("CharaHolder(Clone)").gameObject.GetComponent<Image>().color = Color.gray;
                    break;

                case "item":
                    this.transform.Find("ItemHolder(Clone)").gameObject.GetComponent<Image>().color = Color.gray;
                    break;

                case "backdrop":
                    this.transform.Find("BackdropHolder(Clone)").gameObject.GetComponent<Image>().color = Color.gray;
                    break;

                default:
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {

        string dropTag = GetDropTag(data);

        if (this.GetComponent<SlideDataNoChara>().getLock() == false)
        {
            switch (dropTag)
            {
                case "chara":
                    this.transform.Find("CharaHolder(Clone)").gameObject.GetComponent<Image>().color = Color.white;
                    break;

                case "item":
                    this.transform.Find("ItemHolder(Clone)").gameObject.GetComponent<Image>().color = Color.white;
                    break;

                case "backdrop":
                    this.transform.Find("BackdropHolder(Clone)").gameObject.GetComponent<Image>().color = Color.white;
                    break;

                default:
                    break;
            }
        }
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

    int compareIndex(string tag, Sprite sprite)
    {
        GameObject[] objArray = GameObject.FindGameObjectWithTag("object_arrays_NoChara").GetComponent<ObjectArrayNoChara>().Items; 
        switch (tag)
        {
            case "item":
                objArray = GameObject.FindGameObjectWithTag("object_arrays_NoChara").GetComponent<ObjectArrayNoChara>().Items;
                break;
            case "chara":
                //objArray = GameObject.FindGameObjectWithTag("object_arrays_NoChara").GetComponent<ObjectArrayNoChara>().CharaPoseSets;
                break;
            case "backdrop":
                objArray = GameObject.FindGameObjectWithTag("object_arrays_NoChara").GetComponent<ObjectArrayNoChara>().Backdrops;
                break;
            default:
                break;
        }

        for (int i = 0; i < objArray.Length; i++)
        {
            if (objArray[i].GetComponent<Icon>().iconSprite == sprite) { return i; }
        }

        return 0;
    }


}
