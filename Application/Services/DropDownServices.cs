using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Contracts.Dtos.DropDown;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Services
{
    public class DropDownServices : IDropDownServices
    {

        public Task<ActionResult<IEnumerable<TechnicalOptionDto>>> GetTechnicalOptions()
        {
            throw new NotImplementedException();
        }
    }
}
