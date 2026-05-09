using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.FeatureEntity.Command
{
    public record UpdateFeatureCommand(int Id, string Title, string Description, string IconName, string Language, bool? IsActive) : IRequest<bool>;

}
