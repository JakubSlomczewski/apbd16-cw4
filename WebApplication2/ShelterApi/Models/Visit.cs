namespace ShelterApi.Models
{
    public class Visit
    {
        public int      Id          { get; set; }
        public int      AnimalId    { get; set; }
        public DateTime Date        { get; set; }
        public string   Description { get; set; } = string.Empty;
        public decimal  Price       { get; set; }
    }
}