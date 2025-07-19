using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI txtUI;

    // Start is called before the first frame update
    void Start()
    {
        GoldUIUpdate();
        GameManager.instance.OnGolded += GoldUIUpdate;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnGolded -= GoldUIUpdate;
    }

    private void GoldUIUpdate()
    {
        txtUI.text = GameManager.instance.gold.ToString() + "$";
    }
}
