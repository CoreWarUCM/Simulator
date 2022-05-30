// using System;
// using System.IO;
// using UnityEngine;
//
// namespace Editor
// {
//     using UnityEditor;
//     using UnityEditor.UIElements;
//     using UnityEngine.UIElements;
//
//     [CustomEditor(typeof(Parser))]
//     public class ParserInspector : Editor
//     {
//         public override VisualElement CreateInspectorGUI()
//         {
//             VisualElement myInspector = new VisualElement();
//         
//             myInspector.Add(new Label("Simple PMARS debugger parser"));
//         
//             UnityEngine.UIElements.PopupWindow w = new UnityEngine.UIElements.PopupWindow
//             {
//                 name = "name",
//                 tooltip = "tooltip",
//                 text = "Warrior 1"
//             };
//             var info = new DirectoryInfo($"{Application.dataPath}/SampleWarriors/");
//             var fileInfo = info.GetFiles();
//         
//             String[] warriorList = new string[fileInfo.Length/2];
//             int j = 0;
//             foreach (var file in fileInfo)
//             {
//                 if(file.Name.Contains(".meta"))
//                     continue;
//                 warriorList[j++] = file.Name;
//             }
//             for (int i = 0; i < warriorList.Length; i++)
//             {
//                 var i1 = i;
//                 
//                 Button b = new Button(() => ((Parser) this.target).setWarrior1(warriorList[i1]))
//                 {
//                     text = $"{warriorList[i]}"
//                 };
//                 w.Add(b);
//             }
//             
//             UnityEngine.UIElements.PopupWindow w2 = new UnityEngine.UIElements.PopupWindow
//             {
//                 name = "name",
//                 tooltip = "tooltip",
//                 text = "Warrior 2"
//             };
//             for (int i = 0; i < warriorList.Length; i++)
//             {
//                 var i1 = i;
//                 
//                 Button b = new Button(() => ((Parser) this.target).setWarrior2(warriorList[i1]))
//                 {
//                     text = $"{warriorList[i]}"
//                 };
//                 w2.Add(b);
//             }
//             
//             myInspector.Add(w);
//             myInspector.Add(w2);
//             
//             return myInspector;
//         }
//     }
// }