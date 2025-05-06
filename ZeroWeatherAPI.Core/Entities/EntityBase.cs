namespace ZeroWeatherAPI.Core.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; private set; } = DateTime.Now;
        public DateTime? UpdateDate { get; private set; }
        
        public void UpdateAudit()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
