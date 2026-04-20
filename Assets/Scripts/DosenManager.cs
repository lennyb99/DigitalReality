using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DosenManager : MonoBehaviour
{
    [SerializeField]
    private List<Dose> dosen;

    public static DosenManager Instance;

    public int stehendeDosenCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        stehendeDosenCount = CountDosen();
    }

    private int CountDosen()
    {
        int count = 0;
        foreach (Dose dose in dosen)
        {
            if (!dose.umgekippt) count++;
        }
        return count;
    }

    public void NotifyDosenKipp()
    {
        CountDosen();
    }

}
