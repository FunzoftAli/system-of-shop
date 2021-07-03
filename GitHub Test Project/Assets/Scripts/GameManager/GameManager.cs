using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject _BuyClothes;
    public GameObject _playerToShow;

    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClothesUIPanel(false);
        }
    }

    public void ClothesUIPanel(bool value)
    {
        _BuyClothes.SetActive(value);
        _playerToShow.SetActive(value);
    }
}
