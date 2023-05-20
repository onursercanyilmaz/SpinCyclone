using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpinDatabase : ScriptableObject
{
    public List<Spin> spins = new List<Spin>();

    public int SpinCount
    {
        get { return spins.Count; }
    }

    public Spin GetSpin(int index)
    {
        return spins[index];
    }
}
