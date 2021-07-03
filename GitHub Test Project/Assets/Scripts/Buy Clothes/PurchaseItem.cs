using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PurchaseItem : MonoBehaviour
{
    public Text txtPrice;
    public Text txtCurrent;
    public GameObject _Notification;
    public void _btnPurchase()
    {
        int price = Convert.ToInt32(txtPrice.text);
        int currentValue = Convert.ToInt32(txtCurrent.text);
        int result = currentValue - price < 0 ? -1 : currentValue - price;
        if (result < 0)
        {
            ShowNotification();
            return;
        }
        txtCurrent.text = "" + result;

        FindGridSystem();
    }

    private void FindGridSystem()
    {
        btnClick selectedObject = null;
        GameObject obj = GameObject.Find("GridSystem");
        List<btnClick> _objs = new List<btnClick>();
        _objs.AddRange(obj.transform.GetComponentsInChildren<btnClick>());
        for (int i = 0; i < _objs.Count; i++)
        {
            Transform _trans = _objs[i].transform.GetChild(0);
            if (_trans.gameObject.activeInHierarchy)
            {
                selectedObject = _objs[i];
            }
        };
        if (selectedObject != null)
        {
            selectedObject.isBuyyed = true;
            btnClick.previousID = selectedObject.id;
            selectedObject._pricePanel.GetComponent<CanvasGroup>().alpha = 0;
            selectedObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void ShowNotification()
    {
        _Notification.SetActive(true);
    }
}
