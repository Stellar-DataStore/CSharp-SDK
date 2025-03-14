namespace StellarDS.Demos.WPF;

public static class Utilities
{
    public static (int CurrentPage, int MaxPages) CalculatePagination(int offset, int pageSize, int totalRecords)
    {
        if (pageSize <= 0) throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
        if (offset < 0) throw new ArgumentException("Offset cannot be negative.", nameof(offset));
        if (totalRecords < 0) throw new ArgumentException("Total records cannot be negative.", nameof(totalRecords));

        var currentPage = (offset / pageSize) + 1;
        var maxPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        return (currentPage, maxPages);
    }
}