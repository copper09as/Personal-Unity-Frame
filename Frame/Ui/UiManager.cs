using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UiManager
{
    MyStack<UiBase> uiStack = new MyStack<UiBase>();
    Dictionary<string, UiBase> UiDic = new Dictionary<string, UiBase>();

    /// <summary>
    /// ����ָ��ui
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<UiBase> GetUi(string name)
    {
        UiBase ui = null;

        if (!UiDic.ContainsKey(name))
        {
            var handle = Addressables.InstantiateAsync("Assets/Prefab/Ui/" + name);
            await handle.Task;
            ui = handle.Result.GetComponent<UiBase>();
            UiDic[name] = ui;
            handle.Result.transform.SetParent(GameObject.Find("Canvas").transform);
            handle.Result.transform.localPosition = Vector3.zero;
        }
        else
        {
            ui = UiDic[name];
            Debug.Log("ui��������");
        }
        if (ui != null)
        {
            if (uiStack.Contains(ui))
            {
                if (uiStack.SetLast(ui))
                {
                    uiStack.Find(uiStack.Count() - 2).OnExit();
                    ui.OnEnter();//�ڶ��ν�����enter
                }
            }
            else
            {
                if (uiStack.Count() > 0)
                {
                    uiStack.Peek().OnExit();
                }
                uiStack.Push(ui);
                ui.OnOpen();//��һ�ν�����Open
            }
        }
        return ui;
    }
    public async Task<TipPanel> GetUi(string name, string tip = "")
    {
        TipPanel ui = null;
        var handle = Addressables.InstantiateAsync("Assets/Prefab/Ui/" + name);
        await handle.Task;
        ui = handle.Result.GetComponent<TipPanel>();
        handle.Result.transform.SetParent(GameObject.Find("Canvas").transform);
        handle.Result.transform.localPosition = Vector3.zero;

        if (ui != null)
        {
            if (uiStack.Count() > 0)
            {
                uiStack.Peek().OnExit();
            }
            uiStack.Push(ui);
            ui.OnOpen();
        }
        if (tip != "")
        {
            ui.GetComponent<TipPanel>().tip.text = tip;
        }
        return ui;
    }
    public async Task<BagTipPanel> GetUi(string name, ItemData item)
    {
        BagTipPanel ui = null;

        if (!UiDic.ContainsKey(name))
        {
            var handle = Addressables.InstantiateAsync("Assets/Prefab/Ui/" + name);
            await handle.Task;
            ui = handle.Result.GetComponent<BagTipPanel>();
            UiDic[name] = ui;
            handle.Result.transform.SetParent(GameObject.Find("Canvas").transform);
            handle.Result.transform.localPosition = Vector3.zero;
        }
        else
        {
            ui = (BagTipPanel)UiDic[name];
            Debug.Log("ui��������");
        }
        if (ui != null)
        {
            if (uiStack.Contains(ui))
            {
                if (uiStack.SetLast(ui))
                {
                    uiStack.Find(uiStack.Count() - 2).OnExit();
                    ui.OnEnter();//�ڶ��ν�����enter
                }
            }
            else
            {
                uiStack.Push(ui);
                ui.OnOpen();//��һ�ν�����Open
            }
        }
        ui.SetItem(item);
        return ui;
    }
    /// <summary>
    /// ������һ��ui
    /// </summary>
    public void PopUi()
    {
        if (uiStack.Count() > 0)
        {
            uiStack.Pop().OnExit();
            if (uiStack.Count() > 0)
            {
                uiStack.Peek().OnEnter();
            }
        }
        //Debug.Log(uiStack.Count());
    }
    public void CloseUi(string name)
    {

    }
    /// <summary>
    /// �ر�����ui
    /// </summary>
    private void CloseAll()
    {
        while (uiStack.Count() > 0)
        {
            //uiStack.Pop().OnClose();
        }
    }
}
