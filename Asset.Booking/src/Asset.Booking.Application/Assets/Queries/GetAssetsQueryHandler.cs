namespace Asset.Booking.Application.Assets.Queries;

using Abstractions.Configurations;
using Abstractions.Messaging;
using Dapper;
using Npgsql;
using SharedKernel;
using ViewModels;

public class GetAssetsQueryHandler(IApplicationConfiguration configuration)
    : IQueryHandler<GetAssetsQuery, IEnumerable<AssetViewModel>>
{
    public async Task<Result<IEnumerable<AssetViewModel>>> Handle(GetAssetsQuery request,
        CancellationToken cancellationToken)
    {
        List<AssetViewModel> assetViewModels = [];

        await using NpgsqlConnection connection = new (configuration.SqlConnectionString);

        const string assetsQuery = "select * from assets";
        var assets = (await connection.QueryAsync(assetsQuery))
            .ToList();

        var categoryReferenceQuery = @"
            WITH RECURSIVE CategoryHierarchy AS (
                SELECT c.id, name, parent_category_id, a.id as asset_id
                FROM categories c
                join assets a on a.category_id = c.id
                WHERE a.id = any(@assetIds)

                UNION ALL

                SELECT c.id, c.name, c.parent_category_id, ch.asset_id
                FROM categories c
                JOIN CategoryHierarchy ch ON c.id = ch.parent_category_id
            )
            SELECT
                string_agg(name, ' ' ORDER BY id) AS parent_categories,
                asset_id
            FROM CategoryHierarchy
            group by asset_id;
        ";

        var assetIds = assets.Select(a => a.id).OfType<int>().ToList();
        List<dynamic> categoryReferences = (await connection.QueryAsync(categoryReferenceQuery, new { assetIds }))
            .ToList();

        foreach (var asset in assets)
        {
            assetViewModels.Add(new AssetViewModel(
                asset.id,
                categoryReferences.Single(c => c.asset_id.Equals(asset.id)).parent_categories,
                asset.specification,
                asset.specification_icons?.Split(";"),
                asset.note,
                asset.note_icons?.Split(";")));
        }

        return Result<IEnumerable<AssetViewModel>>.Success(assetViewModels);
    }
}