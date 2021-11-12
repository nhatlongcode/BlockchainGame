using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System;

class MyToken
{
    private static string abiERC20 = "[{\"inputs\": [{\"internalType\": \"address\",\"name\": \"owner\",\"type\": \"address\"},{\"internalType\": \"contract TestTokenERC721\",\"name\": \"partner\",\"type\": \"address\"}],\"stateMutability\": \"nonpayable\",\"type\": \"constructor\"},{\"inputs\": [],\"name\": \"ERC721\",\"outputs\": [{\"internalType\": \"contract TestTokenERC721\",\"name\": \"\",\"type\": \"address\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"WinAmount\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"_ethExchangeRate\",\"outputs\": [{\"internalType\": \"uint128\",\"name\": \"\",\"type\": \"uint128\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"account\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"_mintERC20\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"_totalSupply\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"sender\",\"type\": \"address\"},{\"internalType\": \"address\",\"name\": \"recipient\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"_transferERC20Partner\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"excess\",\"type\": \"uint256\"}],\"name\": \"addExcessCurrency\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"account\",\"type\": \"address\"}],\"name\": \"balanceOf\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"decimals\",\"outputs\": [{\"internalType\": \"uint8\",\"name\": \"\",\"type\": \"uint8\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"to\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"mint\",\"outputs\": [],\"stateMutability\": \"payable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"loser\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"timestamp\",\"type\": \"uint256\"},{\"internalType\": \"bytes\",\"name\": \"signature\",\"type\": \"bytes\"}],\"name\": \"mintWithGameplay\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"name\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint128\",\"name\": \"exchangeRate\",\"type\": \"uint128\"}],\"name\": \"setExchangeRate\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"symbol\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"totalSupply\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"recipient\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"transfer\",\"outputs\": [{\"internalType\": \"bool\",\"name\": \"\",\"type\": \"bool\"}],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"sender\",\"type\": \"address\"},{\"internalType\": \"address\",\"name\": \"recipient\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"transferFrom\",\"outputs\": [{\"internalType\": \"bool\",\"name\": \"\",\"type\": \"bool\"}],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"withdrawEth\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"}]";
    private static string abiERC721 = "[{\"inputs\": [{\"internalType\": \"address\",\"name\": \"from\",\"type\": \"address\"},{\"internalType\": \"address\",\"name\": \"to\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"_transferERC721Partner\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"parent1\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"parent2\",\"type\": \"uint256\"}],\"name\": \"breed\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"buy\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"budget\",\"type\": \"uint256\"}],\"name\": \"buyWithBudget\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"sellPrice\",\"type\": \"uint256\"},{\"internalType\": \"bytes\",\"name\": \"signature\",\"type\": \"bytes\"}],\"name\": \"buyWithCoupon\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"cancelSale\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"to\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"mint\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"from\",\"type\": \"address\"},{\"internalType\": \"address\",\"name\": \"to\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"safeTransferFrom\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256[]\",\"name\": \"tokens\",\"type\": \"uint256[]\"}],\"name\": \"setFreeTokens\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"price\",\"type\": \"uint256\"}],\"name\": \"setSalePrice\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"},{\"internalType\": \"string\",\"name\": \"_tokenURI\",\"type\": \"string\"}],\"name\": \"setTokenURI\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"from\",\"type\": \"address\"},{\"internalType\": \"address\",\"name\": \"to\",\"type\": \"address\"},{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"transferFrom\",\"outputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"function\"},{\"inputs\": [],\"stateMutability\": \"nonpayable\",\"type\": \"constructor\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"_exists\",\"outputs\": [{\"internalType\": \"bool\",\"name\": \"\",\"type\": \"bool\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"_lastTokenCreated\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"_numberOfTokens\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"name\": \"_owners\",\"outputs\": [{\"internalType\": \"address\",\"name\": \"\",\"type\": \"address\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"name\": \"_tokenLinkedList\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"name\": \"_tokenURIs\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"account\",\"type\": \"address\"}],\"name\": \"balanceOf\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"indexFirst\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"amount\",\"type\": \"uint256\"}],\"name\": \"browseMarketItems\",\"outputs\": [{\"internalType\": \"uint256[]\",\"name\": \"\",\"type\": \"uint256[]\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"},{\"internalType\": \"uint256\",\"name\": \"sellPrice\",\"type\": \"uint256\"},{\"internalType\": \"address\",\"name\": \"seller\",\"type\": \"address\"},{\"internalType\": \"bytes\",\"name\": \"signature\",\"type\": \"bytes\"}],\"name\": \"checkRealCoupon\",\"outputs\": [{\"internalType\": \"bool\",\"name\": \"\",\"type\": \"bool\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"ERC20\",\"outputs\": [{\"internalType\": \"contract TestTokenERC20\",\"name\": \"\",\"type\": \"address\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"name\": \"freeTokens\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"getSalePrice\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"name\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"ownerOf\",\"outputs\": [{\"internalType\": \"address\",\"name\": \"\",\"type\": \"address\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"name\": \"salePrice\",\"outputs\": [{\"internalType\": \"uint256\",\"name\": \"\",\"type\": \"uint256\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [],\"name\": \"symbol\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"pure\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"address\",\"name\": \"account\",\"type\": \"address\"}],\"name\": \"tokensOwned\",\"outputs\": [{\"internalType\": \"uint256[]\",\"name\": \"\",\"type\": \"uint256[]\"}],\"stateMutability\": \"view\",\"type\": \"function\"},{\"inputs\": [{\"internalType\": \"uint256\",\"name\": \"tokenId\",\"type\": \"uint256\"}],\"name\": \"tokenURI\",\"outputs\": [{\"internalType\": \"string\",\"name\": \"\",\"type\": \"string\"}],\"stateMutability\": \"view\",\"type\": \"function\"}]";
    
    public const string chain = "binance";
    public const string network = "testnet";
    public const string contractERC20 = "0x6e6affe5027626Ea81C1cAd7E7465809CeE1d693";
    public const string contractERC721 = "0x4Ae623D6e4be864B76a8F36A6811e16be4308332";
    public static string account { get => PlayerPrefs.GetString("Account"); }
    // TODO: Buy with coupon, Mint with gameplay.
    #region private helper
    private static BigInteger ParseBigInt(string number)
    {
        try
        {
            return BigInteger.Parse(number);
        }
        catch
        {
            Debug.LogError("Parse number fails: " + number);
            throw;
        }
    }

    private static List<BigInteger> ParseListBigInt(string list)
    {
        try
        {
            string[] items = JsonConvert.DeserializeObject<string[]>(list);
            List<BigInteger> tokens = new List<BigInteger>();
            for (int i = 0; i < items.Length; i++)
            {
                tokens.Add(BigInteger.Parse(items[i]));
            }
            return tokens;
        }
        catch
        {
            Debug.LogError(list);
            throw;
        }
    }
#endregion

    #region ERC20
    // The balance of the main currency
    public static async Task<BigInteger> BalanceOf(string _account = null, string _rpc = "")
    {
        string method = "balanceOf";
        //string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        if (_account == null) _account = account;
        string[] obj = { _account };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, _rpc);
        return ParseBigInt(response);
    }

    /// The name of the main currency.
    public static async Task<string> Name(string _rpc = "")
    {
        string method = "name";
        //string abi = "[{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"}]";
        string[] obj = { };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, _rpc);
        return response;
    }

    /// The symbol of the main currency.
    public static async Task<string> Symbol(string _rpc = "")
    {
        string method = "symbol";
        //string abi = "[{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"}]";
        string[] obj = { };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, _rpc);
        return response;
    }

    /// The number of decimal places to use for the main currency.
    private static BigInteger? _decimals = null;
    public static async Task<BigInteger> Decimals(string _rpc = "")
    {
        if (_decimals != null)
            return _decimals.Value;
        string method = "decimals";
        //string abi = "[{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"pure\",\"type\":\"function\"}]";
        string[] obj = { };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, _rpc);
        _decimals = ParseBigInt(response);
        return _decimals.Value;
    }

    /// total supply of the main currency.
    public static async Task<BigInteger> TotalSupply(string _rpc = "")
    {
        string method = "totalSupply";
        //string abi = "[{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        string[] obj = { };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC20, abiERC20, method, args, _rpc);
        return ParseBigInt(response);
    }
