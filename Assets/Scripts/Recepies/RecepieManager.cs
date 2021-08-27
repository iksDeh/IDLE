using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RecepieManager : MonoBehaviour
{
    #region

    public static RecepieManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnRecepieLearned(Recepie recepie);
    public OnRecepieLearned onRecepieLearned;

    public List<Recepie> recepieList;
    private Dictionary<string, Recepie> list = new Dictionary<string, Recepie>();

    void Start()
    {
        foreach(Recepie r in recepieList)
        {
            r.Init();
            list.Add(r.name, r);
        }

        onRecepieLearned += LearnRecepie;
    }

    public Recepie GetRecepie(string recepie)
    {
        return list[recepie];
    }

    public void LearnRecepie(Recepie recepie)
    {
        recepie.SetIsLearned(true);
    }

}
