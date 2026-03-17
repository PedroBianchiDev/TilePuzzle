using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Painéis do Menu")]
    public GameObject painelPrincipal;
    public GameObject painelFases;

    public void AbrirSelecaoDeFases()
    {
        painelPrincipal.SetActive(false);
        painelFases.SetActive(true);
    }

    public void VoltarAoMenuPrincipal()
    {
        painelFases.SetActive(false);
        painelPrincipal.SetActive(true);
    }
}