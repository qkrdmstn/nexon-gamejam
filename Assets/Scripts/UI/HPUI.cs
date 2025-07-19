using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    public TextMeshProUGUI txtUI;

    // Start is called before the first frame update
    void Start()
    {
        HPUIUpdate();
        GameManager.instance.OnHPChanged += HPUIUpdate;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnHPChanged -= HPUIUpdate;
    }

    private void HPUIUpdate()
    {
        txtUI.text = "HP: " + GameManager.instance.curHP.ToString();
    }
}
