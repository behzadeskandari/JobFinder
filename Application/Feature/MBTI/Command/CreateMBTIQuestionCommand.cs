using FluentResults;
using JobFinder.Contracts.Dtos.MbtiTest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Command
{
    public class CreateMBTIQuestionCommand : IRequest<Result<MBTIQuestionDTO>>
    {
        public MBTIQuestionDTO MBTIQuestion { get; set; }
    }
}
