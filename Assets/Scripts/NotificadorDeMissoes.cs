// Arquivo: NotificadorDeMissoes.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NotificadorDeMissoes : MonoBehaviour
{
    private List<BlocoMissoes> blocosDefinicoes = new List<BlocoMissoes>();

    public GameObject painelPlacar;
    public GameObject painelPopUp;

    public TextMeshProUGUI textoMissaoCompleta;
    public TextMeshProUGUI textoBlocoCompleto;
    public TextMeshProUGUI textoDescricao;

    public Button botaoContinuar;
    public Button botaoVerMissoes;
    public Button botaoColetarEmblema;

    void Start()
    {
        if (painelPopUp != null) painelPopUp.SetActive(false);
        InicializarDefinicoesMissoes();

        botaoContinuar.onClick.AddListener(FecharPopUpEMostrarPlacar);
        botaoVerMissoes.onClick.AddListener(IrParaMissoes);
        botaoColetarEmblema.onClick.AddListener(IrParaMissoes);
    }
    
    public void VerificarMissoesComAtraso()
    {
        StartCoroutine(ExecutarVerificacao(0.75f)); 
    }

    public void VerificarMissoesImediatamente()
    {
        StartCoroutine(ExecutarVerificacao(0.0f)); 
    }

    private IEnumerator ExecutarVerificacao(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        foreach (var bloco in blocosDefinicoes)
        {
            foreach (var missao in bloco.missoes)
            {
                string chaveNotificado = "Notificado_" + missao.chavePlayerPrefs;

                if (PlayerPrefs.GetInt(missao.chavePlayerPrefs, 0) == 1 && PlayerPrefs.GetInt(chaveNotificado, 0) == 0)
                {

                    PlayerPrefs.SetInt(chaveNotificado, 1);
                    PlayerPrefs.Save();

                    if (painelPlacar != null) painelPlacar.SetActive(false);
                    ConfigurarEAtivarPopUp(missao, bloco);
                    
                    yield break; 
                }
            }
        }
        
        if (painelPlacar != null && !painelPlacar.activeSelf)
        {
            painelPlacar.SetActive(true);
        }
    }

    private void InicializarDefinicoesMissoes()
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador;

        blocosDefinicoes.Clear(); 

        BlocoMissoes bloco1 = new BlocoMissoes();
        bloco1.missoes = new List<Missao>
        {
            new Missao { chavePlayerPrefs = $"Bloco1_Missao1Concluida_{nomeJogador}", descricao = "Jogue o nível 1 (unidades) e conquiste no mínimo 1 estrela." },
            new Missao { chavePlayerPrefs = $"Bloco1_Missao2Concluida_{nomeJogador}", descricao = "Jogue o nível 2 (dezenas) e conquiste no mínimo 2 estrelas." },
            new Missao { chavePlayerPrefs = $"Bloco1_Missao3Concluida_{nomeJogador}", descricao = "Jogue o nível 3 (centenas) e conquiste o máximo de estrelas." }
        };
        blocosDefinicoes.Add(bloco1);

        BlocoMissoes bloco2 = new BlocoMissoes();
        bloco2.missoes = new List<Missao>
        {
            new Missao { chavePlayerPrefs = $"Bloco2_Missao1Concluida_{nomeJogador}", descricao = "Jogue o nível 4 (soma de unidades) e conquiste no mínimo 1 estrela." },
            new Missao { chavePlayerPrefs = $"Bloco2_Missao2Concluida_{nomeJogador}", descricao = "Jogue o nível 5 (soma de dezenas) e conquiste no mínimo 2 estrelas." },
            new Missao { chavePlayerPrefs = $"Bloco2_Missao3Concluida_{nomeJogador}", descricao = "Jogue o nível 6 (soma de centenas) e conquiste o máximo de estrelas." }
        };
        blocosDefinicoes.Add(bloco2);
        
        BlocoMissoes bloco3 = new BlocoMissoes();
        bloco3.missoes = new List<Missao>
        {
            new Missao { chavePlayerPrefs = $"Bloco3_Missao1Concluida_{nomeJogador}", descricao = "Jogue o nível 7 (subtração de unidades) e conquiste no mínimo 1 estrela." },
            new Missao { chavePlayerPrefs = $"Bloco3_Missao2Concluida_{nomeJogador}", descricao = "Jogue o nível 8 (subtração de dezenas) e conquiste no mínimo 2 estrelas." },
            new Missao { chavePlayerPrefs = $"Bloco3_Missao3Concluida_{nomeJogador}", descricao = "Jogue o nível 9 (subtração de centenas) e conquiste o máximo de estrelas." }
        };
        blocosDefinicoes.Add(bloco3);
    }

    private void ConfigurarEAtivarPopUp(Missao missaoConcluida, BlocoMissoes blocoPai)
    {
        bool blocoFoiCompleto = VerificarSeBlocoEstaCompleto(blocoPai);

        if (blocoFoiCompleto)
        {
            textoMissaoCompleta.gameObject.SetActive(false);
            textoBlocoCompleto.gameObject.SetActive(true);
            botaoContinuar.gameObject.SetActive(false);
            botaoVerMissoes.gameObject.SetActive(false);
            botaoColetarEmblema.gameObject.SetActive(true);
        }
        else
        {
            textoMissaoCompleta.gameObject.SetActive(true);
            textoBlocoCompleto.gameObject.SetActive(false);
            textoDescricao.text = $"<i>{missaoConcluida.descricao}</i>";
            botaoContinuar.gameObject.SetActive(true);
            botaoVerMissoes.gameObject.SetActive(true);
            botaoColetarEmblema.gameObject.SetActive(false);
        }
        
        painelPopUp.SetActive(true);
    }
    
    private bool VerificarSeBlocoEstaCompleto(BlocoMissoes bloco)
    {
        foreach (var m in bloco.missoes)
        {
            if (PlayerPrefs.GetInt(m.chavePlayerPrefs, 0) == 0)
            {
                return false; 
            }
        }
        return true; 
    }

    public void FecharPopUpEMostrarPlacar()
    {
        if (painelPopUp != null) painelPopUp.SetActive(false);
        if (painelPlacar != null) painelPlacar.SetActive(true);
    }

    public void IrParaMissoes()
    {
        changeScenes.proxCena("Missoes");
    }
}