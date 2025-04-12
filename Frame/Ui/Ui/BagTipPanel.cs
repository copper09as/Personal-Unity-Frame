using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagTipPanel : UiBase
{   
    [SerializeField] private ItemData item;
    [SerializeField] private TextMeshProUGUI desText;
    public void SetItem(ItemData item)
    {
        desText.text = item.description;
    }
}
