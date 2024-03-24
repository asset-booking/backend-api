
dotnet ef migrations add --startup-project ../Asset.Booking.API --context ManagementContext --output-dir Migrations/Management InitialCreate
dotnet ef migrations add --startup-project ../Asset.Booking.API --context BookingContext  --output-dir Migrations/Booking InitialCreate

dotnet ef database update --startup-project ../Asset.Booking.API --context ManagementContext
dotnet ef database update --startup-project ../Asset.Booking.API --context BookingContext