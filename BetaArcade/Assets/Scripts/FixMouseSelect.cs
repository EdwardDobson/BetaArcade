using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class FixMouseSelect : MonoBehaviour
  {
  public GameObject m_lastSelect;

  // Update is called once per frame
  void Update()
    {
    if(EventSystem.current != null)
      {
      if (EventSystem.current.currentSelectedGameObject == null)
        {
        EventSystem.current.SetSelectedGameObject(m_lastSelect);
        }
      else
        {
        m_lastSelect = EventSystem.current.currentSelectedGameObject;
        }
      }
    }
  }
