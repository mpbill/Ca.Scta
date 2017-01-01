using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Cqrs.Contacts;

namespace Ca.Scta.Api.Controllers.Contacts
{
    [RoutePrefix("Contacts"), Authorize]
    public class ContactsController : ApiController
    {
        private readonly DapperCommandHandler<CreateContactCommand, GenericResult<int>> _createContactCommandHandler;

        public ContactsController(
            DapperCommandHandler<CreateContactCommand,GenericResult<int>> createContactCommandHandler
            )
        {
            _createContactCommandHandler = createContactCommandHandler;
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreateContact(CreateContactViewModel viewModel)
        {
            var command = new CreateContactCommand(
                viewModel.City,
                viewModel.Name,
                viewModel.Description,
                viewModel.Position,
                viewModel.Email);
            var result = await _createContactCommandHandler.HandleAsync(command);
            var resultModel = new CreatedResultModel(result.Data);
            var response = Ok(resultModel);
            return response;

        }
        
    }
}
