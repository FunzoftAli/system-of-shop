using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cloth", menuName = "Clothes/Add Cloth", order = 0)]
public class clothDetails : ScriptableObject
{
    public List<int> _cloth_id
    {
        get
        {
            List<int> id = new List<int>();
            for (int i = 0; i < _clothBody.Count; i++)
            {
                id.Add(i);
            }
            return id;
        }
    }

    private List<int> _cloth_Price
    {
        get
        {
            List<int> id = new List<int>();
            for (int i = 0; i < _clothBody.Count; i++)
            {
                int price = i * 100;
                id.Add(price);
            }
            return id;
        }
    }
    private List<int> _Hear_Price
    {
        get
        {
            List<int> id = new List<int>();
            for (int i = 0; i < _Hair.Count; i++)
            {
                int price = i * 100;
                id.Add(price);
            }
            return id;
        }
    }
    public int GetPriceClothes(int index)
    {
        return _cloth_Price[index];
    }

    public int GetPriceHair(int index)
    {
        return _Hear_Price[index];
    }
    public List<Sprite> _Hair = new List<Sprite>();
    public List<Sprite> _clothBody = new List<Sprite>();
    public List<Sprite> _clothArmsL = new List<Sprite>();
    public List<Sprite> _clothArmsR = new List<Sprite>();
    public List<Sprite> _clothLegL = new List<Sprite>();
    public List<Sprite> _clothLegR = new List<Sprite>();
    public List<Sprite> _clothSleeveL = new List<Sprite>();
    public List<Sprite> _clothSleeveR = new List<Sprite>();

}


