using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModelLayer
{
    public class ServiceResponse<T>
    {
            public int StatusCode { get; set; }
            public String Message { get; set; } = null;
            public T Data { get; set; }  
    }
}
