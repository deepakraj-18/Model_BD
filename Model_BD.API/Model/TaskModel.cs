using Model_BD.DAL.Models;

namespace Model_BD.API.Model
{
    public class AddModelTask
    {
        public string AgentName { get; set; }

        public string Model { get; set; }

        public string GuestFirstName { get; set; }

        public string GuestPhoneNo { get; set; }

        public string AmountFixed { get; set; }

        public string AdvanceReceived { get; set; }

        public DateTime? DateAndTime { get; set; }

        public string City { get; set; }

        public string TypeOfService { get; set; }

        public string Duration { get; set; }

        public string Incash { get; set; }

        public string Online { get; set; }

        public string TravelExpense { get; set; }

        public string GuestPreference { get; set; }

        public string StuffType { get; set; }

        public string NoOfServices { get; set; }

        public string Location { get; set; }

        public string GuestRating { get; set; }

        public string GuestOccupation { get; set; }

        public string YourProfit { get; set; }

        public string Comment { get; set; }

        public long? StatusId { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public long? DeletedBy { get; set; }

        public virtual TaskStatusMaster Status { get; set; }



    }

    public class AddAgentTask()
    {
        public long AgentId { get; set; }

        public long ModelId { get; set; }

        public string GuestFirstName { get; set; }

        public string GuestPhoneNo { get; set; }

        public string AmountFixed { get; set; }

        public string AdvanceReceived { get; set; }
        public string City { get; set; }
    }
    public class UpdateModelTask
    {
        public long Id { get; set; }

        public string ReferenceNo { get; set; }

        public string AgentName { get; set; }

        public string Model { get; set; }

        public string GuestFirstName { get; set; }

        public string GuestPhoneNo { get; set; }

        public string AmountFixed { get; set; }

        public string AdvanceReceived { get; set; }

        public DateTime? DateAndTime { get; set; }

        public string City { get; set; }

        public string TypeOfService { get; set; }

        public string Duration { get; set; }

        public string Incash { get; set; }

        public string Online { get; set; }

        public string TravelExpense { get; set; }

        public string GuestPreference { get; set; }

        public string StuffType { get; set; }

        public string NoOfServices { get; set; }

        public string Location { get; set; }

        public string GuestRating { get; set; }

        public string GuestOccupation { get; set; }

        public string YourProfit { get; set; }

        public string Comment { get; set; }

        public long? StatusId { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public long? DeletedBy { get; set; }

        public virtual TaskStatusMaster Status { get; set; }


    }

}