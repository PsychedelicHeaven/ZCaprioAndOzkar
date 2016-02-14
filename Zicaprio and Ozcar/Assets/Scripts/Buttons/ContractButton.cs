using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContractButton : MonoBehaviour {

    public ContractData contract;
    public Text button_text;

    public void ShowDescription()
    {
        
        UIManager.Instance.contract_description.text = contract.description;
        UIManager.Instance.contract_cost.text = contract.baseCash + "$";
        UIManager.Instance.contract_time.text = contract.contractTime + " months + " + (Actor.Instance.actualTime(contract.contractTime) - contract.contractTime);
        string temp = " ";
        for(int i = 0; i < contract.genre.Count;i++)
        {
            if(i == 0)
            {
                temp = contract.genre[i].genreType.ToString();
            }
            else
            {
                temp = temp + ", " + contract.genre[i].genreType.ToString();
            }
            
        }
        UIManager.Instance.contract_genre.text = temp;
        UIManager.Instance.contract_type.text = contract.contractType.ToString();

        UIManager.Instance.contract_skill1.text = "";
        UIManager.Instance.contract_skill2.text = "";
        UIManager.Instance.contract_skill3.text = "";
        UIManager.Instance.contract_skill4.text = "";
        UIManager.Instance.contract_skill5.text = "";

        for (int i = 0; i < contract.skill.Count; i++)
        {
            switch(i)
            {
                case 0:
                    UIManager.Instance.contract_skill1.text = contract.skill[i].skillType.ToString() + ": " + contract.skill[i].minReq.ToString();
                    break;
                case 1:
                    UIManager.Instance.contract_skill2.text = contract.skill[i].skillType.ToString() + ": " + contract.skill[i].minReq.ToString();
                    break;
                case 2:
                    UIManager.Instance.contract_skill3.text = contract.skill[i].skillType.ToString() + ": " + contract.skill[i].minReq.ToString();
                    break;
                case 3:
                    UIManager.Instance.contract_skill4.text = contract.skill[i].skillType.ToString() + ": " + contract.skill[i].minReq.ToString();
                    break;
                case 4:
                    UIManager.Instance.contract_skill5.text = contract.skill[i].skillType.ToString() + ": " + contract.skill[i].minReq.ToString();
                    break;
                default:
                    break;
            }
            
        }
        
        
    }

    public void ChoseContract()
    {
        if (Actor.Instance.ContractPossibility(contract))
        {
            UIManager.Instance.Chosen_contract = contract;
            UIManager.Instance.contract_is_chosen = true;
            UIManager.Instance.contract_sorry.enabled = false;
        }
        else
        {
            UIManager.Instance.contract_sorry.enabled = true;
            UIManager.Instance.sorry_timer = 0;
        }
    }


}
