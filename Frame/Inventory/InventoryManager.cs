using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class InventoryManager
{
    private ItemDataList itemDataList;
    public ItemId[] items;
    private ItemId defautItem;
    private Dictionary<int, ItemData> itemDataDic;
    public InventoryManager(int cap = 50)
    {
        items = new ItemId[cap];
        itemDataDic = new Dictionary<int,ItemData>();
        defautItem = new ItemId()
        { 
            id = -1,
            mount = -1
        };
        itemDataList = GameApp.Instance.resManager.LoadData<ItemDataList>("Assets/So/ItemData/ItemDataListSo");
        Debug.Log(itemDataList.itemDatas[0].name);
        Array.Fill(items,defautItem);
    }
    public ItemData FindItem(int id)
    {
        if (!itemDataDic.ContainsKey(id))
        {
            itemDataDic.Add(id, itemDataList.itemDatas.Find(i => i.id == id));
        }
        return itemDataDic[id];
    }
    public bool AddItem(int id, int mount)
    {
        int index = Array.FindIndex(items, i => i.id == id);
        if (index != -1)
        {
            if (FindItem(id).isStack)
            {
                items[index].mount += 1;//�ɶѻ�ʱ������������
                return true;
            }
        }
        ItemId tempItem = new ItemId()
        {
            id = id,
            mount = mount
        };
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == -1)
            {
                items[i] = tempItem;
                return true;
            }
        }
        _ = GameApp.Instance.uiManager.GetTipUi("Bag is fill!");
        return false;
    }
    public bool RemoveItem(int id, int mount)
    {
        int index = Array.FindIndex(items, i => i.id == id);
        if (index == -1)
        {
            return false;
        }
        if (mount > items[index].mount)
        {
            return false;
        }
        items[index].mount -= 1;
        if (items[index].mount <=0)
        {
            items[index] = defautItem;
        }
        return true;
    }
    public void SortItem()//�ո����ƶ������
    {
    }
    public void SwapItem(int id_1,int id_2)
    {
        if (id_1 == -1 || id_2 == -1)
            return;
        var temp = items[id_2];
        items[id_2] = items[id_1];
        items[id_1] = temp;
    }

    public ItemId[] GetItems() => items;
}
