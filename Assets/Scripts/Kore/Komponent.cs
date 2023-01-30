using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kore
{
    public abstract class Komponent<T> : IKomponent where T : IKomponentData
    {
        protected T Data { get; private set; }

        public Komponent(T data)
        {
            Data = data;
            // TODO: maybe add to debug entity witch holder all component
        }

        public void AttachTo(Kentity kentity)
        {
            try
            {
                kentity.Komponents.Add(this as Komponent<IKomponentData>);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public static void AttachTo<T1, T2>(Kentity kentity, T2 data, Func<T2, T1> action) 
            where T1 : Komponent<T2> 
            where T2 : IKomponentData
        {
            try
            {
                var komponent = action.Invoke(data);
                komponent.AttachTo(kentity);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public void DetachFrom(Kentity kentity)
        {
            try
            {
                kentity.Komponents.Remove(this);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public static void DetachFrom<T1>(Kentity kentity, Func<T1> action) where T1 : Komponent<IKomponentData>
        {
            try
            {
                var komponent = action.Invoke();
                komponent.DetachFrom(kentity);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}