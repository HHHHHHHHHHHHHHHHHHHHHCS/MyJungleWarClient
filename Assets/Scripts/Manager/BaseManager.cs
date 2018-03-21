using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : BaseManager<T>
{
    protected GameFacade facade;
    public BaseManager(GameFacade _facade)
    {
        facade = _facade;
    }
    public virtual T OnInit() { return this as T; }
    public virtual void OnDesotry() { }
}
