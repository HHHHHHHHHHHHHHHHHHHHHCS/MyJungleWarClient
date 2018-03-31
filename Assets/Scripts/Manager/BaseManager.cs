using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : BaseManager<T>
{
    public virtual T OnInit() { return this as T; }

    public virtual void OnUpdate()
    {

    }
    public virtual void OnDesotry() { }
}
