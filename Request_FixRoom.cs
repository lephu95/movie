namespace movie.Playloads.DataRequest
{
    public class Request_FixRoom
    {
        public string Capacity { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Cinemaname { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string CodeSheduces { get; set; }
        
        public string NameSheduces { get; set; }

        public int Number { get; set; }
        public int seatStatusId { get; set; }
        public string Line { get; set; }
    }
}
