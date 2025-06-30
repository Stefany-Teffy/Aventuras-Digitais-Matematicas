using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Missao
{
    public string nome;
    public string descricao;
    public bool concluida;
    public bool ativa;
    public string chavePlayerPrefs;
    public int estrelasNecessarias;
    public AudioClip audio;
}

[System.Serializable]
public class BlocoMissoes
{
    public string nomeBloco;
    public List<Missao> missoes = new List<Missao>();
    public GameObject botaoBloco;
    public TextMeshProUGUI textoBotao;
    public TextMeshProUGUI percentualBotao;
    public TextMeshProUGUI descricaoText;
    public GameObject iconSom;
    public GameObject fundoEscuro;
    public GameObject fundoRosa;
    public GameObject fundoVerde;
    public GameObject popUp;
    public GameObject emblema;
    public GameObject coletar;
    public GameObject botReturn;
    public int missaoAtiva = 0;
    public GameObject fundoTextCinza;
    public GameObject fundoTextRosa;
    public GameObject fundoTextLaranja;
    public GameObject fundoTextAmarelo;
    public GameObject fundoTextVerde;
}

public class GerenciadorMissoes : MonoBehaviour
{
    public List<BlocoMissoes> blocosMissoes = new List<BlocoMissoes>();
    public AudioSource audioSource;
    string nomeJogador;

    void Start()
    {
        if (GerenciaJogador.instancia == null)
        {
            Debug.LogError("GerenciaJogador.instancia não foi encontrado! O GerenciadorMissoes não pode funcionar.");
            return;
        }
        nomeJogador = GerenciaJogador.instancia.nomeJogador;

        InicializarBlocosMissoes();
        VerificarMissoes();
        DeterminarMissaoAtivaCorreta();
        AtualizarUI();
    }

