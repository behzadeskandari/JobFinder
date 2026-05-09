using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.Services
{
    public  interface IDropDownServices
    {
        Task<ActionResult<IEnumerable<TechnicalOptionDto>>> GetTechnicalOptions();
    }
}
