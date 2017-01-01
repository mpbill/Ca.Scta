namespace Ca.Scta.Api.Controllers.Contacts
{
    public class CreatedResultModel 
    {
        public int CreatedId { get; set; }

        public CreatedResultModel(int createdId)
        {
            CreatedId = createdId;
        }
    }
}