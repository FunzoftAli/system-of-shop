using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _BuyClothes;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _BuyClothes.SetActive(!_BuyClothes.activeInHierarchy);
        }
    }
}
