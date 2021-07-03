using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum _SlotType
{
    Armor, Hair
}
public class addCloth_In_Slots : MonoBehaviour
{
    public _SlotType _Slot;
    public clothDetails _Body;
    public Transform parent;
    public GameObject PrefabSlots;
    public GameObject _btnPrice;
    private RectTransform _rect;


    private List<GameObject> GarbgeList = new List<GameObject>();


    #region RunTimeSaveSystem
    private List<bool> _isBuyyedClothes = new List<bool>();
    private List<bool> _isBuyyedHairs = new List<bool>();
    #endregion
    private void OnEnable()
    {
        switch (_Slot)
        {
            case _SlotType.Armor:
                _FunArmor();
                break;
            case _SlotType.Hair:
                _FunHair();
                break;
            default:
                break;
        }

        _resetScrollView();
    }
    private void _resetScrollView()
    {
        _rect = this.GetComponent<RectTransform>();
        _rect.anchoredPosition = new Vector2(0, transform.position.y - 1200.0f);
    }
    private void _FunHair()
    {
        GarbgeList.Clear();
        int totalHairCount = _Body._Hair.Count;
        for (int i = 0; i < totalHairCount; i++)
        {
            GameObject _slot = Instantiate(PrefabSlots, parent);
            Image _image = _slot.transform.GetChild(0).GetComponent<Image>();
            btnClick _btnClick = _slot.transform.GetChild(0).gameObject.AddComponent<btnClick>();
            _image.sprite = _Body._Hair[i];
            _btnClick.id = i;
            GarbgeList.Add(_btnClick.gameObject);
            if (_isBuyyedHairs.Count != 0)
            {
                _btnClick.isBuyyed = _isBuyyedHairs[i];
            }
        }
    }

    void _FunArmor()
    {
        GarbgeList.Clear();
        int totalFrontCount = _Body._clothBody.Count;
        for (int i = 0; i < totalFrontCount; i++)
        {
            GameObject _slot = Instantiate(PrefabSlots, parent);
            Image _image = _slot.transform.GetChild(0).GetComponent<Image>();
            btnClick _btnClick = _slot.transform.GetChild(0).gameObject.AddComponent<btnClick>();
            _image.sprite = _Body._clothBody[i];
            _btnClick.id = i;
            GarbgeList.Add(_btnClick.gameObject);
            if (_isBuyyedClothes.Count != 0)
            {
                _btnClick.isBuyyed = _isBuyyedClothes[i];
            }
        }
    }


    private void OnDisable()
    {
        _isBuyyedClothes.Clear();
        _isBuyyedHairs.Clear();
        GameObject obj = GarbgeList.Find(x => x.GetComponent<btnClick>().isSelected);
        btnClick bC = obj.GetComponent<btnClick>();
        if (bC.isBuyyed) { }
        else
            bC.ResetPreviousBuyyed();

        for (int i = 0; i < GarbgeList.Count; i++)
        {
            if (_Slot == _SlotType.Armor)
                _isBuyyedClothes.Add(GarbgeList[i].GetComponent<btnClick>().isBuyyed);
            if (_Slot == _SlotType.Hair)
                _isBuyyedHairs.Add(GarbgeList[i].GetComponent<btnClick>().isBuyyed);

            Destroy(GarbgeList[i].transform.parent.gameObject);
        }
        GarbgeList.Clear();
    }
}


public class btnClick : MonoBehaviour
{
    public bool isSelected;
    public bool isBuyyed;
    public int id;
    internal static int previousID;
    Button btn;
    public static GameObject previous;
    private addCloth_In_Slots _slots;
    internal GameObject _pricePanel;
    private void OnEnable()
    {
        _slots = this.GetComponentInParent<addCloth_In_Slots>();
        _pricePanel = GameObject.Find("pricePanel").gameObject;
        btn = this.GetComponent<Button>();
        switch (_slots._Slot)
        {
            case _SlotType.Armor:
                btn.onClick.AddListener(OnSlotClickGetPrice);
                btn.onClick.AddListener(OnClickSelect);
                btn.onClick.AddListener(OnSlotClickArmor);
                btn.onClick.AddListener(OnSlotClickArms);
                btn.onClick.AddListener(OnSlotClickLegs);
                btn.onClick.AddListener(OnSlotClickSleeves);
                break;
            case _SlotType.Hair:
                btn.onClick.AddListener(OnClickSelect);
                btn.onClick.AddListener(OnSlotClickHair);
                btn.onClick.AddListener(OnSlotClickHairGetPrice);
                break;
            default:
                break;
        }
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.0f);
        if (id == 0 || isBuyyed == true)
        {
            previousID = id;
            isBuyyed = true;
            _pricePanel.GetComponent<CanvasGroup>().alpha = 0;
            OnClickSelect();
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private void OnClickSelect()
    {
        SelectedItem();
    }

    private void SelectedItem()
    {
        if (previous == null)
        {
            previous = this.gameObject;
            previous.transform.GetChild(0).gameObject.SetActive(true);
            previous.GetComponent<btnClick>().isSelected = true;
        }
        else
        {
            previous.transform.GetChild(0).gameObject.SetActive(false);
            previous.GetComponent<btnClick>().isSelected = false;
            previous = this.gameObject;
            previous.transform.GetChild(0).gameObject.SetActive(true);
            previous.GetComponent<btnClick>().isSelected = true;
        }
    }

    BodyHandler GeneralThings(string nameBody)
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        Transform _getBodyTrans = _player.transform.Find(nameBody);
        BodyHandler _handler = _getBodyTrans.GetComponent<BodyHandler>();
        return _handler;
    }

