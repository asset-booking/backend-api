namespace Asset.Management.Domain.Asset;

using Booking.SharedKernel;
using Booking.SharedKernel.Abstractions;

public class Asset : Entity<int>, IAggregateRoot
{
    private readonly List<string> _specificationIcons = [];
    private readonly List<string> _noteIcons = [];

    private Asset(
        int categoryId,
        string specification,
        string? note)
    {
        CategoryId = categoryId;
        Specification = specification;
        Note = note;
    }

    public Asset(
        int categoryId,
        string specification,
        string? note,
        IEnumerable<string> specificationIcons,
        IEnumerable<string> noteIcons)
        : this(categoryId, specification, note)
    {
        _specificationIcons = specificationIcons.ToList();
        _noteIcons = noteIcons.ToList();
    }
    
    public int CategoryId { get; }
    public string Specification { get; private set; }
    public string? Note { get; private set; }

    public IReadOnlyCollection<string> SpecificationIcons => _specificationIcons.AsReadOnly();
    public IReadOnlyCollection<string> NoteIcons => _noteIcons.AsReadOnly();

    public void ChangeSpecification(string newSpec) =>
        Specification = newSpec;
    
    public void ChangeNote(string newNote) =>
        Note = newNote;

    public void AddSpecificationIcon(string iconName)
    {
        if (!_specificationIcons.Contains(iconName, StringComparer.OrdinalIgnoreCase))
        {
            _specificationIcons.Add(iconName);
        }
    }
    
    public void AddNoteIcon(string iconName)
    {
        if (!_noteIcons.Contains(iconName, StringComparer.OrdinalIgnoreCase))
        {
            _noteIcons.Add(iconName);
        }
    }

    public void RemoveSpecificationIcon(string iconName) =>
        _specificationIcons.RemoveAll(i => i.Equals(iconName, StringComparison.OrdinalIgnoreCase));
    
    public void RemoveNoteIcon(string iconName)=>
        _noteIcons.RemoveAll(i => i.Equals(iconName, StringComparison.OrdinalIgnoreCase));
}