namespace Asset.Booking.Application.Assets.Queries.ViewModels;

public record AssetViewModel(
    int Id,
    string CategoryReference,
    string? Specification = null,
    IEnumerable<string>? SpecificationIcons = null,
    string? Notes = null,
    IEnumerable<string>? NotesIcons = null);
