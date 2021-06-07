using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Temporary stat changes via equipment or buff and debuffs.
 */
public class StatModifier
{
    private Dictionary<string, int> add;            // stat, magnitude
    private Dictionary<string, int> mult;

    /**
     * Example dictionary { ["str"] = -0.5,  ["spd"] = -0.5, ["int"] = -0.5 } }
     * Pass null if either no add or mult. 
     */
    public StatModifier(Dictionary<string, int> add, Dictionary<string , int> mult)
    {
        this.add = add;
        this.mult = mult;
    }

    public void apply(Dictionary<string, int> stats)
    {
        if (add != null && add.Count != 0)
        {
            foreach (KeyValuePair<string, int> kv in add)
            {
                stats[kv.Key] += kv.Value;
            }
        }

        if (mult != null && mult.Count != 0)
        {
            foreach (KeyValuePair<string, int> kv in mult)
            {
                stats[kv.Key] *= kv.Value;
            }
        }
    }

    public static StatModifier sum(Dictionary<string, StatModifier> mlist)
    {
        Dictionary<string, int> cadd = new Dictionary<string, int>();
        Dictionary<string, int> cmult = new Dictionary<string, int>();
        if (mlist != null && mlist.Count != 0)
        {
            foreach (StatModifier m in mlist.Values)
            {
                if (m.add != null && m.add.Count != 0)
                {
                    foreach (KeyValuePair<string, int> kv in m.add)
                    {
                        if (cadd.ContainsKey(kv.Key))
                        {
                            cadd[kv.Key] += kv.Value;
                        } 
                        else
                        {
                            cadd[kv.Key] = kv.Value;
                        }
                    }
                }
                if (m.mult != null && m.mult.Count != 0)
                {
                    foreach (KeyValuePair<string, int> kv in m.mult)
                    {
                        if (cmult.ContainsKey(kv.Key)) 
                        {
                            cmult[kv.Key] += kv.Value;
                        }
                        else
                        {
                            cmult[kv.Key] = kv.Value;
                        }
                    }
                }
            }
        }
        return new StatModifier(cadd, cmult);
    }

}
