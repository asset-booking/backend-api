namespace Asset.Management.Domain.Asset;

using Booking.SharedKernel;
using Booking.SharedKernel.Abstractions;

public class Category(string name, int? parentCategoryId = null)
    : Entity<int>, IAggregateRoot
{
    private readonly List<Category> _subCategories = [];

    public string Name { get; private set; } = name;
    public int? ParentCategoryId { get; } = parentCategoryId;
    public bool IsParentCategory => ParentCategoryId is null;
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();

    public void ChangeName(string newName) =>
        Name = newName;

    public void AddSubCategory(string subCategoryName) =>
        _subCategories.Add(new Category(subCategoryName, Id));
}