namespace ENTITY_L.Models.Category
{
    public class CategoryE
    {
        
        public string Id { get; set; }
        [FirestoreProperty]
        public DateTime fecha { get; set; }
        [FirestoreProperty]
        public string CategoryName { get; set; }
        [FirestoreProperty]
        public string TypeJob { get; set; }
        [FirestoreProperty]
        public string Designation { get; set; }
        [FirestoreProperty]

    }
}