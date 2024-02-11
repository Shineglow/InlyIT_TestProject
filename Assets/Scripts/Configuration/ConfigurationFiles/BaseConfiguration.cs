using System;
using System.Collections.Generic;

public class BaseConfiguration<T, E> where E : Enum
{
    protected Dictionary<E, T> data;

    public T GetConfiguration(E id) => data[id];
}
