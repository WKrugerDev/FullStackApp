namespace ClientApp.Models
{
    // Represents a product returned from the /api/productlist endpoint
    // In production this would be a shared DTO used by both front-end and back-end
    public class Product
    {
        public int Id { get; set; }
        // Initialized to empty string to avoid null reference issues
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        // Nullable as a product without a category is a valid data state
        public Category? Category { get; set; }
    }

    // Represents the nested category object within a product response
    public class Categorys
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}