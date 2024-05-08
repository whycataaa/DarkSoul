using echo17.EndlessBook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookControl : Singleton<BookControl>
{
    public EndlessBook endlessBook;

    public Material[] pageMaterial;
    public string[] pageName;

    public Dictionary<string, Material> pageTable=new Dictionary<string, Material>();

    protected override void Awake()
    {
        base.Awake();
        endlessBook = GetComponent<EndlessBook>();

        // 确保 pageMaterial 和 pageName 数组长度相等
        if (pageMaterial.Length != pageName.Length)
        {
            Debug.LogError("Page material and page name arrays must have the same length.");
            return;
        }

        // 添加键值对到字典
        for (int i = 0; i < pageMaterial.Length; i++)
        {
            string page = pageName[i];
            Material material = pageMaterial[i];

            // 检查键是否已存在
            if (!pageTable.ContainsKey(page))
            {
                pageTable.Add(page, material);
            }
            else
            {
                Debug.LogWarning($"Page '{page}' is already added to the page table.");
            }
        }

    }
}