    private void OnSlotClickHair()
    {
        Sprite _sprite = _slots._Body._Hair[id];
        BodyHandler _handler = GeneralThings("Front");
        _handler.ChangeHairSprite(_sprite);
    }
    void OnSlotClickArmor()
    {
        Sprite _sprite = _slots._Body._clothBody[id];

        BodyHandler _fronthandler = GeneralThings("Front");
        //BodyHandler __backhandler = GeneralThings("Back");
        //BodyHandler _lefthandler = GeneralThings("Left");
        //BodyHandler _righthandler = GeneralThings("Right");
        _fronthandler.ChangeBodySprite(_sprite);
        //__backhandler.ChangeSprite(_sprite);
        //_lefthandler.ChangeSprite(_sprite);
        //_righthandler.ChangeSprite(_sprite);
        //   SelectedItem();
    }
    void OnSlotClickArms()
    {
        Sprite _spriteL = _slots._Body._clothArmsL[id];
        Sprite _spriteR = _slots._Body._clothArmsR[id];
        BodyHandler _fronthandler = GeneralThings("Front");
        //BodyHandler __backhandler = GeneralThings("Back");
        //BodyHandler _lefthandler = GeneralThings("Left");
        //BodyHandler _righthandler = GeneralThings("Right");
        _fronthandler.ChangeArmSprite(_spriteL, _spriteR);

        //__backhandler.ChangeSprite(_sprite);
        //_lefthandler.ChangeSprite(_sprite);
        //_righthandler.ChangeSprite(_sprite);
    }
    void OnSlotClickLegs()
    {
        Sprite _spriteL = _slots._Body._clothLegL[id];
        Sprite _spriteR = _slots._Body._clothLegR[id];
        BodyHandler _fronthandler = GeneralThings("Front");
        //BodyHandler __backhandler = GeneralThings("Back");
        //BodyHandler _lefthandler = GeneralThings("Left");
        //BodyHandler _righthandler = GeneralThings("Right");
        _fronthandler.ChangeLegSprite(_spriteL, _spriteR);
        //__backhandler.ChangeSprite(_sprite);
        //_lefthandler.ChangeSprite(_sprite);
        //_righthandler.ChangeSprite(_sprite);
    }

    void OnSlotClickSleeves()
    {
        Sprite _spriteL = _slots._Body._clothSleeveL[id];
        Sprite _spriteR = _slots._Body._clothSleeveR[id];
        BodyHandler _fronthandler = GeneralThings("Front");
        //BodyHandler __backhandler = GeneralThings("Back");
        //BodyHandler _lefthandler = GeneralThings("Left");
        //BodyHandler _righthandler = GeneralThings("Right");
        _fronthandler.ChangeSleevesSprite(_spriteL, _spriteR);
        //__backhandler.ChangeSprite(_sprite);
        //_lefthandler.ChangeSprite(_sprite);
        //_righthandler.ChangeSprite(_sprite);
    }

    void OnSlotClickGetPrice()
    {
        if (isBuyyed)
            _pricePanel.GetComponent<CanvasGroup>().alpha = 0;
        else
            _pricePanel.GetComponent<CanvasGroup>().alpha = 1;

        Text txtPrice = _slots._btnPrice.GetComponentInChildren<Text>();
        txtPrice.text = "" + _slots._Body.GetPriceClothes(id);
    }

    void OnSlotClickHairGetPrice()
    {
        if (isBuyyed)
            _pricePanel.GetComponent<CanvasGroup>().alpha = 0;
        else
            _pricePanel.GetComponent<CanvasGroup>().alpha = 1;

        Text txtPrice = _slots._btnPrice.GetComponentInChildren<Text>();
        txtPrice.text = "" + _slots._Body.GetPriceHair(id);
    }

    public void ResetPreviousBuyyed()
    {
        BodyHandler _handler = GeneralThings("Front");

        if (_slots._Slot == _SlotType.Hair)
        {
            Sprite _Hair = _slots._Body._Hair[previousID];
            _handler.ChangeHairSprite(_Hair);
            return;
        }

        Sprite _sprite = _slots._Body._clothBody[previousID];

        Sprite _ArmL = _slots._Body._clothArmsL[previousID];
        Sprite _ArmR = _slots._Body._clothArmsR[previousID];

        Sprite _sleeveL = _slots._Body._clothSleeveL[previousID];
        Sprite _sleeveR = _slots._Body._clothSleeveR[previousID];

        Sprite _LegL = _slots._Body._clothLegL[previousID];
        Sprite _LegR = _slots._Body._clothLegR[previousID];

        _handler.ChangeBodySprite(_sprite);
        _handler.ChangeArmSprite(_ArmL, _ArmR);
        _handler.ChangeSleevesSprite(_sleeveL, _sleeveR);
        _handler.ChangeLegSprite(_LegL, _LegR);

    }

}
