namespace RxFlow.Catalog;
public sealed class FrameCatalogDraft { private readonly Dictionary<string, string> names = []; public string DisplayName(string sku) => names.GetValueOrDefault(sku, sku); }
