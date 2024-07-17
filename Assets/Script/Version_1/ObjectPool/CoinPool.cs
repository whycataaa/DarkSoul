using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 金币对象池
/// </summary>
public class CoinPool : SingletonMono<CoinPool>
{
    public GameObject coinPrefab;
    [Header("金币生成间隔")]
    public float time;
    private float timer;
    public ObjectPool<GameObject> coinPool;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        coinPool=new ObjectPool<GameObject>(createFunc,
                                            actionOnGet,
                                            actionOnRelease,
                                            actionOnDestroy,
                                            true,
                                            10,
                                            1000);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=time)
        {
            timer-=time;
            CoinSet();
        }
    }
    /// <summary>
    /// 生成金币
    /// </summary>
    private void CoinSet()
    {
        var coin=coinPool.Get();
        coin.transform.position=new Vector3(Random.Range(-5,5),transform.position.y,Random.Range(-5,5));
    }

    private GameObject createFunc()
    {
        var obj=Instantiate(coinPrefab,transform);
        return obj;
    }
    private void actionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void actionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }



}
