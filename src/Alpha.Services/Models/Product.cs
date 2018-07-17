namespace Alpha.Services.Models
{
    using System;

    public class Product
    {
        public Product()
        {
        }

        public Product(int id, string name, DateTime? endOfSupportDate)
        {
            Id = id;
            Name = name;
            EndOfSupportDate = endOfSupportDate;
        }

        public int Id { get; }

        public string Name { get; }

        public DateTime? EndOfSupportDate { get; }
    }
}