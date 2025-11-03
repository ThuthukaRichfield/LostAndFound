using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Common.Models;
using LostAndFound.Domain;
using LostAndFound.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Users
{
    public class ReportLostItemCommand : IRequest<OperationStatus>, ICommand
    {
        // Getters and setters
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        //public byte[]? Image { get; set; }
    }

    public class ReportLostItemCommandHandler : IRequestHandler<ReportLostItemCommand, OperationStatus>
    {
        // Variables
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        // Ctor
        public ReportLostItemCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<OperationStatus> Handle(ReportLostItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get User the Item will be linked to
                var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email.ToLower().Equals(request.UserEmail.ToLower()), cancellationToken);

                // Check if User exists
                if (user == null)
                {
                    return OperationStatus.CreateFromException("User not found.", new Exception($"User with ID {request.UserEmail} not found."));
                }

                // Create Lost Object
                var newItem = new Item
                {
                    Title = request.Title,
                    Category = request.Category,
                    LostDescription = request.Description,
                    Location = request.Location,
                    LostImage = null,
                    Status = ItemStatus.Lost,
                    User = user
                };

                // Add to DB
                _dbContext.Items.Add(newItem);

                // Save to DB
                var opStatus = await _dbContext.SaveChangesAsync(cancellationToken);
                return opStatus;
            }
            catch (Exception ex)
            {
                var opStatus = OperationStatus.CreateFromException("Error reporting lost item.", ex);
                return opStatus;
            }
        }
    }
}
