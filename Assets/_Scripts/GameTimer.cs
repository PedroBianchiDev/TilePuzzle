using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public float tempoMaximo = 60f;
    private float tempoAtual;
    private bool tempoRodando = false;

    [Header("Referências")]
    public TextMeshProUGUI textoTempo; 
    public PuzzleManager puzzleManager; 

    void Start()
    {
        tempoAtual = tempoMaximo;
    }

    void Update()
    {
        if (tempoRodando)
        {
            tempoAtual -= Time.deltaTime;

            if (textoTempo != null)
            {
                textoTempo.text = Mathf.Ceil(tempoAtual).ToString() + "s";
            }

            if (tempoAtual <= 0)
            {
                tempoAtual = 0;
                PararTempo();

                if (puzzleManager != null)
                {
                    puzzleManager.Derrota();
                }
            }
        }
    }

    public void IniciarTempo()
    {
        tempoRodando = true;
    }

    public void PararTempo()
    {
        tempoRodando = false;
    }
}