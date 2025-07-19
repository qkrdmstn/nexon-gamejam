using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParryingGaugeUI : MonoBehaviour
{
    public Slider slider;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value != player.parryingGauge / 100f)
        {
            slider.value = Mathf.Lerp(slider.value, player.parryingGauge / 100f, Time.deltaTime);
        }
    }
}
