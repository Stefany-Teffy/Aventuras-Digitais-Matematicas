using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MDNivelRomanos : MonoBehaviour
{
    public AudioSource dicaRemoverSource;
    public AudioSource dicaAdicionarSource;
    public Text inventoryText;
    public GameObject b100, b10, b1;
    public int valorTotal;
    int numeroGerado = 0;
    int sorteio = 0;
    int tentativas, fases, nAtual; 
    private static HashSet<int> numerosGeradosGlobal = new HashSet<int>();
    private static List<int> listaPossivel = new List<int>();
    Text geradoText;

    private static readonly string[] unidades = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
    private static readonly string[] dezenas = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
    private static readonly string[] centenas = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };

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
        if (nAtual <= 10)
        {
            string numeroEmRomanos = ConverterParaRomano(numeroGerado);
            geradoText.text = "VALOR GERADO: " + numeroEmRomanos;
        }
    
    }

    int GerarNumero()
    {
        switch (nAtual)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
                GerarNumeroParaRomanos(); // Usa a função corrigida
                break;
        }
        return numeroGerado;
    }
    void GerarNumeroParaRomanos()
    {
        if (nAtual >= 1 && nAtual <= 3)
        {
            listaPossivel = Enumerable.Range(1, 9).ToList(); // 1 a 9
        }
        else if (nAtual >= 4 && nAtual <= 6)
        {
            listaPossivel = new List<int> {10, 20, 30, 40, 50, 60, 70, 80, 90}; // dezenas exatas
        }
        else if (nAtual >= 7 && nAtual <= 9)
        {
            listaPossivel = new List<int> {100, 200, 300, 400, 500, 600, 700, 800, 900}; // centenas exatas
        }
        else if (nAtual == 10)
        {
            numeroGerado = Random.Range(1, 1000);
            return;
        }

        var disponiveis = listaPossivel.Except(numerosGeradosGlobal).ToList();

        if (disponiveis.Count > 0)
        {
            int index = Random.Range(0, disponiveis.Count);
            numeroGerado = disponiveis[index];
            numerosGeradosGlobal.Add(numeroGerado);
        }
        else
        {
            Debug.LogWarning("Todos os números possíveis já foram usados em todos os níveis.");
            numeroGerado = -1;
        }
        
        Debug.Log("Números já gerados: " + string.Join(", ", numerosGeradosGlobal));

    }

    string ConverterParaRomano(int numero)
    {
        if (numero < 1 || numero > 999)
        {
            return "Número fora do intervalo";
        }
        
        string resultado = "";

        // Converter centenas
        int centenasIndex = numero / 100;
        resultado += centenas[centenasIndex];
        numero %= 100;

        // Converter dezenas
        int dezenasIndex = numero / 10;
        resultado += dezenas[dezenasIndex];
        numero %= 10;

        // Converter unidades
        resultado += unidades[numero];

        return resultado;
    }
    public void Verificar()
    {
        valorTotal = CalcularValorTotal();
        tentativas--;
        GerenciadorEstrelasRomanos gerenciadorEstrelas = GameObject.Find("GerenciadorEstrelasRomanos").GetComponent<GerenciadorEstrelasRomanos>();

        if (tentativas == 0)
        {
            if (dicaRemoverSource.isPlaying)
            dicaRemoverSource.Stop();
            if (dicaAdicionarSource.isPlaying)
            dicaAdicionarSource.Stop();
            PlacarRomanos.Ativar(gerenciadorEstrelas, valorTotal, numeroGerado, tentativas, fases, nAtual);
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
            PlacarRomanos.Ativar(gerenciadorEstrelas, valorTotal, numeroGerado, tentativas, fases, nAtual);
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

        PlayerPrefs.SetInt("rom_bloqueado_" + nomeJogador + "_" + (valorAtual + 1), 0);
        Debug.Log("Nível atual antes: " + valorAtual);

        if (valorAtual == 0) 
        {
            valorAtual = 1;
        }

        valorAtual += 1;
        PlayerPrefs.SetInt("nAtual_" + nomeJogador, valorAtual);
        PlayerPrefs.Save();
        Debug.Log("Nível após incremento: " + valorAtual);
        SceneManager.LoadScene("MatDouradoRomanos");
    }

    public void Retornar()
    {
        fases = 1;
        tentativas = 4;
        AtualizaAposDestruir();
        PlacarRomanos.Desativar();
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
}
