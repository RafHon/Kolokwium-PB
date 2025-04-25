namespace Model
{
    public class Student
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public int? GrupaID { get; set; }
        public Grupa? Grupa { get; set; }
    }

}