#if UNITY_WEBGL
    /// <summary>
    /// TRANSACTION: transfer the main token to the recipient.
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> Transfer(string recipient, BigInteger amount, string _rpc = "")
    {
        string method = "transfer";
        //string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string[] obj = { recipient, amount.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC20, contractERC20, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        };
    }

#endif
#endregion

    #region ERC721

    // The address of the owner of the certain token
    public static async Task<string> OwnerOf(BigInteger tokenId, string _rpc = "")
    {
        string method = "ownerOf";
        //string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"ownerOf\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        string[] obj = { tokenId.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC721, abiERC721, method, args, _rpc);
        return response;
    }

    // Get the list of tokens owned by an account.
    public static async Task<List<BigInteger>> TokensOwned(string account, string _rpc = "")
    {
        string method = "tokensOwned";
        //string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"tokensOwned\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
        string[] obj = { account };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC721, abiERC721, method, args, _rpc);
        return ParseListBigInt(response);
    }
#if UNITY_WEBGL
    /// <summary>
    /// TRANSACTION: transfer the token from one user to another
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> TransferToken(string from, string to, BigInteger tokenId)
    {
        string method = "safeTransferFrom";
        //string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string[] obj = { from, to, tokenId.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
    /// <summary>
    /// TRANSACTION: mint token for an account. Only admin has this right.
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> MintToken(string to, string tokenId)
    {
        /*
        string keyAccount = "0x8d7090b7E3F8436150DFf41e233B499c0343A45f";
        if (keyAccount != account)
        {
            Debug.LogWarning("You don't have access to this function");
            return;
        }
        //*/
        string method = "mint";
        //string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"mint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string[] obj = { to, tokenId.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }

    public static async Task<string> Breed(BigInteger parent1, BigInteger parent2)
    {
        string method = "breed";
        string[] obj = { parent1.ToString(), parent2.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
#endif
#endregion

    #region Buying & selling
    public static async Task<List<BigInteger>> ItemsForSale(int indexFirst, int amount, string _rpc = "")
    {
        string method = "browseMarketItems";
        string[] obj = { indexFirst.ToString(), amount.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(chain, network, contractERC721, abiERC721, method, args, _rpc);
        return ParseListBigInt(response);
    }
    public static async Task<Dictionary<BigInteger, BigInteger>> GetSalePrices(int indexFirst, int amount, string _rpc = "")
    {
        List<BigInteger> items = await ItemsForSale(indexFirst, amount, _rpc);

        string method = "salePrice";
        List<string[]> obj = new List<string[]>();
        for (int i = 0; i < items.Count; i++)
            obj.Add(new string[] { items.ToString() });
        string args = JsonConvert.SerializeObject(obj.ToArray());
        string response = await EVM.MultiCall(chain, network, contractERC721, abiERC721, method, args, _rpc);

        List<BigInteger> prices = ParseListBigInt(response);
        Dictionary<BigInteger, BigInteger> result = new Dictionary<BigInteger, BigInteger>();
        try
        {
            for (int i = 0; i < items.Count; i++)
            {
                result[items[i]] = prices[i];
            }
            return result;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        }
    }
#if UNITY_WEBGL
    /// <summary>
    /// TRANSACTION: transfer the main token to the recipient.
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> SetSalePrice(BigInteger tokenId, BigInteger price, string _rpc = "")
    {
        string method = "setSalePrice";
        //string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"price\",\"type\":\"uint256\"}],\"name\":\"setSalePrice\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string[] obj = { tokenId.ToString(), price.ToString() };
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
    /// <summary>
    /// TRANSACTION: cancel the sale of this token
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> CancelSale(BigInteger tokenId, string _rpc = "")
    {
        string method = "setSalePrice";
        string[] obj = { tokenId.ToString(), "0" };
        //string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"cancelSale\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
    /// <summary>
    /// TRANSACTION: Buy the token at any cost.
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> Buy(BigInteger tokenId, string _rpc = "")
    {
        string method = "buy";
        string[] obj = { tokenId.ToString() };
        //string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"buy\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
    /// <summary>
    /// TRANSACTION: Buy the token if the price is within budget.
    /// </summary>
    /// <returns> the transaction ID </returns>
    public static async Task<string> BuyWithBudget(BigInteger tokenId, BigInteger budget, string _rpc = "")
    {
        string method = "buyWithBudget";
        string[] obj = { tokenId.ToString(), budget.ToString() };
        //string abi = "[{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"budget\",\"type\":\"uint256\"}],\"name\":\"buyWithBudget\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
        string args = JsonConvert.SerializeObject(obj);
        try
        {
            string transactionId = await Web3GL.Send(method, abiERC721, contractERC721, args, "0");
            return transactionId;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        };
    }
#endif
#endregion

    public static async Task<bool> IsTransactionConfirmed(string transactionId, string _rpc = "")
    {
        return await EVM.IsTxConfirmed(chain, network, transactionId, _rpc);
    }
}