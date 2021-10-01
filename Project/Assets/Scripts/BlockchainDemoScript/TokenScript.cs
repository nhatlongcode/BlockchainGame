using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TokenScript : MonoBehaviour
{
    public async void BalanceOf(Text balanceText)
    {
        Debug.Log("balance of clicked");

        BigInteger balance = await MyToken.BalanceOf("0x8d7090b7E3F8436150DFf41e233B499c0343A45f");
        balanceText.text = balance.ToString();
        Debug.Log("balanceOf Complete");
    }

    public string transaction;
    public async void Transact(Text amount)
    {
        string recipient = "0x6A178306b94903Ca76cab17D239D3Bf525fddd3B";

        try
        {
            BigInteger.Parse(amount.text);

            transaction = await MyToken.Transfer(recipient, 1000000000);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    // WARNING: Broken.
    public async void CheckTransactionSuccessful(Text transactionStatus)
    {
        //string transaction = "0xfb2c20195513313a29ad1aad69e7aabc79c59becf6838d4056c0f80b6964d773";
        bool status = await MyToken.IsTransactionConfirmed(transaction);
        if (status)
            transactionStatus.text = "Confirmed";
        else
            transactionStatus.text = "Not confirmed";
    }
}
