using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DSA
{

    /// <summary>
    /// Implementation of a binary heap. Supports duplicate values via counting.
    /// Can be set to either a max heap or min heap.
    /// </summary>
    /// <typeparam name="T">The type of value to store in the heap.</typeparam>
    public class Heap<T> where T : IComparable<T>
    {
       
    }
}
