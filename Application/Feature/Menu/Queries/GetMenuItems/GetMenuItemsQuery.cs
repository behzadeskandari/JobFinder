using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Contracts.Dtos.Menu;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Menu.Queries.GetMenuItems
{
    public class GetMenuItemsQuery : IRequest<List<MenuItemDto>>
    {
    }

    
}
