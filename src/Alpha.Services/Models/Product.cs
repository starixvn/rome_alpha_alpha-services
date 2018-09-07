using System;

namespace Alpha.Services.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(int id, bool isHidden, DateTime? endOfSupportDate)
        {
            Id = id;
            IsHidden = isHidden;
            EndOfSupportDate = endOfSupportDate;
        }

        public int Id { get; }

        public bool IsHidden { get; }

        public DateTime? EndOfSupportDate { get; }
    }
}