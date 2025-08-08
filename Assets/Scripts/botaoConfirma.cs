using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class botaoConfirma : MonoBehaviour
{
    [Header("Áudios de Feedback")]
    public AudioSource dicaRemoverSource;
    public AudioSource dicaAdicionarSource;
    
    public AudioSource vitoriaSource; 

    public void VerificarResposta()
    {
        int valortotal = CalcularValorTotal();
        int respostaCorreta = 0;
        bool respostaDefinida = false;
        
        if (changeScenes.nomeAnt == "op2")
        {
            respostaCorreta = Inventory.resposta;
            respostaDefinida = true;
        }
        else if (changeScenes.nomeAnt == "selecao")
        {
            respostaCorreta = numMax.num;
            respostaDefinida = true;
        }

        if (!respostaDefinida) return;

        if (valortotal == respostaCorreta) // ACERTOU
        {
            if (dicaRemoverSource.isPlaying) dicaRemoverSource.Stop();
            if (dicaAdicionarSource.isPlaying) dicaAdicionarSource.Stop();
            
            StartCoroutine(VitoriaModoComAtrasoDeAudio());
        }
        else // ERROU
        {
            if (valortotal > respostaCorreta)
            {
                if (!dicaRemoverSource.isPlaying) dicaRemoverSource.Play();
            }
            else
            {
                if (!dicaAdicionarSource.isPlaying) dicaAdicionarSource.Play();
            }
        }
    }

    private IEnumerator VitoriaModoComAtrasoDeAudio()
    {
        if (vitoriaSource != null)
        {
            vitoriaSource.Play();
            yield return new WaitForSeconds(vitoriaSource.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
        }
        
        ativar.ativarModal();
    }

    private int CalcularValorTotal()
    {
        int valortotal = 0;
        foreach (Transform slotTransform in Inventory.slots)
        {
            for (int i = 0; i < slotTransform.childCount; i++)
                valortotal += int.Parse(slotTransform.GetChild(i).gameObject.tag);
        }
        return valortotal;
    }
}