    private void InicializarBlocosMissoes()
    {
        Debug.Log($"Inicializando blocos de missões para o jogador: {nomeJogador}");

        if (blocosMissoes.Count > 0)
        {
            // Bloco 1
            BlocoMissoes bloco1 = blocosMissoes[0];
            bloco1.missoes = new List<Missao>
            {
                new Missao { nome = "Missão 1", descricao = "Jogue o nível 1 (unidades) e conquiste no mínimo 1 estrela.", chavePlayerPrefs = $"Bloco1_Missao1Concluida_{nomeJogador}", estrelasNecessarias = 1, audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco1") },
                new Missao { nome = "Missão 2", descricao = "Jogue o nível 2 (dezenas) e conquiste no mínimo 2 estrelas.", chavePlayerPrefs = $"Bloco1_Missao2Concluida_{nomeJogador}", estrelasNecessarias = 2, audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco1") },
                new Missao { nome = "Missão 3", descricao = "Jogue o nível 3 (centenas) e conquiste o máximo de estrelas.", chavePlayerPrefs = $"Bloco1_Missao3Concluida_{nomeJogador}", estrelasNecessarias = 3, audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco1") }
            };
            
            // Bloco 2
            BlocoMissoes bloco2 = blocosMissoes[1];
            bloco2.missoes = new List<Missao>
            {
                new Missao { nome = "Missão 1", descricao = "Jogue o nível 4 (soma de unidades) e conquiste no mínimo 1 estrela.", chavePlayerPrefs = $"Bloco2_Missao1Concluida_{nomeJogador}", estrelasNecessarias = 1, audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco2") },
                new Missao { nome = "Missão 2", descricao = "Jogue o nível 5 (soma de dezenas) e conquiste no mínimo 2 estrelas.", chavePlayerPrefs = $"Bloco2_Missao2Concluida_{nomeJogador}", estrelasNecessarias = 2, audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco2") },
                new Missao { nome = "Missão 3", descricao = "Jogue o nível 6 (soma de centenas) e conquiste o máximo de estrelas.", chavePlayerPrefs = $"Bloco2_Missao3Concluida_{nomeJogador}", estrelasNecessarias = 3, audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco2") }
            };

            // Bloco 3
            BlocoMissoes bloco3 = blocosMissoes[2];
            bloco3.missoes = new List<Missao>
            {
                new Missao { nome = "Missão 1", descricao = "Jogue o nível 7 (subtração de unidades) e conquiste no mínimo 1 estrela.", chavePlayerPrefs = $"Bloco3_Missao1Concluida_{nomeJogador}", estrelasNecessarias = 1, audio = Resources.Load<AudioClip>("Sounds/sommissao1bloco3") },
                new Missao { nome = "Missão 2", descricao = "Jogue o nível 8 (subtração de dezenas) e conquiste no mínimo 2 estrelas.", chavePlayerPrefs = $"Bloco3_Missao2Concluida_{nomeJogador}", estrelasNecessarias = 2, audio = Resources.Load<AudioClip>("Sounds/sommissao2bloco3") },
                new Missao { nome = "Missão 3", descricao = "Jogue o nível 9 (subtração de centenas) e conquiste o máximo de estrelas.", chavePlayerPrefs = $"Bloco3_Missao3Concluida_{nomeJogador}", estrelasNecessarias = 3, audio = Resources.Load<AudioClip>("Sounds/sommissao3bloco3") }
            };
        }
    }

    private void VerificarMissoes()
    {
        foreach (var bloco in blocosMissoes)
        {
            foreach (var missao in bloco.missoes)
            {
                missao.concluida = PlayerPrefs.GetInt(missao.chavePlayerPrefs, 0) == 1;
            }
        }
    }

    private void DeterminarMissaoAtivaCorreta()
    {
        foreach (var bloco in blocosMissoes)
        {
            string chaveBlocoConcluido = $"Bloco{blocosMissoes.IndexOf(bloco) + 1}_Concluido_{nomeJogador}";
            if (PlayerPrefs.GetInt(chaveBlocoConcluido, 0) == 1)
            {
                bloco.missaoAtiva = -1; 
                SalvarEstadoMissaoAtiva(bloco);
                continue;
            }
            bool encontrouAtiva = false;
            for (int i = 0; i < bloco.missoes.Count; i++)
            {
                if (!bloco.missoes[i].concluida)
                {
                    bloco.missaoAtiva = i;
                    encontrouAtiva = true;
                    break;
                }
            }

            if (!encontrouAtiva)
            {
                bloco.missaoAtiva = bloco.missoes.Count - 1;
            }

            SalvarEstadoMissaoAtiva(bloco);
        }
    }
    
    private void SalvarEstadoMissaoAtiva(BlocoMissoes bloco)
    {
        string chaveMissaoAtiva = $"MissaoAtiva_{bloco.nomeBloco}_{nomeJogador}";
        PlayerPrefs.SetInt(chaveMissaoAtiva, bloco.missaoAtiva);
        PlayerPrefs.Save();
    }

    private void AtualizarUI()
    {
        foreach (var bloco in blocosMissoes)
        {
            AtualizarBotaoBloco(bloco);
        }
    }

    private void AtualizarBotaoBloco(BlocoMissoes bloco)
    {
        if (bloco.botaoBloco == null) return;

        bloco.fundoTextCinza.SetActive(bloco.missaoAtiva == 0 || bloco.missaoAtiva == -1);
        bloco.fundoTextRosa.SetActive(bloco.missaoAtiva == 1);
        bloco.fundoTextLaranja.SetActive(bloco.missaoAtiva == 2);
        bloco.fundoTextAmarelo.SetActive(bloco.missaoAtiva == 3);
        bloco.fundoTextVerde.SetActive(bloco.missaoAtiva == 4);

        bloco.fundoEscuro.SetActive(false);
        bloco.fundoRosa.SetActive(false);
        bloco.fundoVerde.SetActive(false);

        if (bloco.missaoAtiva == -1)
        {
            bloco.botaoBloco.SetActive(false);
            bloco.emblema.SetActive(false);
            bloco.descricaoText.text = "Todas as missões desse bloco completas!";
            if (bloco.iconSom != null)
            {
                bloco.iconSom.SetActive(false);
            }
        }
        else if (bloco.missaoAtiva >= 0 && bloco.missaoAtiva < bloco.missoes.Count)
        {
            if (bloco.iconSom != null)
            {
                bloco.iconSom.SetActive(true);
            }
            Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];
            bloco.descricaoText.text = missaoAtual.descricao;

            int indiceBlocoParaAudio = blocosMissoes.IndexOf(bloco);
            bloco.descricaoText.GetComponent<Button>().onClick.RemoveAllListeners();
            bloco.descricaoText.GetComponent<Button>().onClick.AddListener(() => TocarAudioMissaoAtiva(indiceBlocoParaAudio));

            if (!missaoAtual.concluida)
            {
                // ---- MISSÃO ATIVA, MAS NÃO CONCLUÍDA ----
                bloco.fundoEscuro.SetActive(true);
                bloco.textoBotao.text = "Completar";
                
                int indiceBloco = blocosMissoes.IndexOf(bloco);
                int numeroDoNivel = (indiceBloco * 3) + (bloco.missaoAtiva + 1);
                string chaveEstrelasNivel = $"starDesNiveis_{nomeJogador}_{numeroDoNivel}";
                int estrelasObtidas = 3 - PlayerPrefs.GetInt(chaveEstrelasNivel, 3);
                
                float progresso = 0f;
                if (missaoAtual.estrelasNecessarias > 0)
                {
                    progresso = (Mathf.Min((float)estrelasObtidas, missaoAtual.estrelasNecessarias) / missaoAtual.estrelasNecessarias) * 100f;
                }
                bloco.percentualBotao.text = $"{progresso:F0}%";
                
                bloco.botaoBloco.GetComponent<Button>().onClick.RemoveAllListeners();
                bloco.botaoBloco.GetComponent<Button>().onClick.AddListener(() => changeScenes.proxCena("Naturais"));
            }
            else
            {
                // ---- MISSÃO CONCLUÍDA, AGUARDANDO AÇÃO ----
                bloco.percentualBotao.text = "100%";
                
                int indiceBlocoParaClique = blocosMissoes.IndexOf(bloco);
                bloco.botaoBloco.GetComponent<Button>().onClick.RemoveAllListeners();
                bloco.botaoBloco.GetComponent<Button>().onClick.AddListener(() => BotaoBlocoClicado(indiceBlocoParaClique));

                if (bloco.missaoAtiva < bloco.missoes.Count - 1)
                {
                    bloco.fundoRosa.SetActive(true);
                    bloco.textoBotao.text = "Próxima";
                }
                else
                {
                    bloco.fundoVerde.SetActive(true);
                    bloco.textoBotao.text = "Concluído";
                }
            }
        }
    }

    public void BotaoBlocoClicado(int indiceBloco)
    {
        if (indiceBloco < 0 || indiceBloco >= blocosMissoes.Count) return;
        
        var bloco = blocosMissoes[indiceBloco];
        if (bloco.missaoAtiva < 0 || bloco.missaoAtiva >= bloco.missoes.Count) return;

        Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];

        if (missaoAtual.concluida)
        {
            if (bloco.missaoAtiva < bloco.missoes.Count - 1)
            {
                bloco.missaoAtiva++;
                SalvarEstadoMissaoAtiva(bloco);
                AtualizarUI();
            }
            else
            {
                string chaveBlocoConcluido = $"Bloco{indiceBloco + 1}_Concluido_{nomeJogador}";
                PlayerPrefs.SetInt(chaveBlocoConcluido, 1);
                PlayerPrefs.SetInt($"EmblemaBloco{indiceBloco + 1}Concluido", 1); 
                PlayerPrefs.Save();

                bloco.missaoAtiva = -1;
                SalvarEstadoMissaoAtiva(bloco);

                bloco.botReturn.SetActive(false);
                bloco.coletar.SetActive(true);
                bloco.botaoBloco.SetActive(false);
                AbrirPopUpDoBloco(indiceBloco);
            
                AtualizarUI();
            }
        }
    }

    public void AbrirPopUpDoBloco(int indiceBloco)
    {
        var bloco = blocosMissoes[indiceBloco];
        if (bloco.popUp != null) bloco.popUp.SetActive(true);
    }

    public void FecharPopUpDoBloco(int indiceBloco)
    {
        var bloco = blocosMissoes[indiceBloco];
        if (bloco.popUp != null) bloco.popUp.SetActive(false);
    }

    public void TocarAudioMissaoAtiva(int indiceBloco)
    {
        if (indiceBloco < 0 || indiceBloco >= blocosMissoes.Count) return;

        var bloco = blocosMissoes[indiceBloco];
        if (bloco.missaoAtiva < 0 || bloco.missaoAtiva >= bloco.missoes.Count) return;

        Missao missaoAtual = bloco.missoes[bloco.missaoAtiva];
        if (missaoAtual.audio != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = missaoAtual.audio;
            audioSource.Play();
        }
    }
}