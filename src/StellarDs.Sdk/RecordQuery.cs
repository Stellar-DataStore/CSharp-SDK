using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StellarDs.Sdk;

/// <summary>
/// Paging, filtering, sorting, joining and projection for <c>GET /v1/data/table</c>.
/// <code>
/// var query = new RecordQuery()
///     .Where("Country", WhereOperator.Equal, "BE")
///     .AndWhere("Age", WhereOperator.GreaterThanOrEqual, 18)
///     .OrderBy("Name")
///     .Page(offset: 0, take: 50);
/// </code>
/// The builder methods encode values for you. The raw <see cref="WhereQuery"/>, <see cref="JoinQuery"/> and
/// <see cref="SortQuery"/> strings are sent as given (the SDK URL-encodes the whole parameter once).
/// </summary>
public sealed class RecordQuery
{
    private readonly StringBuilder _where = new();
    private readonly List<string> _joins = new();
    private readonly List<string> _sorts = new();
    private string? _rawWhere;
    private string? _rawJoin;
    private string? _rawSort;

    /// <summary>The number of records to skip.</summary>
    public long? Offset { get; set; }

    /// <summary>The maximum number of records to return.</summary>
    public long? Take { get; set; }

    /// <summary>Return only distinct rows (usually with <see cref="Select"/>).</summary>
    public bool Distinct { get; set; }

    /// <summary>The fields to return; all fields when empty.</summary>
    public IList<string> Select { get; } = new List<string>();

    /// <summary>
    /// The where filter in server syntax: <c>field;operator;value</c> clauses, each followed by <c>&amp;</c> (and) or
    /// <c>|</c> (or) when another clause follows, e.g. <c>Age;largerthan;30&amp;Country;equal;BE</c>. Setting it replaces
    /// clauses added with <see cref="Where"/>.
    /// </summary>
    public string? WhereQuery
    {
        get => _rawWhere ?? (_where.Length == 0 ? null : _where.ToString());
        set
        {
            _rawWhere = value;
            _where.Clear();
        }
    }

    /// <summary>The join in server syntax: <c>tableA;fieldA=tableB;fieldB</c>, several separated by <c>&amp;</c>. Setting it replaces joins added with <see cref="Join"/>.</summary>
    public string? JoinQuery
    {
        get => _rawJoin ?? (_joins.Count == 0 ? null : string.Join("&", _joins));
        set
        {
            _rawJoin = value;
            _joins.Clear();
        }
    }

    /// <summary>The sort in server syntax: <c>field;asc</c> or <c>field;desc</c>, several separated by commas.</summary>
    public string? SortQuery
    {
        get => _rawSort ?? (_sorts.Count == 0 ? null : string.Join(",", _sorts));
        set
        {
            _rawSort = value;
            _sorts.Clear();
        }
    }

    /// <summary>Adds a filter clause. Several clauses added with <see cref="Where"/> or <see cref="AndWhere"/> are combined with AND.</summary>
    public RecordQuery Where(string field, string op, object? value) => AddClause('&', field, op, value);

    /// <summary>Adds a filter clause combined with AND.</summary>
    public RecordQuery AndWhere(string field, string op, object? value) => AddClause('&', field, op, value);

    /// <summary>Adds a filter clause combined with OR with the previous clause.</summary>
    public RecordQuery OrWhere(string field, string op, object? value) => AddClause('|', field, op, value);

    /// <summary>Adds a sort on <paramref name="field"/>.</summary>
    public RecordQuery OrderBy(string field, bool descending = false)
    {
        ValidateName(field, nameof(field));
        _rawSort = null;
        _sorts.Add(field + (descending ? ";desc" : ";asc"));
        return this;
    }

    /// <summary>Adds a descending sort on <paramref name="field"/>.</summary>
    public RecordQuery OrderByDescending(string field) => OrderBy(field, descending: true);

    /// <summary>Joins <paramref name="tableA"/>.<paramref name="fieldA"/> to <paramref name="tableB"/>.<paramref name="fieldB"/>.</summary>
    public RecordQuery Join(string tableA, string fieldA, string tableB, string fieldB)
    {
        ValidateName(tableA, nameof(tableA));
        ValidateName(fieldA, nameof(fieldA));
        ValidateName(tableB, nameof(tableB));
        ValidateName(fieldB, nameof(fieldB));
        _rawJoin = null;
        _joins.Add($"{tableA};{fieldA}={tableB};{fieldB}");
        return this;
    }

