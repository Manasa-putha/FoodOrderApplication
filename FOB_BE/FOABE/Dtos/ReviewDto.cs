namespace FOABE.Dtos
{

    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int DishId { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
    }
}
