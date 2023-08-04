using System;
using UnityEditor;
using UnityEngine;
using System.Collections;


namespace Picturesque.Darkbringer
{
    [CustomEditor(typeof(DarkbringerEffect))]
    public class DarkBringerEffectEditor : Editor
    {
        SerializedObject serObj;
        void OnEnable()
        {
            serObj = new SerializedObject(target);
        }
        bool experimental = false;
        public override void OnInspectorGUI()
        {
            serObj.Update();
            DarkbringerEffect dbe = target as DarkbringerEffect;
            float nWidth = EditorGUIUtility.currentViewWidth-56;
            EditorGUILayout.LabelField("Choose options, adjust parameters", EditorStyles.miniLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Custom screen size", new GUILayoutOption[] { GUILayout.Width(nWidth) });
            dbe.CustomScreenSize= EditorGUILayout.Toggle(dbe.CustomScreenSize); 
            EditorGUILayout.EndHorizontal();

            if(dbe.CustomScreenSize)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Aspect ratio Black Bars", new GUILayoutOption[] { GUILayout.Width(nWidth - EditorGUI.indentLevel * 15) });
                dbe.AspectRatioBars = EditorGUILayout.Toggle(dbe.AspectRatioBars);
                EditorGUILayout.EndHorizontal();

                dbe.newScreenSize = EditorGUILayout.Vector2Field("Screen size", dbe.newScreenSize);
                dbe.screenStretching = EditorGUILayout.Vector2Field("Pixel stretching (C64/CPC/Others)", dbe.screenStretching);
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("1-bit color", new GUILayoutOption[] { GUILayout.Width(nWidth) });
            dbe.OneBitColor = EditorGUILayout.Toggle(dbe.OneBitColor);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (!dbe.OneBitColor)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Enable dithering", new GUILayoutOption[] { GUILayout.Width(nWidth - EditorGUI.indentLevel * 15) });
                dbe.Dithering = EditorGUILayout.Toggle(dbe.Dithering);
                EditorGUILayout.EndHorizontal();

                if(dbe.Dithering)
                {
                    EditorGUI.indentLevel++;
                    dbe.ColorBleed = EditorGUILayout.Slider("Dither bleed", dbe.ColorBleed, -1.5f, 1.5f);
                    dbe.ColorShift = EditorGUILayout.Slider("Dither brightness", dbe.ColorShift, -1.5f, 1.5f);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Enable custom palette lut", new GUILayoutOption[] { GUILayout.Width(nWidth - EditorGUI.indentLevel * 15) });
                dbe.Paletting = EditorGUILayout.Toggle(dbe.Paletting);
                EditorGUILayout.EndHorizontal();
                if (dbe.Paletting)
                {
                    EditorGUI.indentLevel++;
                    dbe.flatLut = EditorGUILayout.ObjectField("Lut table", dbe.flatLut, typeof(Texture2D), false) as Texture2D;
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.Space();
                
                EditorGUILayout.BeginHorizontal();
               

                EditorGUILayout.LabelField("Experimental features", new GUILayoutOption[] { GUILayout.Width(nWidth) });
                experimental = EditorGUILayout.Toggle(experimental);
                EditorGUILayout.EndHorizontal();
                if(experimental)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField("Warning, these options are experimental.", EditorStyles.miniLabel);
                    EditorGUILayout.LabelField("Hor. lines have flickering with custom screen size.", EditorStyles.miniLabel);
                    EditorGUILayout.LabelField("Vert. lines might not show up at all.", EditorStyles.miniLabel);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Enable horizontal lines", new GUILayoutOption[] { GUILayout.Width(nWidth - EditorGUI.indentLevel * 15) });
                    dbe.HorizontalLines= EditorGUILayout.Toggle(dbe.HorizontalLines);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Enable vertical lines", new GUILayoutOption[] { GUILayout.Width(nWidth - EditorGUI.indentLevel * 15) });
                    dbe.VerticalLines = EditorGUILayout.Toggle(dbe.VerticalLines);
                    EditorGUILayout.EndHorizontal();


                    dbe.LinesMult = EditorGUILayout.Slider("Line multShade", dbe.LinesMult, 0.0f, 1.5f);

                    EditorGUI.indentLevel--;
                }
                else
                {
                    dbe.HorizontalLines = false;
                    dbe.VerticalLines = false;
                }

                dbe.bayerTexture = EditorGUILayout.ObjectField("Bayer texture", dbe.bayerTexture, typeof(Texture2D), false) as Texture2D;
                //experimental = EditorGUILayout.Toggl

            }
            serObj.ApplyModifiedProperties();
            
            if(GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }


    }
}
