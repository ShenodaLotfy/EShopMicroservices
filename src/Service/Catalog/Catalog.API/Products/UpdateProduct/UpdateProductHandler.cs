
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResponse>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator() {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name length must be between 2 and 150 characters");

            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater that 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle is called with command {@Command}", command);

            var product = await session.LoadAsync<Product>(command.Id);
            if (product is null)
                throw new ProductNotFoundException();

            product.Name = command.Name;
            product.Category = command.Category;    
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync();
            return new UpdateProductResponse(true);
        }
    }
}
