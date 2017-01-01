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
        private readonly DapperQueryHandler<GetAllContactsQuery, List<Contact>> _getAllContactsQueryHandler;
        private readonly DapperQueryHandler<GetContactByIdQuery, Contact> _getContactByIdQueryHandler;

        public ContactsController(
            DapperCommandHandler<CreateContactCommand,GenericResult<int>> createContactCommandHandler,
            DapperQueryHandler<GetAllContactsQuery,List<Contact>> getAllContactsQueryHandler,
            DapperQueryHandler<GetContactByIdQuery,Contact> getContactByIdQueryHandler  
            )
        {
            _createContactCommandHandler = createContactCommandHandler;
            _getAllContactsQueryHandler = getAllContactsQueryHandler;
            _getContactByIdQueryHandler = getContactByIdQueryHandler;
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

        [Route]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAllContacts()
        {
            var allContacts = await _getAllContactsQueryHandler.HandleAsync(null);
            var allContactsAsResponseViewModels = allContacts.Select(contact => new ContactResponseViewModel(contact)).ToList();
            var responseViewModel = new GetAllContactsResponseViewModel(allContactsAsResponseViewModels);
            return Ok(responseViewModel);
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetContactById(int id)
        {
            var query = new GetContactByIdQuery(id);
            var contact = await _getContactByIdQueryHandler.HandleAsync(query);
            if (contact == null)
                return NotFound();
            var responseModel = new ContactResponseViewModel(contact);
            return Ok(responseModel);
        }
        
    }
}
