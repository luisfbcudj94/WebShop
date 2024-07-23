namespace WebShopAPI.Helpers
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int TotalCount { get; set; }
    }
}
