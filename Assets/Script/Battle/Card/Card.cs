using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler    // IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] int nowIndex;
    [SerializeField] Vector2 offset;
    [SerializeField] Vector3 original;

    private void Awake()
    {
        nowIndex = transform.GetSiblingIndex();
    }

    //鼠标进入、离开、点击
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(CardController.Ins.CanBeUse(gameObject.name));
        if (CardController.Ins.CanBeUse(gameObject.name))
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.SetSiblingIndex(10);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardController.Ins.CanBeUse(gameObject.name))
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            transform.SetSiblingIndex(nowIndex);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardController.Ins.CanBeUse(gameObject.name))
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            CardController.Ins.UseCard(gameObject.name);
        }
    }

    //拖拽
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (CardController.Ins.CanBeUse(gameObject.name))
    //    {
    //        original = transform.position;
    //        Vector3 v3=(0,0,0);
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
    //                                        eventData.position, eventData.enterEventCamera, out (Vector2)v3);
    //        //RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(),
    //                                       // eventData.position, eventData.enterEventCamera, out v3);
    //        offset = transform.position - v3;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (CardController.Ins.CanBeUse(gameObject.name))
    //    {
    //        transform.position = Input.mousePosition + offset;
    //    }
    //}

    public void OnEndDrag(PointerEventData eventData)//-600-600 0-350
    {
        if (CardController.Ins.CanBeUse(gameObject.name))
        {
            Collider2D collider = Physics2D.OverlapBox(transform.position, new Vector2(200, 300), 1 << LayerMask.NameToLayer("UI"));
            transform.position = original;
            //Debug.Log("检测到物体" + collider.gameObject.name);
            if (collider != null && collider.tag == "Enemy")
            {
                CardController.Ins.UseCard(gameObject.name);
            }
            //transform.position = original;
        }
    }
}
