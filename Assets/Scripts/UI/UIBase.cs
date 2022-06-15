using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    public virtual Enum GetEnumTransform()
    {
        return null;
    }

    public virtual Enum GetEnumText()
    {
        return null;
    }

    public virtual void OtherSetContent()
    {
    }

    private Enum m_pkEnumTransform;
    public Transform[] m_pkTransforms;

    private Enum m_pkEnumText;
    public Text[] m_pkTexts;


    [ContextMenu("SetContent")]
    public void SetContent()
    {
        m_pkEnumTransform = GetEnumTransform();
        if (m_pkEnumTransform != null)
            SetTransform();

        m_pkEnumText = GetEnumText();
        if (m_pkEnumText != null)
            SetText();

        OtherSetContent();
    }
   
    private void SetTransform()
    {
        Enum pkEnum = m_pkEnumTransform;

        m_pkTransforms = new Transform[(int) Enum.GetNames(pkEnum.GetType()).Length];

        for (int i = 0; i < (int) Enum.GetNames(pkEnum.GetType()).Length; i++)
        {
            string str = Enum.GetName(pkEnum.GetType(), i);

            Transform[] Transforms = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform Transform in Transforms)
            {
                if (Transform.name.Equals(str) == true)
                {
                    if (m_pkTransforms[i] != null)
                    {
                        Debug.LogError("SetTransform Error : " + str);
                    }
                    m_pkTransforms[i] = Transform;
                }
            }
            if (m_pkTransforms[i] == null)
            {
                Debug.LogError("Find not SetTransform Error : " + str);
            }
        }
    }

    private void SetText()
    {
        Enum pkEnum = m_pkEnumText;

        m_pkTexts = new Text[(int) Enum.GetNames(m_pkEnumText.GetType()).Length];

        for (int i = 0; i < (int) Enum.GetNames(pkEnum.GetType()).Length; i++)
        {
            string str = Enum.GetName(pkEnum.GetType(), i);

            Text[] Texts = gameObject.GetComponentsInChildren<Text>();
            foreach (Text Label in Texts)
            {
                if (Label.name.Equals(str) == true)
                {
                    if (m_pkTexts[i] != null)
                    {
                        Debug.LogError("SetLabel Error : " + str);
                    }
                    m_pkTexts[i] = Label;
                }
            }
            if (m_pkTexts[i] == null)
            {
                Debug.LogError("Find not SetLabel Error : " + str);
            }
        }
    }

  
}
