using System;
using UnityEngine;
using System.Collections;

public class UITest : UIBase
{

    public enum E_TRANSFORM
    {
        
    }

    public enum E_TEXT
    {
        TEST,
    }

    public override Enum GetEnumTransform() { return new E_TRANSFORM(); }
    public override Enum GetEnumText() { return new E_TEXT(); }
    public override void OtherSetContent()
    {

    }

    void Start()
    {
    }

    public void Awake()
    {
        m_pkTexts[(int)E_TEXT.TEST].text = "하이";
    }

    public void Confirm()
    {
        
    }
}
