using System;


namespace Pizza_Project
{
    public class OrderItem
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public decimal Quantity { get; set; }

        public virtual string GetOrderSummary()
        {
            return string.Format("Name: {0}\nSize: {1}\nQuantity: {2}",Name,Size,Quantity);
        }
    }
}
