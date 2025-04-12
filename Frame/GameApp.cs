using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoSingleTon<GameApp>
{
    public static string id = "";
    public UiManager uiManager;
    public AudioManager audioManager;
    public ResManager resManager;
    public InventoryManager inventoryManager;
    public EventCenter eventCenter;
    public GameData gameData;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        uiManager = new UiManager();
        audioManager = new AudioManager();
        resManager = new ResManager();
        inventoryManager = new InventoryManager(56);
        eventCenter = new EventCenter();
        LoadData();

    }
    private void LoadData()
    {
        var gameData = GameSave.LoadByJson<GameData>("GameData.json");
        if (gameData!=null)
        {
            inventoryManager.items = gameData.items;
        }
    }
    private void Update()
    {
        //NetManager.Update();
    }    private void Start()
    {
        //NetManager.AddEventListener(NetEvent.Close, OnConnectClose);
        //NetManager.AddMsgListener("MsgKick", OnMsgKick);
        //_ = uiManager.GetUi("LoginPanel");
         _ = uiManager.GetUi("BagPanel");
        inventoryManager.RemoveItem(2, 2);
    }

    private void OnMsgKick(MsgBase msgBase)
    {
        _ = uiManager.GetUi("TipPanel", "Be Kick");
    }

    private void OnConnectClose(string err)
    {
        Debug.Log("�Ͽ�����");
    }
}
