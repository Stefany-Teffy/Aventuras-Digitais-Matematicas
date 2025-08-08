// Junta os quadrados, verifica o resultado, mostra os valores e o resultado de vencedor na tela
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour, IHasChanged {

	// Declaracao de variaveis estaticas para slots e textos
	public static Transform slots; 
	[SerializeField] static Text inventoryText;

	// Declaracao de variaveis de resposta, contadores e outros
	public static int resposta;
	public int x, nb10 = 0, nb100 = 0, nb1=0,  i, y;
	public string nome;
	public Transform slotfinal;
	public GameObject b1;
	public GameObject b10;
	public GameObject b100;
	public int sub;
	public Button confirmaBotao; 
	public Text GeradoText;
	public Text opText;
	public Text numText;
	public static int valorGerado;

	
	void Start () {
		inventoryText = GameObject.Find ("VTotal").GetComponent<Text> ();
		slots = GameObject.Find ("PanelFinal").transform;
		resposta = GetOp2.valorOp;
	
		 // Verifica a cena anterior e define a visibilidade de textos com base nela
		if (changeScenes.nomeAnt.ToString () == "Opcoes") {
			opText.enabled=false;
			resposta = -1;
			confirmaBotao.gameObject.SetActive(false);
		}			
		else if (changeScenes.nomeAnt == "selecao") {
			opText.enabled=false;
			numText.enabled=true;
		}
		else if (changeScenes.nomeAnt == "op2") {
			if (GetOp2.opcao == 2) {
				slotfinal = GameObject.Find ("SlotFinal").transform;
				if (GetOp2.op1 > GetOp2.op2)
					sub = GetOp2.op1;
				else
					sub = GetOp2.op2;
				int subc, subd, subu;
				if (sub >= 100) {
					subc = (int)sub / 100;
					sub = sub - subc * 100;
					for (x = 0; x < subc; x++)
						Instantiate (b100, slotfinal);
				}
				if (sub >= 10) {
					subd = (int)sub / 10;
					sub = sub - subd * 10;
					for (x = 0; x < subd; x++)
						Instantiate (b10, slotfinal);
				}
				if (sub >= 1) {
					subu = sub;
					sub = sub - subu;
					for (x = 0; x < subu; x++)
						Instantiate (b1, slotfinal);
				}

			}
		}
		HasChanged ();
	}

	#region IHasChanged implementation
	public void HasChanged ()
	{
		if (changeScenes.nomeAnt == "op2")
		{
			opText.text = "OBJETIVO: " + GetOp2.operacao.ToString();
			if (GetOp2.opcao == 2)
			{
				slotfinal = GameObject.Find("SlotFinal").transform;
				nb1 = 0;
				nb10 = 0;
				nb100 = 0;
				foreach (Transform bloquinho in slotfinal)
				{
					if (bloquinho.tag == "10")
					{
						nb10++;
						i = slotfinal.GetSiblingIndex();
					}
					else if (bloquinho.tag == "100")
					{
						nb100++;
						y = slotfinal.GetSiblingIndex();
					}
					else if (bloquinho.tag == "1")
						nb1++;
				}
				if (nb100 >= 1 && nb10 == 0)
				{
					Transform bDestruir100 = slotfinal.Find("b100(Clone)");
					DestroyImmediate(bDestruir100.gameObject);
					for (x = 0; x < 10; x++)
						Instantiate(b10, slotfinal);
				}
				if (nb10 >= 1 && nb1 == 0)
				{
					Transform bDestruir10 = slotfinal.Find("b10(Clone)");
					DestroyImmediate(bDestruir10.gameObject);
					for (x = 0; x < 10; x++)
						Instantiate(b1, slotfinal);
				}

			}
		}
		else if (changeScenes.nomeAnt == "selecao")
		{
			numText.text = "OBJETIVO: " + numMax.num.ToString();
		}
		

            AtualizarValor();
		#endregion
	}

	public static void AtualizarValor(){
		int valortotal = 0;
		foreach (Transform slotTransform in slots)
    	{
			if (slotTransform.childCount > 0)
			{
				for (int i = 0; i < slotTransform.childCount; i++)
				{
					valortotal += int.Parse(slotTransform.GetChild(i).gameObject.tag);
				}
			}
		}
		Debug.Log ("Valor no slot: " + valortotal);
		inventoryText.text = "VALOR TOTAL: " + valortotal.ToString();
    }
}
// Interface para notificar mudanças
namespace UnityEngine.EventSystems{
	public interface IHasChanged: IEventSystemHandler{
		void HasChanged ();
	}
}