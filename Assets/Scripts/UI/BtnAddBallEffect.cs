using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnAddBallEffect : MonoBehaviour
{
    public Enums.PowerUpEffect newEffect;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textStat;
    public Image itemImage;

    public void AddEffect()
    {
        References.Instance.soundHandler.PlayClickBtn();
        AllEffects.AddEffect(newEffect);
        //References.Instance.effectHandler.AddEffect(newEffect);
        References.Instance.uiToggler.CloseUpgraedUI();
    }


    public void SetName(string name)
    {
        textName.text = name;
    }

    public void SetDescription(string desc)
    {
        textDesc.text = desc;
    }

    public void SetImage(Sprite newImage)
    {
        itemImage.sprite = newImage;
    }

    public void SetStatChange(string stat)
    {
        textStat.text = stat;

        /*
Mag: 1 -> <color=#47FF16>36</color>
Pip: 2 -> <color=#FF917C>-1</color>
         */
    }

    public void SetEffect(Enums.PowerUpEffect newEffect)
    {
        this.newEffect = newEffect;
    }
}
