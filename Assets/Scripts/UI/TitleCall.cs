using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleCall : MonoBehaviour
{
    UI_SceneTitle ui_title;
 
    IEnumerator Start()
    {
        ui_title = FindObjectOfType<UI_SceneTitle>();
        if (ui_title == null)
        {
            ui_title = UIManager.Instance.CreateObject<UI_SceneTitle>("UI_SceneTitle", EnumTypes.LayoutType.First);
            yield return new WaitUntil(() => ui_title != null);
        }

        ui_title.OnShow();
    }
}