using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.PsychologyTest
{
    public class QuestionAnswerDto
    {
        public Guid QuestionId { get; set; }
        public int Score { get; set; }
    }
}
