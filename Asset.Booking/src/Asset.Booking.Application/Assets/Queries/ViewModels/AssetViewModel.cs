namespace Asset.Booking.Application.Assets.Queries.ViewModels;

public record AssetViewModel(
    int Id,
    string CategoryReference,
    string Specification,
    IEnumerable<string>? SpecificationIcons = null,
    string? Note = null,
    IEnumerable<string>? NoteIcons = null);
