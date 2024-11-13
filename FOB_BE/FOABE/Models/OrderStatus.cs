namespace FOABE.Models
{

    public enum OrderStatus
    {
        Placed,      // Customer placed the order
        Cancelled,   // Customer cancels the order
        Preparing,   // Restaurant owner starts preparing the order
        Ready,       // Restaurant owner marks the order as ready for pickup/delivery
        Delivered    // Restaurant owner marks the order as delivered
    }
}
