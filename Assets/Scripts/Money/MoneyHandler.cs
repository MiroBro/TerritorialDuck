using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    //Money was previously called "Leaf" or "Leaves"
    //The thought was that the Duck would use leafs as currency to make it more cute and removed from capitalism
    //For this reason you may still see variables with names relating to money/currency that are called leaf
    public TextMeshProUGUI leafAmountText;
    public static int leafAmount = 0;

    private void Start()
    {
        leafAmount = 0;
        leafAmountText.text = leafAmount.ToString();
    }
    public void AddMoney(int amount)
    {
        leafAmount += amount;
        leafAmountText.text = leafAmount.ToString();
    }
}
