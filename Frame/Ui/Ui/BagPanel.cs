using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Loading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagPanel : UiBase
{
    [SerializeField] private List<BagSlot> slots;
    [SerializeField]private ItemId[] items;
    [SerializeField] int perMount = 25;
    [SerializeField] Transform slotsTransform;
    [SerializeField] int maxPage;
    [SerializeField] int currentPage = 1;
    [SerializeField] Button nextPageBtn;
    [SerializeField] Button prePageBtn;
    [SerializeField] TextMeshProUGUI pageText;
    InventoryManager inventoryManager;
    public void NextPage()
    { 
        if(currentPage<maxPage)
        {
            currentPage += 1;
        }
        UpdateSlot();
    }
    public void LastPage()
    {
        if(currentPage>1)
        {
            currentPage -= 1;
        }
        UpdateSlot();
    }
    public override void OnOpen()
    {
        inventoryManager = GameApp.Instance.inventoryManager;
        items = inventoryManager.GetItems();
        maxPage = items.Length / perMount;
        CreateSlot();
        base.OnOpen();
    }
    public override void OnEnter()
    {
        currentPage = 1;
        nextPageBtn.onClick.AddListener(NextPage);
        prePageBtn.onClick.AddListener(LastPage);
        pageText.text = currentPage.ToString() + "/" + maxPage.ToString();
        base.OnEnter();
    }
    public override void OnExit()
    {
        nextPageBtn.onClick.RemoveListener(NextPage);
        prePageBtn.onClick.RemoveListener(LastPage);
        base.OnExit();
    }
    private void CreateSlot()
    {
        GameObject slotskinRes = GameApp.Instance.resManager.LoadPrefab("Assets/Prefab/Ui/Slot");
        for (int i = perMount * (currentPage - 1); i < perMount * currentPage; i++)
        {
            Debug.Log($"Actual perMount value: {perMount}");
            var slotOb = (GameObject)Instantiate(slotskinRes);
            slotOb.transform.SetParent(slotsTransform);
            var slot = slotOb.GetComponent<BagSlot>();
            slots.Add(slot);
            slot.SetSlot(inventoryManager.FindItem(items[i].id), items[i].mount);
        }
        
    }
    private void UpdateSlot()
    {
        for (int i = perMount * (currentPage - 1); i < perMount * currentPage; i++)
        {
            var slot = slots[i%perMount];
            slot.SetSlot(inventoryManager.FindItem(items[i].id), items[i].mount);
        }
        pageText.text = currentPage.ToString() + "/" + maxPage.ToString();
    }

}
