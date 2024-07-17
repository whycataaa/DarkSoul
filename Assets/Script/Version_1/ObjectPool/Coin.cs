using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game2;
public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ground")
        {
            CoinPool.Instance.coinPool.Release(gameObject);
        }
        else if(other.tag=="PlayerBody")
        {
            CoinPool.Instance.coinPool.Release(gameObject);
            PlayerControl.CoinNum++;
        }

    }
}
