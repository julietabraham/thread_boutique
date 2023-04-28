namespace Sample.ViewModels
{
    public class CartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal? CartAmount { get; set; } = null;
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}
