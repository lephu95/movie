﻿using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        public int Percent {  get; set; }
        public int Quantity {  get; set; }
        public string Type {  get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Name {  get; set; }
        public bool IsActive {  get; set; }
        public int RankCustomerId {  get; set; }
        public IEnumerable<Bill> Bill { get; set; }
        public RankCustomer RankCustomer { get; set; }
    }
}
