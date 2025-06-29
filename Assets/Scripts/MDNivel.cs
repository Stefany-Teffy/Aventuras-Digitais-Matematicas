using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MDNivel : MonoBehaviour
{
    public AudioSource dicaRemoverSource;
    public AudioSource dicaAdicionarSource;
    public Text inventoryText;
    public GameObject b100, b10, b1;
    public int valorTotal;
    int numeroGerado = 0;
    int numeroGerado1 = 0;
    int numeroGerado2 = 0;
    int sorteio = 0;
    int tentativas, fases, nAtual; 
    private HashSet<int> numerosGerados = new HashSet<int>(); // armazena elementos únicos sem duplicatas, escolhi o tipo int
    Text geradoText;

    void Start()
    {
        tentativas = 4;
        fases = 1;
        nAtual = PlayerPrefs.GetInt("nAtual_" + GerenciaJogador.instancia.nomeJogador, 1);
        Debug.Log("Nível carregado no start: " + nAtual);
        geradoText = GameObject.Find("Gerado").GetComponent<Text>();
        
        GerarNumero();
        AtualizarTextoGerado();
    }

    void AtualizarTextoGerado()
    {
        if (nAtual < 4)
        {
            geradoText.text = "VALOR GERADO: " + numeroGerado.ToString();
        }
        else if (nAtual >= 4 && nAtual <= 6)
        {
            geradoText.text = "VALOR GERADO: " + numeroGerado1.ToString() + "+" + numeroGerado2.ToString();
        }
        else if (nAtual >= 7 && nAtual <= 9)
        {
            geradoText.text = "VALOR GERADO: " + numeroGerado1.ToString() + "-" + numeroGerado2.ToString();
            VisualizarNumero(numeroGerado1);
        }
        else if (nAtual >= 10 && nAtual <= 12)
        {
            if (sorteio == 0)
            {
                geradoText.text = "VALOR GERADO: " + numeroGerado1.ToString() + "-" + numeroGerado2.ToString();
                VisualizarNumero(numeroGerado1);
            }
            else if (sorteio == 1)
            {
                geradoText.text = "VALOR GERADO: " + numeroGerado1.ToString() + "+" + numeroGerado2.ToString();
            }
            else
            {
                geradoText.text = "VALOR GERADO: " + numeroGerado.ToString();
            }
        }
    }

    int GerarNumero()
    {
        switch (nAtual)
        {
            case 1:
                numeroGerado = GerarNumeroUnico(1, 10);
                break;
            case 2:
                numeroGerado = GerarNumeroUnico(10, 100);
                break;
            case 3:
                numeroGerado = GerarNumeroUnico(100, 1000);
                break;
            case 4:
            case 5:
            case 6:
                GerarNumeroAdicao();
                break;
            case 7:
            case 8:
            case 9:
                GerarNumeroSubtracao();
                break;
            case 10:
            case 11:
            case 12:
                GerarNumeroSorteado();
                break;
        }
        return numeroGerado;
    }

    int GerarNumeroUnico(int min, int max)
    {
        int num;
        do
        {
            num = Random.Range(min, max);
        } while (numerosGerados.Contains(num));
        numerosGerados.Add(num);
        return num;
    }

    void GerarNumeroAdicao()
    {
        do
        {
            if (nAtual == 4)
            {
                numeroGerado1 = Random.Range(1, 10);
                numeroGerado2 = Random.Range(1, 10);
            }
            else if (nAtual == 5)
            {
                numeroGerado1 = Random.Range(10, 100);
                numeroGerado2 = Random.Range(10, 100);
            }
            else if (nAtual == 6)
            {
                numeroGerado1 = Random.Range(100, 501);
                numeroGerado2 = Random.Range(100, 500);
            }
            numeroGerado = numeroGerado1 + numeroGerado2;
        } while (numerosGerados.Contains(numeroGerado));
        numerosGerados.Add(numeroGerado);
    }

    void GerarNumeroSubtracao()
    {
        do
        {
            if (nAtual == 7)
            {
                numeroGerado1 = Random.Range(1, 10);
                numeroGerado2 = Random.Range(1, 10);
            }
            else if (nAtual == 8)
            {
                numeroGerado1 = Random.Range(10, 100);
                numeroGerado2 = Random.Range(10, 100);
            }
            else if (nAtual == 9)
            {
                numeroGerado1 = Random.Range(100, 1000);
                numeroGerado2 = Random.Range(100, 1000);
            }
            while (numeroGerado1 <= numeroGerado2)
            {
                if (nAtual == 7)
                {
                    numeroGerado2 = Random.Range(1, 10);
                }
                else if (nAtual == 8)
                {
                    numeroGerado2 = Random.Range(10, 100);
                }
                else if (nAtual == 9)
                {
                    numeroGerado2 = Random.Range(100, 500);
                }
            }
            numeroGerado = numeroGerado1 - numeroGerado2;
        } while (numerosGerados.Contains(numeroGerado));
        numerosGerados.Add(numeroGerado);
    }

    public void Verificar()
    {
        valorTotal = CalcularValorTotal();
        tentativas--;
        GerenciadorEstrelas gerenciadorEstrelas = GameObject.Find("GerenciadorEstrelas").GetComponent<GerenciadorEstrelas>();

        if (tentativas == 0)
        {
            if (dicaRemoverSource.isPlaying)
            dicaRemoverSource.Stop();
            if (dicaAdicionarSource.isPlaying)
            dicaAdicionarSource.Stop();
            Placar.Ativar(gerenciadorEstrelas, valorTotal, numeroGerado, tentativas, fases, nAtual);
        }
        else if (valorTotal == numeroGerado && fases != 3)
        {
            fases++;
            tentativas = 4;
            Debug.Log("fase atual = " + fases);
            GerarNumero();
            AtualizaAposDestruir();
            AtualizarTextoGerado();
        }
        else if (valorTotal == numeroGerado && fases == 3)
        {
            if (dicaRemoverSource.isPlaying)
            dicaRemoverSource.Stop();
            if (dicaAdicionarSource.isPlaying)
            dicaAdicionarSource.Stop();
            Placar.Ativar(gerenciadorEstrelas, valorTotal, numeroGerado, tentativas, fases, nAtual);
        }
        else
        {
            if (valorTotal > numeroGerado)
            {
                dicaRemoverSource.Play();
            }
            else
            {
                dicaAdicionarSource.Play();
            }
        }
    }

    private int CalcularValorTotal()
    {
        int total = 0;
        foreach (Transform slotTransform in Inventory.slots)
        {
            for (int i = 0; i < slotTransform.childCount; i++)
                total += int.Parse(slotTransform.GetChild(i).gameObject.tag);
        }
        return total;
    }

    void AtualizaAposDestruir()
    {
        foreach (Transform slotTransform in Inventory.slots)
        {
            for (int i = 0; i < slotTransform.childCount; i++)
            {
                Destroy(slotTransform.GetChild(i).gameObject);
            }
        }

        inventoryText.text = "VALOR TOTAL: " + 0;
    }

    public void ProximoNivel()
    {
        string nomeJogador = GerenciaJogador.instancia.nomeJogador; // Obtém o nome do jogador logado
        int valorAtual = PlayerPrefs.GetInt("nAtual_" + nomeJogador, 0);

        PlayerPrefs.SetInt("nat_bloqueado_" + nomeJogador + "_" + (valorAtual + 1), 0);
        Debug.Log("Nível atual antes: " + valorAtual);


        if (valorAtual == 0) 
        {
            valorAtual = 1;
        }

        valorAtual += 1;
        PlayerPrefs.SetInt("nAtual_" + GerenciaJogador.instancia.nomeJogador, valorAtual);
        PlayerPrefs.Save();
        Debug.Log("Nível após incremento: " + valorAtual);
        SceneManager.LoadScene("MatDourado2");
    }

    public void Retornar()
    {
        fases = 1;
        tentativas = 4;
        AtualizaAposDestruir();
        Placar.Desativar();
    }

    void VisualizarNumero(int numero)
    {
        Transform slotfinal = GameObject.Find("SlotFinal").transform;
        int sub = numero;
        int subc, subd, subu;

        if (sub >= 100)
        {
            subc = sub / 100;
            sub -= subc * 100;
            for (int x = 0; x < subc; x++)
                Instantiate(b100, slotfinal);
        }

        if (sub >= 10)
        {
            subd = sub / 10;
            sub -= subd * 10;
            for (int x = 0; x < subd; x++)
                Instantiate(b10, slotfinal);
        }

        if (sub >= 1)
        {
            subu = sub;
            sub -= subu;
            for (int x = 0; x < subu; x++)
                Instantiate(b1, slotfinal);
        }
        valorTotal = numero;
        inventoryText.text = "VALOR TOTAL: " + valorTotal.ToString();
    }

    int GerarNumeroSorteado()
    {
        do
        {
            sorteio = Random.Range(0, 3); // 0 = subtração, 1 = adição, 2 = representação

            if (sorteio == 0)
            {
                numeroGerado1 = Random.Range(1, 1000);
                numeroGerado2 = Random.Range(1, 1000);
                while (numeroGerado1 <= numeroGerado2)
                {
                    numeroGerado2 = Random.Range(100, 500);
                }
                numeroGerado = numeroGerado1 - numeroGerado2;
            }
            else if (sorteio == 1)
            {
                numeroGerado1 = Random.Range(100, 501);
                numeroGerado2 = Random.Range(100, 500);
                numeroGerado = numeroGerado1 + numeroGerado2;
            }
            else
            {
                numeroGerado = Random.Range(1, 1000);
            }
        } while (numerosGerados.Contains(numeroGerado));
        numerosGerados.Add(numeroGerado);

        return numeroGerado;
    }
}
