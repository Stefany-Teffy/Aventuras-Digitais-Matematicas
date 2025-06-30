using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Placar : MonoBehaviour
{
    public GameObject placa;
    public static GameObject placar;
    public GameObject estrela3;
    public GameObject estrela1;
    public GameObject estrela2;
    public GameObject textoPerdeu;
    public GameObject textoVenceu;
    public GameObject botaoProxNivel;
    public static GameObject perdeu;
    public static GameObject venceu;
    public static GameObject proxNivel;
    public GerenciadorEstrelas gerenciadorEstrelas;


    void Start()
    {
       placar = placa;
       perdeu = textoPerdeu;
       venceu = textoVenceu;
       proxNivel = botaoProxNivel;
       gerenciadorEstrelas = GameObject.Find("GerenciadorEstrelas").GetComponent<GerenciadorEstrelas>();
       Debug.Log ("placar: " + placar.transform.name);

    }

    public static void Desativar(){
        placar.SetActive (false);
        Debug.Log ("Placar desativado");
    }

    public static void Ativar(GerenciadorEstrelas gerenciadorEstrelas, int valortotal, int numeroGerado, int tentativas, int fases, int nAtual)
    {
        if (nAtual == 12)
        {
            proxNivel.SetActive(false);
        }
        placar.SetActive(true);
        Debug.Log("Placar ativado");
        int quantidadeEstrelasDesativadas = 0;

        if (tentativas == 0 && fases == 1)
        {
            if (valortotal != numeroGerado)
            {
                venceu.SetActive(false);
                perdeu.SetActive(true);
                proxNivel.SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaEsquerda").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 3;
            }
            else
            {
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 2;
            }
        }
        else if (tentativas == 0 && fases == 2)
        {
            if (valortotal != numeroGerado)
            {
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                GameObject.FindGameObjectWithTag("EstrelaDireita").SetActive(false);
                quantidadeEstrelasDesativadas = 2;
            }
            else
            {
                GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
                quantidadeEstrelasDesativadas = 1;
            }
        }
        else if (tentativas == 0 && fases == 3 && valortotal != numeroGerado)
        {
            GameObject.FindGameObjectWithTag("EstrelaMeio").SetActive(false);
            quantidadeEstrelasDesativadas = 1;
        }
        gerenciadorEstrelas.DefinirPorNivel(nAtual, quantidadeEstrelasDesativadas);

        if ((nAtual == 1 || nAtual == 4 || nAtual == 7) && quantidadeEstrelasDesativadas <= 2)
        {
            string nomeJogador = GerenciaJogador.instancia.nomeJogador;
            if (nAtual == 1)
            {
                string chaveMissao = "Bloco1_Missao1Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 1, bloco 1, concluída! Nível 1 completado com pelo menos 1 estrela.");
            }
            else if (nAtual == 4)
            {
                string chaveMissao = "Bloco2_Missao1Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 1, bloco 2, concluída! Nível 4 completado com pelo menos 1 estrela.");
            }
            else
            {
                string chaveMissao = "Bloco3_Missao1Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 1, bloco 3, concluída! Nível 7 completado com pelo menos 1 estrela.");
            }
            PlayerPrefs.Save();
        }

        if ((nAtual == 2 || nAtual == 5 || nAtual == 8) && quantidadeEstrelasDesativadas <= 1)
        {
            string nomeJogador = GerenciaJogador.instancia.nomeJogador;

            if (nAtual == 2)
            {
                string chaveMissao = "Bloco1_Missao2Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 2, bloco 1, concluída! Nível 2 completado com pelo menos 2 estrelas.");
            }
            else if (nAtual == 5)
            {
                string chaveMissao = "Bloco2_Missao2Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 2, bloco 2, concluída! Nível 5 completado com pelo menos 2 estrelas.");
            }
            else
            {
                string chaveMissao = "Bloco3_Missao2Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 2, bloco 3, concluída! Nível 8 completado com pelo menos 2 estrelas.");
            }
            PlayerPrefs.Save();

        }

        if ((nAtual == 3 || nAtual == 6 || nAtual == 9) && quantidadeEstrelasDesativadas == 0)
        {
            string nomeJogador = GerenciaJogador.instancia.nomeJogador;

            if (nAtual == 3)
            {
                string chaveMissao = "Bloco1_Missao3Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 3, bloco 1, concluída! Nível 3 completado com pelo menos 3 estrelas.");
            }
            else if (nAtual == 6)
            {
                string chaveMissao = "Bloco2_Missao3Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 3, bloco 2, concluída! Nível 6 completado com pelo menos 3 estrelas.");
            }
            else
            {
                string chaveMissao = "Bloco3_Missao3Concluida_" + nomeJogador;
                PlayerPrefs.SetInt(chaveMissao, 1);
                Debug.Log("Missão 3, bloco 3, concluída! Nível 9 completado com pelo menos 3 estrelas.");
            }
            PlayerPrefs.Save();
        }

        int estrelasAtivas = 3 - quantidadeEstrelasDesativadas;
        int pontosNivel = estrelasAtivas * 100;

        TextMeshProUGUI placarText = GameObject.Find("PlacarText")?.GetComponent<TextMeshProUGUI>();
        placarText.text = "+" + pontosNivel + " XP";
        GerenciaJogador.instancia.AtualizarPontuacao(pontosNivel);

        Debug.Log($"Nível {nAtual} completo! Estrelas ativas: {estrelasAtivas}, Pontos obtidos: {pontosNivel}");

        Medalhas medalhasScript = GameObject.FindObjectOfType<Medalhas>();
        if (medalhasScript != null)
        {
            medalhasScript.VerificarMedalhasComAtraso(); 
        }
    }
}
