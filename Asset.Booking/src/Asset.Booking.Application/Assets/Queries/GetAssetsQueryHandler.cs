namespace Asset.Booking.Application.Assets.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Messaging;
using Asset.Booking.Application.Assets.Queries.ViewModels;
using SharedKernel;

public class GetAssetsQueryHandler : IQueryHandler<GetAssetsQuery, IEnumerable<AssetViewModel>>
{
    public Task<Result<IEnumerable<AssetViewModel>>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
    {
        var assets = Infrastructure.fakedb.assets;
        var assetViewModels = new List<AssetViewModel>();

        foreach (var asset in assets)
        {
            assetViewModels.Add(new AssetViewModel(
                asset.Id,
                asset.Details.CategoryReference,
                asset.Details.Specification,
                asset.Details.SpecificationIcons?.Split(';'),
                asset.Details.Notes,
                asset.Details.NotesIcons?.Split(';')));
        }

        return Task.FromResult(Result<IEnumerable<AssetViewModel>>.Success(assetViewModels));
    }
}