    /// <summary>Restricts the returned fields.</summary>
    public RecordQuery Fields(params string[] fields)
    {
        foreach (var field in fields)
        {
            ValidateName(field, nameof(fields));
            Select.Add(field);
        }
        return this;
    }

    /// <summary>Sets <see cref="Offset"/> and <see cref="Take"/>.</summary>
    public RecordQuery Page(long offset, long take)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (take < 1) throw new ArgumentOutOfRangeException(nameof(take));
        Offset = offset;
        Take = take;
        return this;
    }

    /// <summary>Returns a copy with a different <see cref="Offset"/> and <see cref="Take"/>; used by the paging helper.</summary>
    public RecordQuery WithPage(long offset, long take)
    {
        var copy = new RecordQuery
        {
            Offset = offset,
            Take = take,
            Distinct = Distinct,
            _rawWhere = _rawWhere,
            _rawJoin = _rawJoin,
            _rawSort = _rawSort,
        };
        copy._where.Append(_where);
        copy._joins.AddRange(_joins);
        copy._sorts.AddRange(_sorts);
        foreach (var field in Select)
            copy.Select.Add(field);
        return copy;
    }

    internal IEnumerable<KeyValuePair<string, string?>> ToQueryParameters()
    {
        if (Offset is not null) yield return Pair("Offset", Offset.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        if (Take is not null) yield return Pair("Take", Take.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
        if (JoinQuery is { Length: > 0 } join) yield return Pair("JoinQuery", join);
        if (WhereQuery is { Length: > 0 } where) yield return Pair("WhereQuery", where);
        if (SortQuery is { Length: > 0 } sort) yield return Pair("SortQuery", sort);
        if (Distinct) yield return Pair("distinct", "true");
        if (Select.Count > 0) yield return Pair("select", string.Join(";", Select));
    }

    private static KeyValuePair<string, string?> Pair(string key, string value) => new(key, value);

    private RecordQuery AddClause(char logical, string field, string op, object? value)
    {
        ValidateName(field, nameof(field));
        if (string.IsNullOrWhiteSpace(op)) throw new ArgumentException("An operator is required.", nameof(op));
        if (_rawWhere is not null)
        {
            // Continue from a raw filter set earlier.
            _where.Append(_rawWhere);
            _rawWhere = null;
        }

        if (_where.Length > 0)
            _where.Append(logical);

        // The server URL-decodes each value once more after the query string itself is decoded, so encode the value
        // here; that keeps '&', '|', ';', '+' and '%' inside a value intact.
        var text = StellarDsValues.ToQueryValue(value);
        _where.Append(field).Append(';').Append(op).Append(';').Append(Uri.EscapeDataString(text));
        return this;
    }

    private static void ValidateName(string name, string parameter)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("A name is required.", parameter);
        if (name.IndexOfAny(new[] { ';', '&', '|', '=', ',' }) >= 0)
            throw new ArgumentException($"'{name}' contains a character that is not allowed in a field or table name.", parameter);
    }
}

/// <summary>The comparison operators of a where clause (see <see cref="RecordQuery.Where"/>).</summary>
public static class WhereOperator
{
    /// <summary>Equal to.</summary>
    public const string Equal = "equal";

    /// <summary>Not equal to.</summary>
    public const string NotEqual = "not_equal";

    /// <summary>Greater than.</summary>
    public const string GreaterThan = "largerthan";

    /// <summary>Greater than or equal to.</summary>
    public const string GreaterThanOrEqual = "largerthan_equal";

    /// <summary>Less than.</summary>
    public const string LessThan = "smallerthan";

    /// <summary>Less than or equal to.</summary>
    public const string LessThanOrEqual = "smallerthan_equal";

    /// <summary>SQL LIKE; use % as wildcard.</summary>
    public const string Like = "like";

    /// <summary>SQL NOT LIKE.</summary>
    public const string NotLike = "notlike";

    /// <summary>In a list of values.</summary>
    public const string In = "in";

    /// <summary>Not in a list of values.</summary>
    public const string NotIn = "notin";

    /// <summary>Is null.</summary>
    public const string Null = "null";

    /// <summary>All operators, for validation or UI lists.</summary>
    public static IReadOnlyList<string> All { get; } = new[]
    {
        Equal, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, Like, NotLike, In, NotIn, Null,
    }.ToList().AsReadOnly();
}
