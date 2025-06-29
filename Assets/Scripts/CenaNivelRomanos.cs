using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CenaNivelRomanos : MonoBehaviour
{
    void Start()
    {
         string nomeJogador = GerenciaJogador.instancia.nomeJogador;
        // Verifica e inicializa os valores necessários para cada nível
        for (int i = 1; i <= 12; i++)
        {
            string chaveBloqueado = "rom_bloqueado_" + nomeJogador + "_" + i;
            string chaveEstrelas = "starRomDesNiveis_" + nomeJogador + "_" + i;

            if (!PlayerPrefs.HasKey(chaveBloqueado))
            {
                PlayerPrefs.SetInt(chaveBloqueado, 1); // Todos os níveis iniciam bloqueados
            }

            if (!PlayerPrefs.HasKey(chaveEstrelas))
            {
                PlayerPrefs.SetInt(chaveEstrelas, 3); // Inicia com 3 estrelas disponíveis
            }
            else
            {
                // Se já existir, não redefine para evitar resetar as estrelas
                Debug.Log("chaveEstrelas" + i + " já está definido.");
            }

            GameObject nivelObj = FindRecursively("Nivel" + i, transform);

            if (nivelObj != null)
            {
                AtualizarEstadoBloqueio(nivelObj, i, nomeJogador);

                // Desativa o botão do nível por padrão, exceto para o nível 1
                nivelObj.GetComponent<Button>().interactable = (i == 1);
            }
            else
            {
                Debug.LogError("Objeto 'Nivel" + i + "' não encontrado!");
            }
        }


        // Verifica e ativa botões dos níveis com base nas estrelas do nível anterior
         for (int i = 2; i <= 12; i++)
        {
            string chaveEstrelasAnterior = "starRomDesNiveis_" + nomeJogador + "_" + (i - 1);
            if (PlayerPrefs.GetInt(chaveEstrelasAnterior) <= 2)
            {
                AtivarBotaoNivel(i, nomeJogador);
            }
        }
    }

    public void Clicou(int nivel)
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador;
        if (nivel >= 1 && nivel <= 12)
        {
            if (nivel > 1 && PlayerPrefs.GetInt("starRomDesNiveis_" + GerenciaJogador.instancia.nomeJogador + "_" + (nivel - 1)) <= 2)
            {
                int estrelasAntigas = 3 - PlayerPrefs.GetInt("starRomDesNiveis_" + GerenciaJogador.instancia.nomeJogador + "_" + nivel);
                int pontosAntigos = estrelasAntigas * 100;

                GerenciaJogador.instancia.AtualizarPontuacao(-pontosAntigos); // Subtrai pontos antigos
                Debug.Log($"Estrelas antigas do nível {nivel}: {estrelasAntigas}, Pontos removidos: {pontosAntigos}");
            }
            else
            {
                Debug.Log("Nível " + (nivel - 1) + " precisa ter pelo menos uma estrela para desbloquear o Nível " + nivel + ".");
            }
            ZerarEstrelasNivel(nivel);
            DesbloquearNivelAtual(nivel, nomeJogador);
            PlayerPrefs.SetInt("nAtual_" + nomeJogador, nivel);
            changeScenes.proxCena("MatDouradoRomanos");

        }
        else
        {
            Debug.Log("Nível " + nivel + " está bloqueado. Complete o nível anterior para desbloqueá-lo.");
        }
    }

    private void ZerarEstrelasNivel(int nivel)
    {
        GameObject nivelObj = FindRecursively("Nivel" + nivel, transform);
        if (nivelObj != null)
        {
            Transform estrelasTransform = nivelObj.transform.Find("Estrelas");
            if (estrelasTransform != null)
            {
                for (int j = 1; j <= 3; j++)
                {
                    Transform estrela = estrelasTransform.Find("estrela" + j);
                    if (estrela != null)
                    {
                        estrela.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogError("Objeto 'Estrelas' não encontrado dentro do Nivel" + nivel + "!");
            }
        }
        else
        {
            Debug.LogError("Objeto 'Nivel" + nivel + "' não encontrado!");
        }
    }

    private void DesbloquearNivelAtual(int nivel, string nomeJogador)
    {
        string chaveBloqueado = "rom_bloqueado_" + nomeJogador + "_" + nivel;
        PlayerPrefs.SetInt(chaveBloqueado, 0); // Desbloqueia o nível
        PlayerPrefs.Save();

        GameObject nivelObj = FindRecursively("Nivel" + nivel, transform);
        if (nivelObj != null)
        {
            Transform bloqueadoTransform = nivelObj.transform.Find("Estrelas/bloqueado");

            if (bloqueadoTransform != null)
            {
                bloqueadoTransform.gameObject.SetActive(false); // Desativa o objeto "bloqueado"
                Debug.Log("Nível " + nivel + " desbloqueado para o jogador " + nomeJogador + "!");
            }
            else
            {
                Debug.LogError("Objeto 'bloqueado' não encontrado dentro do Nivel" + nivel + "!");
            }

            // Habilita o botão do nível
            nivelObj.GetComponent<Button>().interactable = true;
        }
        else
        {
            Debug.LogError("Objeto 'Nivel" + nivel + "' não encontrado!");
        }
    }

    private GameObject FindRecursively(string name, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }

            GameObject found = FindRecursively(name, child);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    private void AtualizarEstadoBloqueio(GameObject nivelObj, int nivel, string nomeJogador)
    {
        string chaveBloqueado = "rom_bloqueado_" + nomeJogador + "_" + nivel;

        // Encontra o objeto "bloqueado" dentro do nível
        Transform bloqueadoTransform = nivelObj.transform.Find("Estrelas/bloqueado");
        if (bloqueadoTransform != null)
        {
            int estadoBloqueado = PlayerPrefs.GetInt(chaveBloqueado);
            bloqueadoTransform.gameObject.SetActive(estadoBloqueado == 1);
        }
        else
        {
            Debug.LogError("Objeto 'bloqueado' não encontrado dentro do Nivel" + nivel + "!");
        }

        // Desativa o botão se o nível estiver bloqueado
        nivelObj.GetComponent<Button>().interactable = (PlayerPrefs.GetInt(chaveBloqueado) == 0);
    }

    private void AtivarBotaoNivel(int nivel, string nomeJogador)
    {
        GameObject nivelObj = FindRecursively("Nivel" + nivel, transform);
        if (nivelObj != null)
        {
            nivelObj.GetComponent<Button>().interactable = true;
        }
        else
        {
            Debug.LogError("Objeto 'Nivel" + nivel + "' não encontrado!");
        }
    }
}
