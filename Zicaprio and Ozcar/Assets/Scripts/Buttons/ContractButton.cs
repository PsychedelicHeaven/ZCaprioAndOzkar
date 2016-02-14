﻿using UnityEngine;
using System.Collections;

public class ContractButton : MonoBehaviour {

    public ContractData contract;

    public void ShowDescription()
    {
        UIManager.Instance.contract_description.text = contract.description;
        UIManager.Instance.contract_cost.text = contract.baseCash + "$";
        UIManager.Instance.contract_time.text = contract.contractTime + " month";
    }

    public void ChoseContract()
    {
        UIManager.Instance.Chosen_contract = contract;
        UIManager.Instance.contract_is_chosen = true;
    }

}