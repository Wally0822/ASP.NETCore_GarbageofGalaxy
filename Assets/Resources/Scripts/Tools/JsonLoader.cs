using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class JsonLoader
{
    public void Load()
    {
        LoadAllJsonFiles();
    }

    private void LoadAllJsonFiles()
    {
        string resourcesPath = Path.Combine(Application.dataPath, "Resources", "ResourcesTable");
        DirectoryInfo directoryInfo = new DirectoryInfo(resourcesPath);

        foreach (FileInfo file in directoryInfo.GetFiles("*.json"))
        {
            string jsonFileName = Path.GetFileNameWithoutExtension(file.Name);
            string jsonFileContent = Resources.Load<TextAsset>(Path.Combine("ResourcesTable", jsonFileName)).text;

            DeserializeAndAddToList(jsonFileName, jsonFileContent);
            Debug.Log($"Load Successed {jsonFileName}.json");
        }
    }

    private void DeserializeAndAddToList(string className, string jsonFileContent)
    {
        string classQualifiedName = className;

        JArray jsonArray = JArray.Parse(jsonFileContent);
        IList<object> deserializedItems = new List<object>();

        foreach (JObject jsonObject in jsonArray)
        {
            object deserializedItem = JsonConvert.DeserializeObject(jsonObject.ToString(), Type.GetType(classQualifiedName));
            deserializedItems.Add(deserializedItem);
        }

        object itemList = GetStaticField(classQualifiedName, "table");
        if (itemList is IDictionary items)
        {
            items.Clear();
            foreach (var deserializedItem in deserializedItems)
            {
                var idField = deserializedItem.GetType().GetField("ID");
                var idValue = (int)idField.GetValue(deserializedItem);
                items.Add(idValue, deserializedItem);
            }
        }
    }

    // �ҽ� �ڵ忡�� GetStaticProperty�� GetStaticField�� ����
    private object GetStaticField(string classQualifiedName, string fieldName)
    {
        System.Type type = Type.GetType(classQualifiedName);
        if (type == null) return null;

        System.Reflection.FieldInfo fieldInfo = type.GetField(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        if (fieldInfo == null) return null;

        return fieldInfo.GetValue(null);
    }
}
