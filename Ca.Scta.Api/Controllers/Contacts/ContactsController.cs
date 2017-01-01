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
        private readonly DapperCommandHandler<DeleteContactByIdCommand, GenericResult<int>> _deleteContactByIdCommandHandler;
        private readonly DapperCommandHandler<UpdateContactByIdCommand, GenericResult<int>> _updateContactByIdCommandHandler;

        public ContactsController(
            DapperCommandHandler<CreateContactCommand,GenericResult<int>> createContactCommandHandler,
            DapperQueryHandler<GetAllContactsQuery,List<Contact>> getAllContactsQueryHandler,
            DapperQueryHandler<GetContactByIdQuery,Contact> getContactByIdQueryHandler,
            DapperCommandHandler<DeleteContactByIdCommand,GenericResult<int>> deleteContactByIdCommandHandler,
            DapperCommandHandler<UpdateContactByIdCommand,GenericResult<int>> updateContactByIdCommandHandler    
            )
        {
            _createContactCommandHandler = createContactCommandHandler;
            _getAllContactsQueryHandler = getAllContactsQueryHandler;
            _getContactByIdQueryHandler = getContactByIdQueryHandler;
            _deleteContactByIdCommandHandler = deleteContactByIdCommandHandler;
            _updateContactByIdCommandHandler = updateContactByIdCommandHandler;
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
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteContactById(int id)
        {
            var command = new DeleteContactByIdCommand(id);
            var result = await _deleteContactByIdCommandHandler.HandleAsync(command);
            if (result.Succeeded)
            {
                var responseModel = new DeletedContactResultModel(id);
                return Ok(responseModel);
            }
            switch (result.ErrorReason)
            {
                case (ErrorReason.NotFound):
                    return NotFound();
                default:
                    return InternalServerError();
            }
        }

        [HttpPut]
        [Route]
        public async Task<IHttpActionResult> UpdateContactById(UpdateContactRequestModel model)
        {
            var cmd = new UpdateContactByIdCommand(
                model.City,
                model.Name,
                model.Description,
                model.Position,
                model.Email,
                model.ContactId);
            var result = await _updateContactByIdCommandHandler.HandleAsync(cmd);
            if (result.Succeeded)
            {
                var responseModel = new UpdateContactResponseModel(result.Data);
                return Ok(responseModel);
            }
            switch (result.ErrorReason)
            {
                case ErrorReason.NotFound:
                    return NotFound();

                default:
                    return InternalServerError();
            }
        }
        
    }

    public class UpdateContactResponseModel
    {
        public UpdateContactResponseModel(int id)
        {
            UpdatedContactId = id;
        }

        public int UpdatedContactId { get; set; }
    }

    public class UpdateContactRequestModel : CreateContactViewModel
    {
        public int ContactId { get; set; }
    }
    public class DeletedContactResultModel
    {
        public DeletedContactResultModel(int id)
        {
            this.DeletedContactId = id;
        }

        public int DeletedContactId { get; set; }
    }
}
