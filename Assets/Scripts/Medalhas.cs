using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medalhas : MonoBehaviour
{
    public GameObject panelMedalha; 
    public GameObject[] medalhas;
    private bool[] medalhasDesbloqueadas; 

    private int[] pontosParaMedalha = { 100, 1200, 2100, 3000, 3600, 3900, 4800, 5700, 6600, 7200 };

    private NotificadorDeMissoes notificadorDeMissoes;

    void Start()
    {
        notificadorDeMissoes = FindObjectOfType<NotificadorDeMissoes>();
        
        medalhasDesbloqueadas = new bool[medalhas.Length];
        for (int i = 0; i < medalhas.Length; i++)
        {
            medalhasDesbloqueadas[i] = PlayerPrefs.GetInt($"Medalha_{i}", 0) == 1;
        }
    }

    public void VerificarMedalhasComAtraso()
    {
        StartCoroutine(ExibirMedalhasAposAtraso(0.5f));
    }

    private IEnumerator ExibirMedalhasAposAtraso(float delay)
    {
        yield return new WaitForSeconds(delay);

        int pontuacaoUsuario = GerenciaJogador.instancia.pontuacaoJogador;

        bool mostrouPopUpDeMedalha = false; 

        for (int i = 0; i < pontosParaMedalha.Length; i++)
        {
            if (pontuacaoUsuario >= pontosParaMedalha[i] && !medalhasDesbloqueadas[i])
            {
                AtivarMedalha(panelMedalha, medalhas[i]);
                medalhasDesbloqueadas[i] = true;
                PlayerPrefs.SetInt($"Medalha_{i}", 1);
                PlayerPrefs.Save();
                
                mostrouPopUpDeMedalha = true; 
                break; 
            }
        }
        
        if (!mostrouPopUpDeMedalha)
        {
            ChamarProximoNotificador();
        }
    }

    private void AtivarMedalha(GameObject painel, GameObject medalha)
    {
        if (notificadorDeMissoes != null && notificadorDeMissoes.painelPlacar != null)
        {
            notificadorDeMissoes.painelPlacar.SetActive(false);
        }
        
        painel.SetActive(true);
        medalha.SetActive(true);
    }

    public void ColetarMedalha(GameObject medalha)
    {
        if (medalha != null) medalha.SetActive(false);
        if (panelMedalha != null) panelMedalha.SetActive(false);
        
        ChamarProximoNotificador();
    }

    private void ChamarProximoNotificador()
    {
        if (notificadorDeMissoes != null)
        {
            notificadorDeMissoes.VerificarMissoesImediatamente();
        }
        else
        {
            var notificadorTemp = FindObjectOfType<NotificadorDeMissoes>();
            if (notificadorTemp != null && notificadorTemp.painelPlacar != null)
            {
                notificadorTemp.painelPlacar.SetActive(true);
            }
        }
    }
}