using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowGold : MonoBehaviour
{
    public TextMeshProUGUI textoDoGold;

    void Start()
    {
        AtualizarTextoGold();
    }

    public void AtualizarTextoGold()
    {
        int goldAtual = PlayerPrefs.GetInt("GoldTotal", 0);

        if (textoDoGold != null)
        {
            textoDoGold.text = "G: " + goldAtual.ToString();
        }
    }

    public void GastarGold(int valor)
    {
        int goldAtual = PlayerPrefs.GetInt("GoldTotal", 0);
        if (goldAtual >= valor)
        {
            PlayerPrefs.SetInt("GoldTotal", goldAtual - valor);
            PlayerPrefs.Save();
            AtualizarTextoGold();
        }
        else
        {
            Debug.Log("Gold insuficiente!");
        }
    }
}
