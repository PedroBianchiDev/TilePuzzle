using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configurações do Tabuleiro")]
    public GridLayoutGroup gridLayout;
    public List<Tile> tiles;
    public Transform emptySpace;
    public int emptySpaceIndex = 8;

    [Header("Imagem Fatiada")]
    public Sprite[] puzzleSprites;

    [Header("Configurações de Jogo")]
    private bool jogoAtivo = false;
    public GameTimer gameTimer;

    [Header("Painel Win or Lose")]
    public GameObject painelVitoria;
    public GameObject painelDerrota;


    IEnumerator Start()
    {
        painelVitoria.SetActive(false);
        painelDerrota.SetActive(false);

        yield return new WaitForEndOfFrame();

        if (gridLayout != null) gridLayout.enabled = false;

        for (int i = 0; i < tiles.Count; i++)
        {
            Image tileImage = tiles[i].GetComponent<Image>();
            if (tileImage != null && i < puzzleSprites.Length) tileImage.sprite = puzzleSprites[i];
            tiles[i].Init(i, this);
        }

        Shuffle();

        jogoAtivo = true;

        if (gameTimer != null)
        {
            gameTimer.IniciarTempo();
        }
    }

    public bool TryMoveTile(Tile tileToMove)
    {
        if (!jogoAtivo) return false; 

        int tileIndex = tileToMove.currentPositionIndex;

        if (IsAdjacent(tileIndex, emptySpaceIndex))
        {
            RectTransform emptyRect = emptySpace.GetComponent<RectTransform>();
            Vector2 emptyVisualPos = emptyRect.anchoredPosition;

            emptyRect.anchoredPosition = tileToMove.startPosition;
            tileToMove.MoveToPosition(emptyVisualPos);

            tileToMove.currentPositionIndex = emptySpaceIndex;
            emptySpaceIndex = tileIndex;

            if (ChecarVitoria())
            {
                Vitoria();
            }

            return true;
        }

        return false;
    }

    private bool IsAdjacent(int index1, int index2)
    {
        if (index1 / 3 == index2 / 3 && Mathf.Abs(index1 - index2) == 1) return true;
        if (index1 % 3 == index2 % 3 && Mathf.Abs(index1 - index2) == 3) return true;
        return false;
    }

    private void Shuffle()
    {
        int movimentosDeEmbaralhamento = 100;

        for (int i = 0; i < movimentosDeEmbaralhamento; i++)
        {
            List<Tile> vizinhosValidos = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                if (IsAdjacent(tile.currentPositionIndex, emptySpaceIndex))
                {
                    vizinhosValidos.Add(tile);
                }
            }

            Tile pecaEscolhida = vizinhosValidos[Random.Range(0, vizinhosValidos.Count)];

            RectTransform emptyRect = emptySpace.GetComponent<RectTransform>();
            RectTransform tileRect = pecaEscolhida.GetComponent<RectTransform>();

            Vector2 tempPos = emptyRect.anchoredPosition;
            emptyRect.anchoredPosition = tileRect.anchoredPosition;
            tileRect.anchoredPosition = tempPos;

            int tempIndex = pecaEscolhida.currentPositionIndex;
            pecaEscolhida.currentPositionIndex = emptySpaceIndex;
            emptySpaceIndex = tempIndex;
        }
    }

    private bool ChecarVitoria()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.currentPositionIndex != tile.correctPositionIndex)
            {
                return false;
            }
        }
        return true; 
    }

    private void Vitoria()
    {
        jogoAtivo = false;

        if (gameTimer != null)
        {
            gameTimer.PararTempo();

            int goldGanhoNestaPartida = gameTimer.ObterTempoRestante();

            int goldAcumulado = PlayerPrefs.GetInt("GoldTotal", 0);

            PlayerPrefs.SetInt("GoldTotal", goldAcumulado + goldGanhoNestaPartida);
            PlayerPrefs.Save(); 

            Debug.Log("Você ganhou " + goldGanhoNestaPartida + " moedas! Total agora: " + (goldAcumulado + goldGanhoNestaPartida));
        }

        if (painelVitoria != null) painelVitoria.SetActive(true);
    }

    public void Derrota()
    {
        jogoAtivo = false;
        if (painelDerrota != null) painelDerrota.SetActive(true);
    }
}