namespace DineMasterApi.DTO
{
    public class CreateDeliveryAddDTO
    {
        public int UserId {  get; set; }
        public string FullAddress { get; set; }
        public string Pincode { get; set; } 
        public string City { get; set; } 
        public string State { get; set; }


        public string Landmark { get; set; }
    }
}
