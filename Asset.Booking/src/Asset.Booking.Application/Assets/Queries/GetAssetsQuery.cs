namespace Asset.Booking.Application.Assets.Queries;
using Abstractions.Messaging;
using Asset.Booking.Application.Assets.Queries.ViewModels;

public record GetAssetsQuery : IQuery<IEnumerable<AssetViewModel>>;
