using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : UiBase
{
    [SerializeField] private Button closeBtn;
    public TextMeshProUGUI tip;
    public override void OnEnter()
    {
        base.OnEnter();
        closeBtn.onClick.AddListener(OnCloseClick);
    }
    public void OnCloseClick()
    {
        GameApp.Instance.uiManager.PopUi();
        Destroy(gameObject);
    }
    public override void OnExit()
    {
        base.OnExit();
        closeBtn.onClick.RemoveListener(OnCloseClick);

    }
}
