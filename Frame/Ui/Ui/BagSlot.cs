using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private ItemData item;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI mountText;
    [SerializeField] private TextMeshProUGUI nameText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item.id == -1)
        {
            return;
        }
        _ = GameApp.Instance.uiManager.GetUi("BagTipPanel", item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item.id == -1)
        {
            return;
        }
        GameApp.Instance.uiManager.PopUi();
    }

    public void SetSlot(ItemData item,int mount)
    { 
        this.item = item;
        this.image.sprite = item.image;
        this.nameText.text = item.name;
        this.mountText.text = mount.ToString();
    }
}
