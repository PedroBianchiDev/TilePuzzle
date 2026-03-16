using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configurações do Tabuleiro")]
    public GridLayoutGroup gridLayout;
    public List<Tile> tiles;
    public Transform emptySpace;

    [Header("Imagem Fatiada")]
    public Sprite[] puzzleSprites;

    [Header("Configurações de Jogo")]
    public float tempoMaximo = 60f; 
    private float tempoAtual;
    private bool jogoAtivo = false;

    public int emptySpaceIndex = 8;

    void Start()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Image tileImage = tiles[i].GetComponent<Image>();
            if (tileImage != null && i < puzzleSprites.Length)
            {
                tileImage.sprite = puzzleSprites[i];
            }
            tiles[i].Init(i, this);
        }

        if (gridLayout != null)
        {
            gridLayout.enabled = false;
        }

        Shuffle();

        tempoAtual = tempoMaximo;
        jogoAtivo = true;
    }

    void Update()
    {
        if (jogoAtivo)
        {
            tempoAtual -= Time.deltaTime;

            // textoTempo.text = Mathf.Ceil(tempoAtual).ToString();

            if (tempoAtual <= 0)
            {
                tempoAtual = 0;
                Derrota();
            }
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

            // Troca lógica
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
        Debug.Log("Você Venceu!");
    }

    private void Derrota()
    {
        jogoAtivo = false;
        Debug.Log("Tempo Esgotado! Game Over.");
    }
}