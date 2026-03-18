using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BtnBlockLevel : MonoBehaviour
{
    [Header("Configurações da Fase")]
    public int idDaCena; 
    public string nomeDoSaveDaFase = "Fase2"; 
    public int precoEmGold = 50; 
    public bool liberadaPorPadrao = false; 

    [Header("Referências Visuais")]
    public TextMeshProUGUI textoDoBotao; 
    public LoadingScene loadingScript;
    public GameObject iconeCadeado; 
    public ShowGold mostradorDeGold; 

    void Start()
    {
        AtualizarVisualDoBotao();
    }

    public void AtualizarVisualDoBotao()
    {
        int estadoSalvo = liberadaPorPadrao ? 1 : PlayerPrefs.GetInt(nomeDoSaveDaFase, 0);

        if (estadoSalvo == 1)
        {
            textoDoBotao.text = "Jogar Fase " + idDaCena;

            if (iconeCadeado != null)
            {
                iconeCadeado.SetActive(false);
            }
        }
        else
        {
            textoDoBotao.text = "Comprar: " + precoEmGold + "G";

            if (iconeCadeado != null)
            {
                iconeCadeado.SetActive(true);
            }
        }
    }

    public void AoClicarNoBotao()
    {
        int estadoSalvo = liberadaPorPadrao ? 1 : PlayerPrefs.GetInt(nomeDoSaveDaFase, 0);

        if (estadoSalvo == 1)
        {
            if (loadingScript != null) loadingScript.LoadScene(idDaCena);
        }
        else
        {
            int goldAtual = PlayerPrefs.GetInt("GoldTotal", 0);

            if (goldAtual >= precoEmGold)
            {
                PlayerPrefs.SetInt("GoldTotal", goldAtual - precoEmGold);
                PlayerPrefs.SetInt(nomeDoSaveDaFase, 1);
                PlayerPrefs.Save();

                AtualizarVisualDoBotao();

                if (mostradorDeGold != null) mostradorDeGold.AtualizarTextoGold();

                Debug.Log("Fase comprada com sucesso!");
            }
            else
            {
                Debug.Log("Você não tem Gold suficiente!");
            }
        }
    }
}
