using class_work.Models;

namespace class_work.Configuration
{
    public class Moc
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "Iphone",
                Price = 100
            },
            new Product
            {
                Name = "Mac Book",
                Price = 200
            },
            new Product
            {
                Name = "Asus Predator",
                Price = 300
            }
        };
    }
}
