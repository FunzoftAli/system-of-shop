using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHandler : MonoBehaviour
{
    public SpriteRenderer Hair;
    public SpriteRenderer Armor;
    public SpriteRenderer ArmL;
    public SpriteRenderer ArmR;
    public SpriteRenderer LegL;
    public SpriteRenderer LegR;
    public SpriteRenderer SleeveL;
    public SpriteRenderer SleeveR;


    public void ChangeBodySprite(Sprite _arm)
    {
        Armor.sprite = _arm;
    }
    public void ChangeArmSprite(Sprite _armL, Sprite _armR)
    {
        ArmL.sprite = _armL;
        ArmR.sprite = _armR;
    }

    public void ChangeLegSprite(Sprite _LegL, Sprite _LegR)
    {
        LegL.sprite = _LegL;
        LegR.sprite = _LegR;
    }
    public void ChangeSleevesSprite(Sprite _L, Sprite _R)
    {
        SleeveL.sprite = _L;
        SleeveR.sprite = _R;
    }

    public void ChangeHairSprite(Sprite _hair)
    {
        Hair.sprite = _hair;
    }

}
