﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.HeroEditor4D.FantasyInventory.Scripts.Data;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.HeroEditor4D.FantasyInventory.Scripts
{
    /// <summary>
    /// Global object that automatically grabs all required images.
    /// </summary>
    [CreateAssetMenu(fileName = "IconCollection", menuName = "ScriptableObjects/IconCollection")]
    public class IconCollection : ScriptableObject
    {
        public List<Sprite> Backgrounds;

        public static Dictionary<string, IconCollection> Instances = new Dictionary<string, IconCollection>();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            var instance = Resources.Load<IconCollection>("IconCollection");

            if (instance == null) throw new Exception("Error loading IconCollection scriptable object.");

            if (!Instances.ContainsKey(instance.Id))
            {
                Instances.Add(instance.Id, instance);
            }
        }

        public string Id;
        public Object ScanFolder;
		public List<ItemIcon> Icons;
        public Sprite DefaultItemIcon;
        
        public Sprite GetIcon(string path)
        {
            var icon = Icons.SingleOrDefault(i => i.Path == path);

            if (icon == null && path != null) Debug.LogWarning("Icon not found: " + path);

            return icon != null ? icon.Sprite : DefaultItemIcon;
        }

		#if UNITY_EDITOR

		public void Refresh()
		{
			var root = AssetDatabase.GetAssetPath(ScanFolder);
		    var pattern = $@"{new DirectoryInfo(root).Name}\\(.*).png";
            var files = Directory.GetFiles(root, "*.png", SearchOption.AllDirectories).ToList();

			Icons.Clear();

			foreach (var file in files)
			{
				var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(file);
			    var match = Regex.Match(file, pattern);
                var icon = new ItemIcon { Name = Path.GetFileNameWithoutExtension(file), Path = match.Groups[1].Value.Replace("\\", "/"), Sprite = sprite };

                if (Icons.Any(i => i.Path == icon.Path))
				{
					Debug.LogErrorFormat($"Duplicated icon: {icon.Path}");
				}
				else
				{
					Icons.Add(icon);
				}
			}

			Icons = Icons.OrderBy(i => i.Name).ToList();
            EditorUtility.SetDirty(this);
        }

        #endif
    }
}