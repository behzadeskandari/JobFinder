using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Command
{

    public record DeleteFeatureCommand(int Id) : IRequest<bool>;
}
