using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Validations
{
    public interface IValidatableObject<T>
    {
        bool Enable { get; set; }
    }
}
