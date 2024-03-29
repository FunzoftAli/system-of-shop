﻿using System;
using Assets.HeroEditor4D.Common.CommonScripts;
using HeroEditor.Common;
using HeroEditor.Common.Data;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.CharacterScripts
{
    [Serializable]
    public class CharacterAppearance
    {
        public string Hair = "BuzzCut";
        public string Ears = "Human";
        public string Eyebrows = "Default";
        public string Eyes = "Boy";
        public string Mouth = "Default";
        public string Body = "HumanPants";
        public string Underwear = "MaleUnderwear";
        
        public Color32 HairColor = new Color32(150, 50, 0, 255);
        public Color32 EyesColor = new Color32(0, 200, 255, 255);
        public Color32 BodyColor = new Color32(255, 200, 120, 255);
        public Color32 UnderwearColor = new Color32(120, 100, 80, 255);

        public void Setup(Character4D character)
        {
            character.Parts.ForEach(i => Setup(i));
        }

        public void Setup(CharacterBase character, bool initialize = true)
        {
            character.Hair = Hair.IsEmpty() ? null : character.HairRenderer.GetComponent<SpriteMapping>().FindSprite(character.SpriteCollection.Hair.FindSprites(Hair));
            character.HairRenderer.color = HairColor;
            character.Ears = character.SpriteCollection.Ears.FindSprites(Ears);

            if (character.Expressions.Count > 0)
            {
                character.Expressions[0] = new Expression
                {
                    Name = "Default",
                    Eyebrows = Eyebrows.IsEmpty() ? null : character.EyebrowsRenderer.GetComponent<SpriteMapping>().FindSprite(character.SpriteCollection.Eyebrows.FindSprites(Eyebrows)),
                    Eyes = character.EyesRenderer.GetComponent<SpriteMapping>().FindSprite(character.SpriteCollection.Eyes.FindSprites(Eyes)),
                    Mouth = character.MouthRenderer.GetComponent<SpriteMapping>().FindSprite(character.SpriteCollection.Mouth.FindSprites(Mouth))
                };
            }

            character.EyesRenderer.color = EyesColor;
            character.BodyRenderers.ForEach(i => i.color = BodyColor);
            character.EarsRenderers.ForEach(i => i.color = BodyColor);
            character.Body = character.SpriteCollection.Body.FindSprites(Body);

            if (initialize) character.Initialize();
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static CharacterAppearance FromJson(string json)
        {
            return JsonUtility.FromJson<CharacterAppearance>(json);
        }
    }
}