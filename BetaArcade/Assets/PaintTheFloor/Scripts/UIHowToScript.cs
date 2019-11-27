using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIHowToScript : MonoBehaviour
  {
  public GameObject ControlsParent;

  public string LT;
  public string LB;
  public string LeftStick;
  public string DPAD;
  public string BackButton;
  public string StartButton;
  public string RT;
  public string RB;
  public string Y;
  public string B;
  public string A;
  public string X;
  public string RightStick;

  public void Start()
    {
    foreach(var thisVar in (this as UIHowToScript).GetType().GetFields().Where(x => x.FieldType == typeof(System.String)))
      {
      Debug.Log(thisVar.Name);
      ControlsParent.transform.Find(thisVar.Name).GetComponent<TextMeshProUGUI>().text = thisVar.GetValue(this).ToString();
      }
    }
  }
