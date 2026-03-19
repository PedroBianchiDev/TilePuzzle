using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolDeBug : MonoBehaviour
{
    [ContextMenu("Adicionar 1000 de Gold")]
    public void DarGold()
    {
        int goldAtual = PlayerPrefs.GetInt("GoldTotal", 0);
        PlayerPrefs.SetInt("GoldTotal", goldAtual + 1000);
        PlayerPrefs.Save();
        Debug.Log("Cheat: 1000 de Gold adicionado! Atualize a tela para ver.");
    }

    [ContextMenu("Zerar Tudo (Resetar Save)")]
    public void ResetarSave()
    {
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.Save();
        Debug.Log("Save resetado! O Gold e as Fases voltaram ao zero.");
    }
}
