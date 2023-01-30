using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kore
{
    public class Kentity : MonoBehaviour
    {
        public int ID { get; }
        public List<IKomponent> Komponents { get; set; }
    }
}