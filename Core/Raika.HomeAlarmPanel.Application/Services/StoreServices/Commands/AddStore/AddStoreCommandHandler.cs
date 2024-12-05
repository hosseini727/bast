using MediatR;
using Microsoft.Extensions.Logging;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedApplicationServices.Services;

namespace Raika.HomeAlarmPanel.Application.Services.StoreServices.Commands.AddStore
{
    public class AddStoreCommandHandler
        : CommandQueryHandlerBase<AddStoreCommand, AddStoreCommandResponse>
        , IRequestHandler<AddStoreCommand, AddStoreCommandResponse>
    {
        public AddStoreCommandHandler(
            ILogger<AddStoreCommand> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService) : base(logger, currentApplicationService, currentUserService)
        {
        }

        public async Task<AddStoreCommandResponse> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
