using BlazorUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUI.BlazorWebService
{
    public interface IBlazorWebService
    {
        public String  WebServiceUrl{get;}
        Task<TestComponentModel> Get();
        Task<TestComponentModel> GetMock();
    }
}
