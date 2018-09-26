namespace DataAccess.Entities
{
    public class CodeTable
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int Category { get; set; }
        public string CategoryDescription { get; set; }
        public int Code { get; set; }
        public string CodeDescription { get; set; }
    }
}
