using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour
{
    //Health was previously called annoyance/patience, the idea being that when the duck ran out of patience/became too annoyed you lost the run.
    //So you may see these names being used in variables!
    public TextMeshProUGUI patiencePoints;
    public Slider patienceSlider;
    public int maxPatience;
    public static int crntPatiencePoints;
    public Material duckMaterial;

    private void Start()
    {
        duckMaterial.SetColor("_Color", defaultcolor);

        patienceSlider.maxValue = maxPatience;
        crntPatiencePoints = maxPatience;
        patienceSlider.value = crntPatiencePoints;
        patiencePoints.text = crntPatiencePoints.ToString();
    }

    public void AlterPatience(int patiencePointsChange)
    {
        crntPatiencePoints += patiencePointsChange;
        
        if (crntPatiencePoints > maxPatience) crntPatiencePoints = maxPatience;

        patiencePoints.text = crntPatiencePoints.ToString();
        patienceSlider.value = crntPatiencePoints;

        if (patiencePointsChange < 0)
            StartCoroutine(HitEffect());
    
        References.Instance.soundHandler.PlayRandomHonk();


        if (crntPatiencePoints <= 0)
        {
            References.Instance.soundHandler.PlayPrimalHonk();
            References.Instance.uiToggler.OpenGiveUp();
            duckMaterial.SetColor("_Color", defaultcolor);
        }
    }
    /*public void EatBread(int breadAmount)
    {
        References.Instance.soundHandler.PlayPickUp();
        References.Instance.frustrationHandler.IncreaseBread(breadAmount);
    }*/

    [ColorUsageAttribute(true, true)]
    public Color hitColor;
    public Color defaultcolor;
    IEnumerator HitEffect()
    {
        duckMaterial.SetColor("_Color", hitColor);
        yield return new WaitForSeconds(0.2f);
        duckMaterial.SetColor("_Color", defaultcolor);
    }
}
