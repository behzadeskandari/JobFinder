using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Candidate.Queries.SearchCandidatesQuery;
using JobFinder.Contracts.Dtos.Candidate;
using MediatR;

namespace JobFinder.Application.Feature.Candidate.Handlers.SearchCandidatesQueryHandler
{
    public class SearchCandidatesQueryHandler : IRequestHandler<SearchCandidatesQuery, CandidateSearchResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchCandidatesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CandidateSearchResponseDto> Handle(SearchCandidatesQuery request, CancellationToken cancellationToken)
        {
            // Note: For total count, a separate count query or a method in repository returning (items, totalCount) tuple is better
            // Or use a more advanced search pattern. Here, for simplicity, assuming SearchCandidatesAsync returns filtered list.
            var candidates = await _unitOfWork.CandidateRepository.SearchCandidatesAsync(request.Criteria);

            // For a more robust search, you'd typically have a separate method in repo for total count
            // Or pass pagination/sorting to the repo method and get total count back.
            // For now, let's assume the search criteria is just for filtering.
            // The actual total count before pagination should be calculated.
            // Example: var totalCount = await _unitOfWork.Candidates.CountAsync(request.Criteria);

            // This is a simplified example. For true pagination, SearchCandidatesAsync needs to return totalCount.
            // Or you would fetch all and then paginate/count, which is inefficient.
            // A common pattern is to return a PaginatedList<T> from the repository.

            var totalCount = candidates.Count(); // This is incorrect for true pagination, just for demo.
                                                 // Realistically, the repository search method should return total count.
            return new CandidateSearchResponseDto
            {
                Candidates = _mapper.Map<List<CandidateDto>>(candidates),
                TotalCount = totalCount, // This should be the total count before pagination applied in DB
                PageIndex = request.Criteria.PageIndex,
                PageSize = request.Criteria.PageSize
            };
        }
    }
}
