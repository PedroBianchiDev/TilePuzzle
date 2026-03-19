using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Fontes de Áudio (Audio Sources)")]
    public AudioSource musicSource; 
    public AudioSource sfxSource;   

    [Header("Música da Fase")]
    public AudioClip musicaDeFundo;

    [Header("Efeitos Sonoros (SFX)")]
    public AudioClip somCliqueBotao;
    public AudioClip somMoverPeca;
    public AudioClip somVitoria;
    public AudioClip somDerrota;
    public AudioClip somComprarFase;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicaDeFundo != null)
        {
            musicSource.clip = musicaDeFundo;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void TocarSFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void TocarSomClique() { TocarSFX(somCliqueBotao); }
    public void TocarSomCompra() { TocarSFX(somComprarFase); }
}